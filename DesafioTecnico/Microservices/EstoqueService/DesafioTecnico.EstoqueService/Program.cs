using DesafioTecnico.EstoqueService.Data;
using DesafioTecnico.EstoqueService.Services;
using DesafioTecnico.EstoqueService.Messaging;
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
    ?? "server=localhost;port=3307;database=desafiotecnico_estoque;user=root;password=123456;";

builder.Services.AddDbContext<EstoqueDbContext>(options =>
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
builder.Services.AddScoped<IEstoqueService, EstoqueService>();
builder.Services.AddSingleton<IMessageBus, RabbitMQMessageBus>();
builder.Services.AddHostedService<VendaCriadaEventHandler>();

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
app.MapGet("/api/estoque", async (IEstoqueService estoqueService) =>
{
    var estoques = await estoqueService.GetAllAsync();
    return Results.Ok(estoques);
})
.WithName("GetEstoques");

app.MapGet("/api/estoque/{id}", async (int id, IEstoqueService estoqueService) =>
{
    var estoque = await estoqueService.GetByIdAsync(id);
    return estoque is not null ? Results.Ok(estoque) : Results.NotFound();
})
.WithName("GetEstoque");

app.MapPost("/api/estoque", async (EstoqueDTO estoqueDto, IEstoqueService estoqueService) =>
{
    var estoque = await estoqueService.CreateAsync(estoqueDto);
    return Results.Created($"/api/estoque/{estoque.Id}", estoque);
})
.WithName("CreateEstoque")
.RequireAuthorization();

app.MapPut("/api/estoque/{id}", async (int id, EstoqueDTO estoqueDto, IEstoqueService estoqueService) =>
{
    var estoque = await estoqueService.UpdateAsync(id, estoqueDto);
    return estoque is not null ? Results.Ok(estoque) : Results.NotFound();
})
.WithName("UpdateEstoque")
.RequireAuthorization();

app.MapDelete("/api/estoque/{id}", async (int id, IEstoqueService estoqueService) =>
{
    var success = await estoqueService.DeleteAsync(id);
    return success ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteEstoque")
.RequireAuthorization();

app.MapPost("/api/estoque/{id}/validate", async (int id, int quantity, IEstoqueService estoqueService) =>
{
    var isValid = await estoqueService.ValidateStockAsync(id, quantity);
    return Results.Ok(new { IsValid = isValid });
})
.WithName("ValidateStock");

app.MapPost("/api/estoque/{id}/update-stock", async (int id, int quantity, IEstoqueService estoqueService) =>
{
    var success = await estoqueService.UpdateStockAsync(id, quantity);
    return success ? Results.Ok() : Results.BadRequest("Estoque insuficiente");
})
.WithName("UpdateStock")
.RequireAuthorization();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EstoqueDbContext>();
    await context.Database.EnsureCreatedAsync();
}

app.Run();
