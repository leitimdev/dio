using DesafioTecnico.Domain.DTOs;
using DesafioTecnico.Domain.Entities;

namespace DesafioTecnico.Domain.Interfaces
{
    /// Regras de negócio: Cadastro de Produtos, Consulta de Produtos, Atualização de Estoque

    public interface IEstoqueServicos
    {
        // 1. CADASTRO DE PRODUTOS
        Task<EstoqueDTO> CadastrarProduto(EstoqueCreateDTO createDto);
        Task<EstoqueDTO> AtualizarProduto(int id, EstoqueCreateDTO updateDto);        
        Task<bool> ExcluirProduto(int id);

        // 2. CONSULTA DE PRODUTOS
        Task<IEnumerable<EstoqueDTO>> ConsultarCatalogo();
        Task<EstoqueDTO> ConsultarProdutoPorId(int id);
        Task<EstoqueDTO> ConsultarProdutoPorCodigo(string codigoProduto);
        Task<int> ConsultarQuantidadeDisponivel(string codigoProduto);

        // 3. ATUALIZAÇÃO DE ESTOQUE (Integração com Vendas)
        Task<bool> AtualizarEstoqueVenda(string codigoProduto, int quantidadeVendida);
        Task<bool> ValidarDisponibilidade(string codigoProduto, int quantidadeNecessaria);
    }
}
