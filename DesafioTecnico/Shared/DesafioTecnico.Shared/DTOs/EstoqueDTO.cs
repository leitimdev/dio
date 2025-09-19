namespace DesafioTecnico.Shared.DTOs
{
    // DTO para resposta de produtos
    public record EstoqueDTO(
        int Id,
        string CodigoProduto,
        string NomeProduto,
        string? Descricao,
        string Categoria,
        string Unidade,
        int QuantidadeAtual,
        int EstoqueMinimo,
        int EstoqueMaximo,
        string? CodigoBarras,
        bool Ativo
    );

    // DTO para criação/atualização de produto
    public record EstoqueCreateDTO(
        string CodigoProduto,
        string NomeProduto,
        string? Descricao,
        string Categoria,
        string Unidade,
        int QuantidadeAtual,
        int EstoqueMinimo,
        int EstoqueMaximo,
        string? CodigoBarras,
        bool Ativo = true
    );

    // DTO para atualização de estoque
    public record EstoqueUpdateDTO(
        int Id,
        string CodigoProduto,
        string NomeProduto,
        string? Descricao,
        string Categoria,
        string Unidade,
        int QuantidadeAtual,
        int EstoqueMinimo,
        int EstoqueMaximo,
        string? CodigoBarras,
        bool Ativo
    );
}