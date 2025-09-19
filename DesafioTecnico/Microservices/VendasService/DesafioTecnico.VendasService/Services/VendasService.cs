using DesafioTecnico.VendasService.Data;
using DesafioTecnico.VendasService.Models;
using DesafioTecnico.Shared.DTOs;
using DesafioTecnico.Shared.Events;
using DesafioTecnico.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

namespace DesafioTecnico.VendasService.Services;

public class VendasService : IVendasService
{
    private readonly VendasDbContext _context;
    private readonly IMessageBus _messageBus;

    public VendasService(VendasDbContext context, IMessageBus messageBus)
    {
        _context = context;
        _messageBus = messageBus;
    }

    public async Task<IEnumerable<VendasDTO>> GetAllAsync()
    {
        var vendas = await _context.Vendas.ToListAsync();
        return vendas.Select(v => new VendasDTO(
            v.Id,
            $"VND{v.Id:D6}", // NumeroVenda formatado
            v.CodigoProduto,
            "Produto Padrão", // NomeProduto - seria buscado do EstoqueService em um cenário real
            v.Quantidade,
            v.ValorTotal / v.Quantidade, // PrecoUnitario calculado
            v.Cliente,
            "Sistema", // Vendedor padrão
            v.DataVenda,
            v.Status
        ));
    }

    public async Task<VendasDTO?> GetByIdAsync(int id)
    {
        var venda = await _context.Vendas.FindAsync(id);
        if (venda == null) return null;

        return new VendasDTO(
            venda.Id,
            $"VND{venda.Id:D6}",
            venda.CodigoProduto,
            "Produto Padrão",
            venda.Quantidade,
            venda.ValorTotal / venda.Quantidade,
            venda.Cliente,
            "Sistema",
            venda.DataVenda,
            venda.Status
        );
    }

    public async Task<VendasDTO> CreateAsync(VendasDTO vendaDto)
    {
        var venda = new Venda
        {
            CodigoProduto = vendaDto.CodigoProduto,
            Quantidade = vendaDto.Quantidade,
            Cliente = vendaDto.Cliente,
            ValorTotal = vendaDto.PrecoUnitario * vendaDto.Quantidade,
            Status = "Pendente",
            DataVenda = DateTime.UtcNow,
            DataAtualizacao = DateTime.UtcNow
        };

        _context.Vendas.Add(venda);
        await _context.SaveChangesAsync();

        // Publicar evento de venda criada
        var vendaCriadaEvent = new VendaCriadaEvent(
            Guid.NewGuid(),
            DateTime.UtcNow,
            "VendaCriada",
            venda.Id,
            venda.CodigoProduto,
            venda.Quantidade,
            venda.Cliente
        );

        await _messageBus.PublishAsync(vendaCriadaEvent);

        return new VendasDTO(
            venda.Id,
            $"VND{venda.Id:D6}",
            venda.CodigoProduto,
            "Produto Padrão",
            venda.Quantidade,
            vendaDto.PrecoUnitario,
            venda.Cliente,
            "Sistema",
            venda.DataVenda,
            venda.Status
        );
    }

    public async Task<VendasDTO?> UpdateAsync(int id, VendasDTO vendaDto)
    {
        var venda = await _context.Vendas.FindAsync(id);
        if (venda == null) return null;

        venda.CodigoProduto = vendaDto.CodigoProduto;
        venda.Quantidade = vendaDto.Quantidade;
        venda.Cliente = vendaDto.Cliente;
        venda.ValorTotal = vendaDto.PrecoUnitario * vendaDto.Quantidade;
        venda.DataAtualizacao = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new VendasDTO(
            venda.Id,
            $"VND{venda.Id:D6}",
            venda.CodigoProduto,
            "Produto Padrão",
            venda.Quantidade,
            vendaDto.PrecoUnitario,
            venda.Cliente,
            "Sistema",
            venda.DataVenda,
            venda.Status
        );
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var venda = await _context.Vendas.FindAsync(id);
        if (venda == null) return false;

        _context.Vendas.Remove(venda);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ConfirmSaleAsync(int id)
    {
        var venda = await _context.Vendas.FindAsync(id);
        if (venda == null || venda.Status != "Pendente") return false;

        venda.Status = "Confirmada";
        venda.DataAtualizacao = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // Publicar evento de venda confirmada
        var vendaConfirmadaEvent = new VendaConfirmadaEvent(
            Guid.NewGuid(),
            DateTime.UtcNow,
            "VendaConfirmada",
            venda.Id,
            venda.Status
        );

        await _messageBus.PublishAsync(vendaConfirmadaEvent);
        return true;
    }

    public async Task<bool> CancelSaleAsync(int id)
    {
        var venda = await _context.Vendas.FindAsync(id);
        if (venda == null || venda.Status == "Confirmada") return false;

        venda.Status = "Cancelada";
        venda.DataAtualizacao = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<VendasDTO>> GetByClientAsync(string cliente)
    {
        var vendas = await _context.Vendas
            .Where(v => v.Cliente.Contains(cliente))
            .ToListAsync();

        return vendas.Select(v => new VendasDTO(
            v.Id,
            $"VND{v.Id:D6}",
            v.CodigoProduto,
            "Produto Padrão",
            v.Quantidade,
            v.ValorTotal / v.Quantidade,
            v.Cliente,
            "Sistema",
            v.DataVenda,
            v.Status
        ));
    }

    public async Task<IEnumerable<VendasDTO>> GetByStatusAsync(string status)
    {
        var vendas = await _context.Vendas
            .Where(v => v.Status == status)
            .ToListAsync();

        return vendas.Select(v => new VendasDTO(
            v.Id,
            $"VND{v.Id:D6}",
            v.CodigoProduto,
            "Produto Padrão",
            v.Quantidade,
            v.ValorTotal / v.Quantidade,
            v.Cliente,
            "Sistema",
            v.DataVenda,
            v.Status
        ));
    }
}