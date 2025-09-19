namespace DesafioTecnico.Domain.DTOs
{
    // DTO para consulta de vendas
    public record VendasDTO(
        int Id,
        string NumeroVenda,
        string CodigoProduto,
        string NomeProduto,
        int Quantidade,
        decimal PrecoUnitario,
        string Cliente,
        string Vendedor,
        DateTime DataVenda,
        string StatusVenda,
        decimal ValorTotal,
        bool VendaCancelada,
        bool VendaPendente
    );

    // DTO para criação de venda
    public record VendasCreateDTO(
        string NumeroVenda,
        string CodigoProduto,
        string NomeProduto,
        int Quantidade,
        decimal PrecoUnitario,
        string Cliente,
        string Vendedor,
        string StatusVenda = "Concluída"
    );

    // DTO para atualização de status de venda
    public record VendasUpdateStatusDTO(
        int Id,
        string StatusVenda
    );

    // DTO para relatório de vendas
    public record VendasRelatorioDTO(
        string Cliente,
        string Vendedor,
        DateTime DataInicio,
        DateTime DataFim,
        decimal ValorTotal,
        int TotalItens
    );
}
