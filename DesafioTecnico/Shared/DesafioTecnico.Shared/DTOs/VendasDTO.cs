namespace DesafioTecnico.Shared.DTOs
{
    // DTO para resposta de vendas
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
        string? StatusVenda
    );

    // DTO para criação de venda
    public record VendasCreateDTO(
        string CodigoProduto,
        string NomeProduto,
        int Quantidade,
        decimal PrecoUnitario,
        string Cliente,
        string Vendedor
    );

    // DTO para atualização de venda
    public record VendasUpdateDTO(
        int Id,
        string NumeroVenda,
        string CodigoProduto,
        string NomeProduto,
        int Quantidade,
        decimal PrecoUnitario,
        string Cliente,
        string Vendedor,
        string? StatusVenda
    );
}