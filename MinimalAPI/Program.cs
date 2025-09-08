using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Dominio.DTOs;
using MinimalAPI.Dominio.Entidades;
using MinimalAPI.Dominio.Interfaces;
using MinimalAPI.Dominio.ModelViews;
using MinimalAPI.Dominio.Servicos;
using MinimalAPI.Infraestrutura.Db;

#region Builder Configuration
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdministradorServicos, AdministradorServicos>();
builder.Services.AddScoped<IVeiculoServicos, VeiculoServico>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContexto>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("mysql");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
#endregion

#region Home Endpoint
app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");
#endregion

#region Administrador Endpoints
app.MapPost("/administradores/login", ([FromBody] LoginDTO loginDTO, IAdministradorServicos administradorServicos) =>
{
    if (administradorServicos.Login(loginDTO) != null)
    {
        return Results.Ok("Login successful");
    }
    return Results.Unauthorized();
}).WithTags("Administradores");

app.MapGet("/administradores", ([FromQuery] int? pagina, IAdministradorServicos administradorServicos) =>
{
    return Results.Ok(administradorServicos.Todos(pagina));

}).WithTags("Administradores");

app.MapGet("/administradores/{id}", ([FromRoute] int id, IAdministradorServicos administradorServicos) =>
{
    var administrador = administradorServicos.BuscaPorID(id);
    return administrador != null ? Results.Ok(administrador) : Results.NotFound();
    
}).WithTags("Administradores");

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


    return Results.Created($"/administrador/{administrador.Id}", administrador);
}).WithTags("Administradores");
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

}).WithTags("Veiculos");

app.MapGet("/veiculos", ([FromQuery] int? pagina, IVeiculoServicos veiculoServicos) =>
{
    var veiculos = veiculoServicos.Todos(pagina);
    return Results.Ok(veiculos);
}).WithTags("Veiculos");

app.MapGet("/veiculos/{id}", ([FromRoute] int id, IVeiculoServicos veiculoServicos) =>
{
    var veiculo = veiculoServicos.BuscaPorID(id);
    return veiculo != null ? Results.Ok(veiculo) : Results.NotFound();
}).WithTags("Veiculos");

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
}).WithTags("Veiculos");

app.MapDelete("/veiculos/{id}", ([FromRoute] int id, IVeiculoServicos veiculoServicos) =>
{
    var veiculo = veiculoServicos.BuscaPorID(id);
    if (veiculo == null) return Results.NotFound();
    veiculoServicos.Excluir(veiculo);
    return Results.NoContent();
}).WithTags("Veiculos");
#endregion

#region App
app.Run();
#endregion
