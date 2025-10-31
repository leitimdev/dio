using DesafioTecnico.Shared.Events;
using DesafioTecnico.Shared.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace DesafioTecnico.VendasService.Messaging;

public class RabbitMQMessageBus : IMessageBus, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _exchangeName = "vendas_exchange";

    public RabbitMQMessageBus()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "guest",
            Password = "guest"
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        // Declarar exchange
        _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Topic, durable: true);
    }

    public async Task PublishAsync<T>(T @event) where T : BaseEvent
    {
        var routingKey = @event.GetType().Name;
        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        var properties = _channel.CreateBasicProperties();
        properties.Persistent = true;
        properties.MessageId = @event.EventId.ToString();
        properties.Timestamp = new AmqpTimestamp(((DateTimeOffset)@event.Timestamp).ToUnixTimeSeconds());

        _channel.BasicPublish(
            exchange: _exchangeName,
            routingKey: routingKey,
            basicProperties: properties,
            body: body);

        await Task.CompletedTask;
    }

    public async Task SubscribeAsync<T>(Func<T, Task> handler) where T : BaseEvent
    {
        var eventType = typeof(T).Name;
        var queueName = $"vendas_{eventType.ToLower()}_queue";
        
        ConfigureQueue(queueName, _exchangeName, eventType);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var @event = JsonSerializer.Deserialize<T>(message);

                if (@event != null)
                {
                    await handler(@event);
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
            }
            catch (Exception ex)
            {
                // Log error and reject message
                Console.WriteLine($"Error processing message: {ex.Message}");
                _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
            }
        };

        _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        await Task.CompletedTask;
    }

    public void ConfigureQueue(string queueName, string exchangeName, string routingKey)
    {
        _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);
        _channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);
    }

    public void Dispose()
    {
        _channel?.Close();
        _channel?.Dispose();
        _connection?.Close();
        _connection?.Dispose();
    }
}