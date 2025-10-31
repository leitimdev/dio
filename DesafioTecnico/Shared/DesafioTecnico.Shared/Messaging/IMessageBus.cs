using DesafioTecnico.Shared.Events;

namespace DesafioTecnico.Shared.Messaging
{
    public interface IMessageBus
    {
        // Publicar eventos
        Task PublishAsync<T>(T @event) where T : BaseEvent;
        
        // Subscrever a eventos
        Task SubscribeAsync<T>(Func<T, Task> handler) where T : BaseEvent;
        
        // Configurar filas
        void ConfigureQueue(string queueName, string exchangeName, string routingKey);
        
        // Fechar conexões
        void Dispose();
    }

    public interface IEventHandler<in T> where T : BaseEvent
    {
        Task HandleAsync(T @event);
    }
}