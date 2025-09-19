using DesafioTecnico.EstoqueService.Data;
using DesafioTecnico.EstoqueService.Models;
using DesafioTecnico.Shared.DTOs;
using DesafioTecnico.Shared.Events;
using DesafioTecnico.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

namespace DesafioTecnico.EstoqueService.Services;

public class EstoqueService : IEstoqueService
{
    private readonly EstoqueDbContext _context;
    private readonly IMessageBus _messageBus;

    public EstoqueService(EstoqueDbContext context, IMessageBus messageBus)
    {
        _context = context;
        _messageBus = messageBus;
    }

    public async Task<IEnumerable<EstoqueDTO>> GetAllAsync()
    {
        var estoques = await _context.Estoques.ToListAsync();
        return estoques.Select(e => new EstoqueDTO(
            e.Id,
            e.Id.ToString(), // CodigoProduto como string do Id
            e.Nome, // NomeProduto
            null, // Descricao
            "Geral", // Categoria default
            "UN", // Unidade default
            e.Quantidade, // QuantidadeAtual
            0, // EstoqueMinimo
            1000, // EstoqueMaximo
            null, // CodigoBarras
            true // Ativo
        ));
    }

    public async Task<EstoqueDTO?> GetByIdAsync(int id)
    {
        var estoque = await _context.Estoques.FindAsync(id);
        if (estoque == null) return null;

        return new EstoqueDTO(
            estoque.Id,
            estoque.Id.ToString(),
            estoque.Nome,
            null,
            "Geral",
            "UN",
            estoque.Quantidade,
            0,
            1000,
            null,
            true
        );
    }

    public async Task<EstoqueDTO> CreateAsync(EstoqueDTO estoqueDto)
    {
        var estoque = new Estoque
        {
            Nome = estoqueDto.NomeProduto,
            Preco = 0, // Assumindo preço zero por enquanto, pois não está no DTO
            Quantidade = estoqueDto.QuantidadeAtual,
            DataCriacao = DateTime.UtcNow,
            DataAtualizacao = DateTime.UtcNow
        };

        _context.Estoques.Add(estoque);
        await _context.SaveChangesAsync();

        // Publicar evento de produto criado
        var eventoEstoqueCriado = new EstoqueValidadoEvent(
            Guid.NewGuid(),
            DateTime.UtcNow,
            "EstoqueCriado",
            estoque.Id.ToString(),
            estoque.Quantidade,
            true,
            0 // VendaId 0 para criação
        );

        await _messageBus.PublishAsync(eventoEstoqueCriado);

        return new EstoqueDTO(
            estoque.Id,
            estoque.Id.ToString(),
            estoque.Nome,
            null,
            "Geral",
            "UN",
            estoque.Quantidade,
            0,
            1000,
            null,
            true
        );
    }

    public async Task<EstoqueDTO?> UpdateAsync(int id, EstoqueDTO estoqueDto)
    {
        var estoque = await _context.Estoques.FindAsync(id);
        if (estoque == null) return null;

        estoque.Nome = estoqueDto.NomeProduto;
        estoque.Quantidade = estoqueDto.QuantidadeAtual;
        estoque.DataAtualizacao = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new EstoqueDTO(
            estoque.Id,
            estoque.Id.ToString(),
            estoque.Nome,
            null,
            "Geral",
            "UN",
            estoque.Quantidade,
            0,
            1000,
            null,
            true
        );
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var estoque = await _context.Estoques.FindAsync(id);
        if (estoque == null) return false;

        _context.Estoques.Remove(estoque);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ValidateStockAsync(int id, int quantity)
    {
        var estoque = await _context.Estoques.FindAsync(id);
        if (estoque == null) return false;

        var isValid = estoque.Quantidade >= quantity;

        // Publicar evento de validação de estoque
        var evento = new EstoqueValidadoEvent(
            Guid.NewGuid(),
            DateTime.UtcNow,
            "EstoqueValidado",
            id.ToString(),
            quantity,
            isValid,
            0 // VendaId - será preenchido pelo contexto da venda
        );

        await _messageBus.PublishAsync(evento);
        return isValid;
    }

    public async Task<bool> UpdateStockAsync(int id, int quantity)
    {
        var estoque = await _context.Estoques.FindAsync(id);
        if (estoque == null || estoque.Quantidade < quantity) return false;

        estoque.Quantidade -= quantity;
        estoque.DataAtualizacao = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return true;
    }
}