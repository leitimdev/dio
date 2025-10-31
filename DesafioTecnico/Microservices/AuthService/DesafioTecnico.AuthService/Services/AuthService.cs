using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DesafioTecnico.AuthService.Data;
using DesafioTecnico.AuthService.Models;
using DesafioTecnico.Shared.DTOs;

namespace DesafioTecnico.AuthService.Services
{
    public interface IAuthService
    {
        Task<UsuariosLoginResponseDTO> LoginAsync(UsuariosLoginDTO loginDto);
        Task<UsuariosDTO> CreateUserAsync(UsuariosCreateDTO createDto);
        Task<bool> ValidateTokenAsync(string token);
    }

    public class AuthService : IAuthService
    {
        private readonly AuthDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _jwtKey;
        private readonly string _jwtIssuer;

        public AuthService(AuthDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _jwtKey = _configuration["Jwt:Key"] ?? "MinhaChaveSuperSecreta123456789012345";
            _jwtIssuer = _configuration["Jwt:Issuer"] ?? "AuthService";
        }

        public async Task<UsuariosLoginResponseDTO> LoginAsync(UsuariosLoginDTO loginDto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (usuario == null || usuario.Senha != loginDto.Senha)
                throw new UnauthorizedAccessException("Email ou senha inválidos.");

            var token = GenerateJwtToken(usuario);
            var expiration = DateTime.UtcNow.AddHours(24);

            return new UsuariosLoginResponseDTO(
                usuario.Id,
                usuario.Email,
                usuario.Perfil,
                token,
                expiration
            );
        }

        public async Task<UsuariosDTO> CreateUserAsync(UsuariosCreateDTO createDto)
        {
            var emailExists = await _context.Usuarios
                .AnyAsync(u => u.Email == createDto.Email);

            if (emailExists)
                throw new InvalidOperationException($"Email {createDto.Email} já está em uso.");

            var usuario = new Usuario
            {
                Email = createDto.Email,
                Senha = createDto.Senha,
                Perfil = createDto.Perfil
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new UsuariosDTO(usuario.Id, usuario.Email, usuario.Perfil);
        }

        public Task<bool> ValidateTokenAsync(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtKey);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtIssuer,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.Perfil)
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                Issuer = _jwtIssuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}