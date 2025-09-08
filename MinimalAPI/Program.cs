using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Dominio.DTOs;
using MinimalAPI.Dominio.Interfaces;
using MinimalAPI.Dominio.Servicos;
using MinimalAPI.Infraestrutura.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdministradorServicos, AdministradorServicos>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("mysql"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql")));
});

var app = builder.Build();

app.MapGet("/", () => Results.Json(new MinimalAPI.Dominio.ModelViews.Home()));

app.MapPost("/login", ([FromBody] LoginDTO loginDTO, IAdministradorServicos administradorServicos) =>
{    
    if (administradorServicos.Login(loginDTO) != null)
    {
        return Results.Ok("Login successful");
    }
    return Results.Unauthorized();
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();



