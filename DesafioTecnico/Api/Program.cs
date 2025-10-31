using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using DesafioTecnico.Infraestrutura.Db;
using DesafioTecnico.Domain.Interfaces;
using DesafioTecnico.Domain.Services;
using DesafioTecnico.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Banco de Dados
builder.Services.AddDbContext<DbContexto>(options =>
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

// Configuração do JWT
var jwtSection = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSection["Key"] ?? "MinhaChaveSuperSecreta123456789012345"); // Chave padrão com 35 caracteres (280 bits)

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Desafio Técnico API",
        Version = "v1",
        Description = "API para gerenciamento de estoque e vendas"
    });

    // Configuração para JWT no Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Digite o token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Injeção de Dependência dos Serviços
builder.Services.AddScoped<IEstoqueServicos, EstoqueServicos>();
builder.Services.AddScoped<IVendasServicos, VendasServicos>();
builder.Services.AddScoped<IUsuariosServicos, UsuariosServicos>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio Técnico API v1");
        c.RoutePrefix = string.Empty; // Swagger na raiz
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

// ========================================
// ENDPOINTS BASEADOS NO MODELO DE NEGÓCIOS
// ========================================

// =====================================
// AUTENTICAÇÃO (Não requer autenticação)
// =====================================
app.MapPost("/api/auth/login", async (UsuariosLoginDTO loginDto, IUsuariosServicos usuariosService) =>
{
    try
    {
        var resultado = await usuariosService.Login(loginDto);
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
.WithTags("Autenticação")
.WithOpenApi();

// =====================================
// MICROSERVIÇO DE ESTOQUE (Requer JWT)
// =====================================

// 1. CADASTRO DE PRODUTOS
app.MapPost("/api/estoque/produtos", [Authorize] async (EstoqueCreateDTO createDto, IEstoqueServicos estoqueService) =>
{
    try
    {
        var produto = await estoqueService.CadastrarProduto(createDto);
        return Results.Created($"/api/estoque/produtos/{produto.Id}", new { success = true, data = produto });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { success = false, message = ex.Message });
    }
})
.WithName("CadastrarProduto")
.WithTags("Estoque - Cadastro")
.WithOpenApi()
.RequireAuthorization();

app.MapPut("/api/estoque/produtos/{id:int}", [Authorize] async (int id, EstoqueCreateDTO updateDto, IEstoqueServicos estoqueService) =>
{
    try
    {
        var produto = await estoqueService.AtualizarProduto(id, updateDto);
        return Results.Ok(new { success = true, data = produto });
    }
    catch (ArgumentException)
    {
        return Results.NotFound(new { success = false, message = "Produto não encontrado" });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { success = false, message = ex.Message });
    }
})
.WithName("AtualizarProduto")
.WithTags("Estoque - Cadastro")
.WithOpenApi()
.RequireAuthorization();

app.MapDelete("/api/estoque/produtos/{id:int}", [Authorize] async (int id, IEstoqueServicos estoqueService) =>
{
    try
    {
        var sucesso = await estoqueService.ExcluirProduto(id);
        if (sucesso)
            return Results.Ok(new { success = true, message = "Produto excluído com sucesso" });
        else
            return Results.NotFound(new { success = false, message = "Produto não encontrado" });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { success = false, message = ex.Message });
    }
})
.WithName("ExcluirProduto")
.WithTags("Estoque - Cadastro")
.WithOpenApi()
.RequireAuthorization();

// 2. CONSULTA DE PRODUTOS
app.MapGet("/api/estoque/catalogo", [Authorize] async (IEstoqueServicos estoqueService) =>
{
    try
    {
        var catalogo = await estoqueService.ConsultarCatalogo();
        return Results.Ok(new { success = true, data = catalogo });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { success = false, message = ex.Message });
    }
})
.WithName("ConsultarCatalogo")
.WithTags("Estoque - Consulta")
.WithOpenApi()
.RequireAuthorization();

app.MapGet("/api/estoque/produtos/{id:int}", [Authorize] async (int id, IEstoqueServicos estoqueService) =>
{
    try
    {
        var produto = await estoqueService.ConsultarProdutoPorId(id);
        return Results.Ok(new { success = true, data = produto });
    }
    catch (ArgumentException)
    {
        return Results.NotFound(new { success = false, message = "Produto não encontrado" });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { success = false, message = ex.Message });
    }
})
.WithName("ConsultarProdutoPorId")
.WithTags("Estoque - Consulta")
.WithOpenApi()
.RequireAuthorization();

app.MapGet("/api/estoque/produtos/codigo/{codigoProduto}", [Authorize] async (string codigoProduto, IEstoqueServicos estoqueService) =>
{
    try
    {
        var produto = await estoqueService.ConsultarProdutoPorCodigo(codigoProduto);
        return Results.Ok(new { success = true, data = produto });
    }
    catch (ArgumentException)
    {
        return Results.NotFound(new { success = false, message = "Produto não encontrado" });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { success = false, message = ex.Message });
    }
})
.WithName("ConsultarProdutoPorCodigo")
.WithTags("Estoque - Consulta")
.WithOpenApi()
.RequireAuthorization();

app.MapGet("/api/estoque/quantidade/{codigoProduto}", [Authorize] async (string codigoProduto, IEstoqueServicos estoqueService) =>
{
    try
    {
        var quantidade = await estoqueService.ConsultarQuantidadeDisponivel(codigoProduto);
        return Results.Ok(new { success = true, data = new { codigoProduto, quantidadeDisponivel = quantidade } });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { success = false, message = ex.Message });
    }
})
.WithName("ConsultarQuantidadeDisponivel")
.WithTags("Estoque - Consulta")
.WithOpenApi()
.RequireAuthorization();

// =====================================
// MICROSERVIÇO DE VENDAS (Requer JWT)
// =====================================

// 1. CRIAÇÃO DE PEDIDOS
app.MapPost("/api/vendas/pedidos", [Authorize] async (VendasCreateDTO createDto, IVendasServicos vendasService) =>
{
    try
    {
        var pedido = await vendasService.CriarPedido(createDto);
        return Results.Created($"/api/vendas/pedidos/{pedido.Id}", new { success = true, data = pedido });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { success = false, message = ex.Message });
    }
})
.WithName("CriarPedido")
.WithTags("Vendas - Criação")
.WithOpenApi()
.RequireAuthorization();

app.MapPost("/api/vendas/processar-completa", [Authorize] async (VendasCreateDTO vendaDto, IVendasServicos vendasService) =>
{
    try
    {
        var venda = await vendasService.ProcessarVendaCompleta(vendaDto);
        return Results.Ok(new { success = true, data = venda, message = "Venda processada com sucesso" });
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
.WithName("ProcessarVendaCompleta")
.WithTags("Vendas - Criação")
.WithOpenApi()
.RequireAuthorization();

app.MapPost("/api/vendas/pedidos/{id:int}/confirmar", [Authorize] async (int id, IVendasServicos vendasService) =>
{
    try
    {
        var confirmado = await vendasService.ConfirmarPedido(id);
        if (confirmado)
            return Results.Ok(new { success = true, message = "Pedido confirmado com sucesso" });
        else
            return Results.BadRequest(new { success = false, message = "Não foi possível confirmar o pedido" });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { success = false, message = ex.Message });
    }
})
.WithName("ConfirmarPedido")
.WithTags("Vendas - Criação")
.WithOpenApi()
.RequireAuthorization();

// 2. CONSULTA DE PEDIDOS
app.MapGet("/api/vendas/pedidos", [Authorize] async (IVendasServicos vendasService) =>
{
    try
    {
        var pedidos = await vendasService.ConsultarTodosPedidos();
        return Results.Ok(new { success = true, data = pedidos });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { success = false, message = ex.Message });
    }
})
.WithName("ConsultarTodosPedidos")
.WithTags("Vendas - Consulta")
.WithOpenApi()
.RequireAuthorization();

app.MapGet("/api/vendas/pedidos/{id:int}", [Authorize] async (int id, IVendasServicos vendasService) =>
{
    try
    {
        var pedido = await vendasService.ConsultarPedidoPorId(id);
        return Results.Ok(new { success = true, data = pedido });
    }
    catch (ArgumentException)
    {
        return Results.NotFound(new { success = false, message = "Pedido não encontrado" });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { success = false, message = ex.Message });
    }
})
.WithName("ConsultarPedidoPorId")
.WithTags("Vendas - Consulta")
.WithOpenApi()
.RequireAuthorization();

app.MapGet("/api/vendas/pedidos/cliente/{cliente}", [Authorize] async (string cliente, IVendasServicos vendasService) =>
{
    try
    {
        var pedidos = await vendasService.ConsultarPedidosPorCliente(cliente);
        return Results.Ok(new { success = true, data = pedidos });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { success = false, message = ex.Message });
    }
})
.WithName("ConsultarPedidosPorCliente")
.WithTags("Vendas - Consulta")
.WithOpenApi()
.RequireAuthorization();

app.MapGet("/api/vendas/pedidos/{id:int}/status", [Authorize] async (int id, IVendasServicos vendasService) =>
{
    try
    {
        var status = await vendasService.ConsultarStatusPedido(id);
        return Results.Ok(new { success = true, data = new { pedidoId = id, status } });
    }
    catch (ArgumentException)
    {
        return Results.NotFound(new { success = false, message = "Pedido não encontrado" });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { success = false, message = ex.Message });
    }
})
.WithName("ConsultarStatusPedido")
.WithTags("Vendas - Consulta")
.WithOpenApi()
.RequireAuthorization();

// =====================================
// ENDPOINTS DE VALIDAÇÃO (Úteis para testes)
// =====================================
app.MapGet("/api/estoque/validar/{codigoProduto}/{quantidade:int}", [Authorize] async (string codigoProduto, int quantidade, IEstoqueServicos estoqueService) =>
{
    try
    {
        var disponivel = await estoqueService.ValidarDisponibilidade(codigoProduto, quantidade);
        return Results.Ok(new { 
            success = true, 
            data = new { 
                codigoProduto, 
                quantidadeSolicitada = quantidade, 
                disponivel 
            } 
        });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { success = false, message = ex.Message });
    }
})
.WithName("ValidarDisponibilidadeEstoque")
.WithTags("Utilitários")
.WithOpenApi()
.RequireAuthorization();

app.Run();
