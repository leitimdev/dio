using DesafioTecnico.EstoqueService.Services;
using DesafioTecnico.Shared.Events;
using DesafioTecnico.Shared.Messaging;

namespace DesafioTecnico.EstoqueService.Messaging;

public class VendaCriadaEventHandler : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMessageBus _messageBus;

    public VendaCriadaEventHandler(IServiceProvider serviceProvider, IMessageBus messageBus)
    {
        _serviceProvider = serviceProvider;
        _messageBus = messageBus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _messageBus.SubscribeAsync<VendaCriadaEvent>(HandleVendaCriadaAsync);
    }

    private async Task HandleVendaCriadaAsync(VendaCriadaEvent evento)
    {
        using var scope = _serviceProvider.CreateScope();
        var estoqueService = scope.ServiceProvider.GetRequiredService<IEstoqueService>();

        try
        {
            // Buscar produto pelo código - assumindo que o código é o Id para simplificar
            // Em um cenário real, teria um mapeamento entre código e ID
            if (int.TryParse(evento.CodigoProduto, out int productId))
            {
                var stockValid = await estoqueService.ValidateStockAsync(productId, evento.Quantidade);
                
                if (stockValid)
                {
                    await estoqueService.UpdateStockAsync(productId, evento.Quantidade);
                    Console.WriteLine($"Estoque atualizado para produto {evento.CodigoProduto}: -{evento.Quantidade}");

                    // Publicar evento de estoque validado
                    var estoqueValidado = new EstoqueValidadoEvent(
                        Guid.NewGuid(),
                        DateTime.UtcNow,
                        "EstoqueValidado",
                        evento.CodigoProduto,
                        evento.Quantidade,
                        true,
                        evento.VendaId
                    );
                    
                    await _messageBus.PublishAsync(estoqueValidado);
                }
                else
                {
                    Console.WriteLine($"Estoque insuficiente para produto {evento.CodigoProduto}");
                    
                    // Publicar evento de estoque insuficiente
                    var estoqueInsuficiente = new EstoqueValidadoEvent(
                        Guid.NewGuid(),
                        DateTime.UtcNow,
                        "EstoqueInsuficiente",
                        evento.CodigoProduto,
                        evento.Quantidade,
                        false,
                        evento.VendaId
                    );
                    
                    await _messageBus.PublishAsync(estoqueInsuficiente);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao processar venda criada: {ex.Message}");
        }
    }
}