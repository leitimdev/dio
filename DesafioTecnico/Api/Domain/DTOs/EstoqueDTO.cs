namespace DesafioTecnico.Domain.DTOs
{
    // DTO para consulta de estoque
    public record EstoqueDTO(
        int Id,
        string CodigoProduto,
        string NomeProduto,
        string Descricao,
        string Categoria,
        string Unidade,
        int QuantidadeAtual,
        int EstoqueMinimo,
        int EstoqueMaximo,
        string CodigoBarras,
        bool Ativo,
        bool EstoqueBaixo,
        bool EstoqueEsgotado
    );

    // DTO para criação/atualização de estoque
    public record EstoqueCreateDTO(
        string CodigoProduto,
        string NomeProduto,
        string Descricao,
        string Categoria,
        string Unidade,
        int QuantidadeAtual,
        int EstoqueMinimo,
        int EstoqueMaximo,
        string CodigoBarras,
        bool Ativo = true
    );

    // DTO para atualização de quantidade
    public record EstoqueUpdateQuantidadeDTO(
        int Id,
        int NovaQuantidade
    );
}
