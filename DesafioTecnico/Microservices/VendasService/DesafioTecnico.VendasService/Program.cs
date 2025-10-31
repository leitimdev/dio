using DesafioTecnico.VendasService.Data;
using DesafioTecnico.VendasService.Services;
using DesafioTecnico.VendasService.Messaging;
using DesafioTecnico.Shared.DTOs;
using DesafioTecnico.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "server=localhost;port=3307;database=desafiotecnico_vendas;user=root;password=123456;";

builder.Services.AddDbContext<VendasDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? "minha_chave_secreta_super_segura_32_caracteres_ou_mais";
var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// Services
builder.Services.AddScoped<IVendasService, VendasService>();
builder.Services.AddSingleton<IMessageBus, RabbitMQMessageBus>();
builder.Services.AddHostedService<EstoqueValidadoEventHandler>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

// Endpoints
app.MapGet("/api/vendas", async (IVendasService vendasService) =>
{
    var vendas = await vendasService.GetAllAsync();
    return Results.Ok(vendas);
})
.WithName("GetVendas");

app.MapGet("/api/vendas/{id}", async (int id, IVendasService vendasService) =>
{
    var venda = await vendasService.GetByIdAsync(id);
    return venda is not null ? Results.Ok(venda) : Results.NotFound();
})
.WithName("GetVenda");

app.MapPost("/api/vendas", async (VendasDTO vendaDto, IVendasService vendasService) =>
{
    var venda = await vendasService.CreateAsync(vendaDto);
    return Results.Created($"/api/vendas/{venda.Id}", venda);
})
.WithName("CreateVenda")
.RequireAuthorization();

app.MapPut("/api/vendas/{id}", async (int id, VendasDTO vendaDto, IVendasService vendasService) =>
{
    var venda = await vendasService.UpdateAsync(id, vendaDto);
    return venda is not null ? Results.Ok(venda) : Results.NotFound();
})
.WithName("UpdateVenda")
.RequireAuthorization();

app.MapDelete("/api/vendas/{id}", async (int id, IVendasService vendasService) =>
{
    var success = await vendasService.DeleteAsync(id);
    return success ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteVenda")
.RequireAuthorization();

app.MapPost("/api/vendas/{id}/confirm", async (int id, IVendasService vendasService) =>
{
    var success = await vendasService.ConfirmSaleAsync(id);
    return success ? Results.Ok(new { Message = "Venda confirmada" }) : Results.BadRequest("Não foi possível confirmar a venda");
})
.WithName("ConfirmVenda")
.RequireAuthorization();

app.MapPost("/api/vendas/{id}/cancel", async (int id, IVendasService vendasService) =>
{
    var success = await vendasService.CancelSaleAsync(id);
    return success ? Results.Ok(new { Message = "Venda cancelada" }) : Results.BadRequest("Não foi possível cancelar a venda");
})
.WithName("CancelVenda")
.RequireAuthorization();

app.MapGet("/api/vendas/cliente/{cliente}", async (string cliente, IVendasService vendasService) =>
{
    var vendas = await vendasService.GetByClientAsync(cliente);
    return Results.Ok(vendas);
})
.WithName("GetVendasByClient");

app.MapGet("/api/vendas/status/{status}", async (string status, IVendasService vendasService) =>
{
    var vendas = await vendasService.GetByStatusAsync(status);
    return Results.Ok(vendas);
})
.WithName("GetVendasByStatus");

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<VendasDbContext>();
    await context.Database.EnsureCreatedAsync();
}

app.Run();
