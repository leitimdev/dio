using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using DesafioTecnico.Domain.DTOs;
using DesafioTecnico.Domain.Entities;
using DesafioTecnico.Domain.Interfaces;
using DesafioTecnico.Infraestrutura.Db;

namespace DesafioTecnico.Domain.Services
{
    public class UsuariosServicos : IUsuariosServicos
    {
        private readonly DbContexto _context;
        private readonly IConfiguration _configuration;
        private readonly string _jwtKey;
        private readonly string _jwtIssuer;

        public UsuariosServicos(DbContexto context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _jwtKey = _configuration["Jwt:Key"] ?? "MinhaChaveSuperSecreta123456789012345";
            _jwtIssuer = _configuration["Jwt:Issuer"] ?? "DesafioTecnico";
        }
        // AUTENTICAÇÃO
        public async Task<UsuariosLoginResponseDTO> Login(UsuariosLoginDTO loginDto)
        {
            try
            {
                // Buscar usuário por email
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

                if (usuario == null)
                    throw new UnauthorizedAccessException("Email ou senha inválidos.");

                // Verificar senha (comparação direta sem criptografia)
                if (loginDto.Senha != usuario.Senha)
                    throw new UnauthorizedAccessException("Email ou senha inválidos.");

                // Gerar token JWT
                var token = GerarTokenJWT(usuario);
                var expiracao = DateTime.UtcNow.AddHours(24);

                return new UsuariosLoginResponseDTO(
                    usuario.Id,
                    usuario.Email,
                    usuario.Perfil,
                    token,
                    expiracao
                );
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao realizar login: {ex.Message}", ex);
            }
        }

        public Task<bool> ValidarToken(string token)
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

        public async Task Logout(int usuarioId)
        {
            // Para JWT stateless, logout é tratado no frontend removendo o token
            // Aqui podemos implementar uma blacklist de tokens se necessário
            await Task.CompletedTask;
        }

        // CRUD DE USUÁRIOS
        public async Task<UsuariosDTO> Criar(UsuariosCreateDTO createDto)
        {
            try
            {
                // Verificar se email já existe
                var emailExiste = await _context.Usuarios
                    .AnyAsync(u => u.Email == createDto.Email);

                if (emailExiste)
                    throw new InvalidOperationException($"Email {createDto.Email} já está em uso.");

                // Criar novo usuário (senha sem criptografia)
                var novoUsuario = new Usuarios
                {
                    Email = createDto.Email,
                    Senha = createDto.Senha, // Senha sem criptografia
                    Perfil = createDto.Perfil
                };

                _context.Usuarios.Add(novoUsuario);
                await _context.SaveChangesAsync();

                return MapearParaDTO(novoUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar usuário: {ex.Message}", ex);
            }
        }

        public async Task<UsuariosDTO> Atualizar(UsuariosUpdateDTO updateDto)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(updateDto.Id);

                if (usuario == null)
                    throw new ArgumentException($"Usuário com ID {updateDto.Id} não encontrado.");

                // Verificar conflito de email
                var conflito = await _context.Usuarios
                    .AnyAsync(u => u.Email == updateDto.Email && u.Id != updateDto.Id);

                if (conflito)
                    throw new InvalidOperationException($"Email {updateDto.Email} já está em uso por outro usuário.");

                // Atualizar dados
                usuario.Email = updateDto.Email;
                usuario.Perfil = updateDto.Perfil;

                await _context.SaveChangesAsync();

                return MapearParaDTO(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar usuário: {ex.Message}", ex);
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);

                if (usuario == null)
                    return false;

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir usuário: {ex.Message}", ex);
            }
        }

        // GESTÃO DE SENHAS
        public async Task<bool> AlterarSenha(UsuariosAlterarSenhaDTO alterarSenhaDto)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(alterarSenhaDto.Id);

                if (usuario == null)
                    throw new ArgumentException($"Usuário com ID {alterarSenhaDto.Id} não encontrado.");

                // Verificar senha atual (comparação direta)
                if (alterarSenhaDto.SenhaAtual != usuario.Senha)
                    throw new UnauthorizedAccessException("Senha atual incorreta.");

                // Atualizar senha (sem criptografia)
                usuario.Senha = alterarSenhaDto.NovaSenha;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao alterar senha: {ex.Message}", ex);
            }
        }

        public async Task<bool> ValidarSenha(int usuarioId, string senha)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(usuarioId);

                if (usuario == null)
                    return false;

                return senha == usuario.Senha; // Comparação direta
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao validar senha: {ex.Message}", ex);
            }
        }

        // MÉTODOS AUXILIARES
        private string GerarTokenJWT(Usuarios usuario)
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

        private static UsuariosDTO MapearParaDTO(Usuarios usuario)
        {
            return new UsuariosDTO(
                usuario.Id,
                usuario.Email,
                usuario.Perfil
            );
        }
    }
}