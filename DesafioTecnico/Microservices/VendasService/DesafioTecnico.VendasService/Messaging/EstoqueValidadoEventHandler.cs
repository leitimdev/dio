using DesafioTecnico.VendasService.Services;
using DesafioTecnico.Shared.Events;
using DesafioTecnico.Shared.Messaging;

namespace DesafioTecnico.VendasService.Messaging;

public class EstoqueValidadoEventHandler : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMessageBus _messageBus;

    public EstoqueValidadoEventHandler(IServiceProvider serviceProvider, IMessageBus messageBus)
    {
        _serviceProvider = serviceProvider;
        _messageBus = messageBus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _messageBus.SubscribeAsync<EstoqueValidadoEvent>(HandleEstoqueValidadoAsync);
    }

    private async Task HandleEstoqueValidadoAsync(EstoqueValidadoEvent evento)
    {
        using var scope = _serviceProvider.CreateScope();
        var vendasService = scope.ServiceProvider.GetRequiredService<IVendasService>();

        try
        {
            if (evento.VendaId > 0)
            {
                if (evento.EstoqueDisponivel)
                {
                    // Confirmar a venda se o estoque estiver disponível
                    var success = await vendasService.ConfirmSaleAsync(evento.VendaId);
                    if (success)
                    {
                        Console.WriteLine($"Venda {evento.VendaId} confirmada automaticamente - estoque disponível para produto {evento.CodigoProduto}");
                    }
                }
                else
                {
                    // Cancelar a venda se não houver estoque suficiente
                    var success = await vendasService.CancelSaleAsync(evento.VendaId);
                    if (success)
                    {
                        Console.WriteLine($"Venda {evento.VendaId} cancelada automaticamente - estoque insuficiente para produto {evento.CodigoProduto}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao processar evento de estoque validado: {ex.Message}");
        }
    }
}