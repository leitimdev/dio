using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors();

// Health check endpoint
app.MapGet("/health", () => Results.Ok(new { Status = "Healthy", Timestamp = DateTime.UtcNow }));

// API Gateway info endpoint
app.MapGet("/gateway/info", () => Results.Ok(new 
{ 
    Service = "API Gateway",
    Version = "1.0.0",
    Description = "Gateway centralizado para microserviços do Desafio Técnico"
}));

app.Run();