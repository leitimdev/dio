using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using DesafioTecnico.AuthService.Data;
using DesafioTecnico.AuthService.Services;
using DesafioTecnico.Shared.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Banco de Dados
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))
    )
);

// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AuthService API",
        Version = "v1",
        Description = "Microserviço de Autenticação"
    });
});

// Injeção de Dependência
builder.Services.AddScoped<IAuthService, DesafioTecnico.AuthService.Services.AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthService API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

// ========================================
// ENDPOINTS DO MICROSERVIÇO DE AUTENTICAÇÃO
// ========================================

app.MapPost("/api/auth/login", async (UsuariosLoginDTO loginDto, IAuthService authService) =>
{
    try
    {
        var resultado = await authService.LoginAsync(loginDto);
        return Results.Ok(new { success = true, data = resultado });
    }
    catch (UnauthorizedAccessException)
    {
        return Results.Unauthorized();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { success = false, message = ex.Message });
    }
})
.WithName("Login")
.WithTags("Authentication")
.WithOpenApi();

app.MapPost("/api/auth/register", async (UsuariosCreateDTO createDto, IAuthService authService) =>
{
    try
    {
        var usuario = await authService.CreateUserAsync(createDto);
        return Results.Created($"/api/users/{usuario.Id}", new { success = true, data = usuario });
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(new { success = false, message = ex.Message });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { success = false, message = ex.Message });
    }
})
.WithName("Register")
.WithTags("Authentication")
.WithOpenApi();

app.MapGet("/api/health", () => Results.Ok(new { 
    success = true, 
    service = "AuthService",
    message = "AuthService funcionando", 
    timestamp = DateTime.UtcNow 
}))
.WithName("HealthCheck")
.WithTags("Health")
.WithOpenApi();

app.Run();
