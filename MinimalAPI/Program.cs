using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinimalAPI.Dominio.DTOs;
using MinimalAPI.Dominio.Entidades;
using MinimalAPI.Dominio.Enuns;
using MinimalAPI.Dominio.Interfaces;
using MinimalAPI.Dominio.ModelViews;
using MinimalAPI.Dominio.Servicos;
using MinimalAPI.Infraestrutura.Db;

#region Builder Configuration
var builder = WebApplication.CreateBuilder(args);
var key = builder.Configuration.GetSection("Jwt").ToString();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key ?? string.Empty)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization();
builder.Services.AddScoped<IAdministradorServicos, AdministradorServicos>();
builder.Services.AddScoped<IVeiculoServicos, VeiculoServico>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Insira o token JWT aqui"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
    });
});

builder.Services.AddDbContext<DbContexto>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("mysql");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
#endregion

#region Home Endpoint
app.MapGet("/", () => Results.Json(new Home())).AllowAnonymous().WithTags("Home");
#endregion

#region Administrador Endpoints

string GerarTokenJwt(Administrador administrador)
{
    if (string.IsNullOrEmpty(key)) return string.Empty;

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new List<Claim>()
    {
        new Claim("Email", administrador.Email),
        new Claim("Perfil", administrador.Perfil),
        new Claim(ClaimTypes.Role, administrador.Perfil)
    };

    var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}

app.MapPost("/administradores/login", ([FromBody] LoginDTO loginDTO, IAdministradorServicos administradorServicos) =>
{
    var adm = administradorServicos.Login(loginDTO);
    if (adm != null)
    {
        string token = GerarTokenJwt(adm);
        return Results.Ok(new AdministradorLogado
        {
            Email = adm.Email,
            Perfil = adm.Perfil,
            Token = token
        });
    }
    return Results.Unauthorized();
}).AllowAnonymous().WithTags("Administradores");

app.MapGet("/administradores", ([FromQuery] int? pagina, IAdministradorServicos administradorServicos) =>
{
    var adms = new List<AdministradorModelView>();
    var administradores = administradorServicos.Todos(pagina);
    foreach(var adm in administradores)
    {
        // Corrija esta linha com tratamento de erro:
        Perfil perfil;
        if (!Enum.TryParse<Perfil>(adm.Perfil, true, out perfil))
        {
            perfil = Perfil.Adm;
        }

        adms.Add(new AdministradorModelView
        {
            Id = adm.Id,
            Email = adm.Email,
            Perfil = perfil.ToString()
        });
    }
    return Results.Ok(adms);

}).RequireAuthorization(new AuthorizeAttribute{ Roles = "Adm"}).WithTags("Administradores");

app.MapGet("/administradores/{id}", ([FromRoute] int id, IAdministradorServicos administradorServicos) =>
{
    var administrador = administradorServicos.BuscaPorID(id);
    if(administrador == null) return Results.NotFound();
    return Results.Ok(new AdministradorModelView
        {
            Id = administrador.Id,
            Email = administrador.Email,
            Perfil = administrador.Perfil.ToString()
        });

}).RequireAuthorization(new AuthorizeAttribute{ Roles = "Adm"}).WithTags("Administradores");

app.MapPost("/administradores", ([FromBody] AdministradorDTO administradorDTO, IAdministradorServicos administradorServicos) =>
{

    var mensagens = new ErrosDeValidacoes
    {
        Mensagens = new List<string>()
    };

    if(administradorDTO.Email == null)
    {
        mensagens.Mensagens.Add("Email is required.");
    }
    if(administradorDTO.Senha == null)
    {
        mensagens.Mensagens.Add("Senha is required.");
    }
    if(administradorDTO.Perfil == null)
    {
        mensagens.Mensagens.Add("Perfil is required.");
    }

    if (mensagens.Mensagens.Count > 0)
    {
        return Results.BadRequest(mensagens);
    }

    var administrador = new Administrador
    {
        Email = administradorDTO.Email ?? string.Empty,
        Senha = administradorDTO.Senha ?? string.Empty,
        Perfil = administradorDTO.Perfil?.ToString() ?? string.Empty
    };
    administradorServicos.Incluir(administrador);


    return Results.Created($"/administrador/{administrador.Id}", new AdministradorModelView
        {
            Id = administrador.Id,
            Email = administrador.Email,
            Perfil = administrador.Perfil.ToString()
        });
}).RequireAuthorization(new AuthorizeAttribute{ Roles = "Adm"}).WithTags("Administradores");
#endregion

#region Veiculo Endpoints
ErrosDeValidacoes validaDTO(VeiculoDTO veiculoDTO)
{
    var mensagens = new ErrosDeValidacoes
    {
        Mensagens = new List<string>()
    };
    if (string.IsNullOrEmpty(veiculoDTO.Marca))
    {
        mensagens.Mensagens.Add("Marca is required.");
    }
    if (string.IsNullOrEmpty(veiculoDTO.Nome))
    {
        mensagens.Mensagens.Add("Nome is required.");
    }
    if (veiculoDTO.Ano <= 0)
    {
        mensagens.Mensagens.Add("Ano must be a positive integer.");
    }

    return mensagens;
}

app.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServicos veiculoServicos) =>
{
    var mensagens = validaDTO(veiculoDTO);
    if (mensagens.Mensagens.Count > 0)
    {
        return Results.BadRequest(mensagens);
    }
    var veiculo = new Veiculo
    {
        Marca = veiculoDTO.Marca,
        Nome = veiculoDTO.Nome,
        Ano = veiculoDTO.Ano
    };
    veiculoServicos.Incluir(veiculo);
    return Results.Created($"/veiculo/{veiculo.Id}", veiculo);

}).RequireAuthorization(new AuthorizeAttribute{ Roles = "Adm, Editor"}).WithTags("Veiculos");

app.MapGet("/veiculos", ([FromQuery] int? pagina, IVeiculoServicos veiculoServicos) =>
{
    var veiculos = veiculoServicos.Todos(pagina);
    return Results.Ok(veiculos);
}).RequireAuthorization(new AuthorizeAttribute{ Roles = "Adm, Editor"}).WithTags("Veiculos");

app.MapGet("/veiculos/{id}", ([FromRoute] int id, IVeiculoServicos veiculoServicos) =>
{
    var veiculo = veiculoServicos.BuscaPorID(id);
    return veiculo != null ? Results.Ok(veiculo) : Results.NotFound();
}).RequireAuthorization(new AuthorizeAttribute{ Roles = "Adm, Editor"}).WithTags("Veiculos");

app.MapPut("/veiculos/{id}", ([FromRoute] int id, VeiculoDTO veiculoDTO, IVeiculoServicos veiculoServicos) =>
{
    var mensagens = validaDTO(veiculoDTO);
    var veiculo = veiculoServicos.BuscaPorID(id);

    if (mensagens.Mensagens.Count > 0)
    {
        return Results.BadRequest(mensagens);
    }

    if (veiculo == null)
    {
        return Results.NotFound();
    }
    veiculo.Marca = veiculoDTO.Marca;
    veiculo.Nome = veiculoDTO.Nome;
    veiculo.Ano = veiculoDTO.Ano;
    veiculoServicos.Alterar(veiculo);

    return Results.Ok(veiculo);
}).RequireAuthorization(new AuthorizeAttribute{ Roles = "Adm, Editor"}).WithTags("Veiculos");

app.MapDelete("/veiculos/{id}", ([FromRoute] int id, IVeiculoServicos veiculoServicos) =>
{
    var veiculo = veiculoServicos.BuscaPorID(id);
    if (veiculo == null) return Results.NotFound();
    veiculoServicos.Excluir(veiculo);
    return Results.NoContent();
}).RequireAuthorization(new AuthorizeAttribute{ Roles = "Adm, Editor"}).WithTags("Veiculos");
#endregion

#region App
app.Run();
#endregion
