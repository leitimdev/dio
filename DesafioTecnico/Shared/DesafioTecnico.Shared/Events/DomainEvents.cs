namespace DesafioTecnico.Shared.Events
{
    // Evento base para todos os eventos
    public abstract record BaseEvent(
        Guid EventId,
        DateTime Timestamp,
        string EventType
    );

    // Evento quando uma venda é criada
    public record VendaCriadaEvent(
        Guid EventId,
        DateTime Timestamp,
        string EventType,
        int VendaId,
        string CodigoProduto,
        int Quantidade,
        string Cliente
    ) : BaseEvent(EventId, Timestamp, EventType);

    // Evento para validar estoque
    public record ValidarEstoqueEvent(
        Guid EventId,
        DateTime Timestamp,
        string EventType,
        string CodigoProduto,
        int QuantidadeSolicitada,
        int VendaId
    ) : BaseEvent(EventId, Timestamp, EventType);

    // Evento quando estoque é validado
    public record EstoqueValidadoEvent(
        Guid EventId,
        DateTime Timestamp,
        string EventType,
        string CodigoProduto,
        int QuantidadeSolicitada,
        bool EstoqueDisponivel,
        int VendaId
    ) : BaseEvent(EventId, Timestamp, EventType);

    // Evento para atualizar estoque
    public record AtualizarEstoqueEvent(
        Guid EventId,
        DateTime Timestamp,
        string EventType,
        string CodigoProduto,
        int QuantidadeReduzir,
        int VendaId
    ) : BaseEvent(EventId, Timestamp, EventType);

    // Evento quando estoque é atualizado
    public record EstoqueAtualizadoEvent(
        Guid EventId,
        DateTime Timestamp,
        string EventType,
        string CodigoProduto,
        int QuantidadeAnterior,
        int QuantidadeAtual,
        int VendaId
    ) : BaseEvent(EventId, Timestamp, EventType);

    // Evento quando venda é confirmada
    public record VendaConfirmadaEvent(
        Guid EventId,
        DateTime Timestamp,
        string EventType,
        int VendaId,
        string StatusVenda
    ) : BaseEvent(EventId, Timestamp, EventType);
}