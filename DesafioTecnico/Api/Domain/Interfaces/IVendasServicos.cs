using DesafioTecnico.Domain.DTOs;
using DesafioTecnico.Domain.Entities;

namespace DesafioTecnico.Domain.Interfaces
{

    public interface IVendasServicos
    {
        // 1. CRIAÇÃO DE PEDIDOS
        Task<VendasDTO> CriarPedido(VendasCreateDTO createDto);
        Task<bool> ValidarEstoqueParaPedido(string codigoProduto, int quantidade);
        Task<bool> ConfirmarPedido(int pedidoId);

        // 2. CONSULTA DE PEDIDOS
        Task<VendasDTO> ConsultarPedidoPorId(int id);
        Task<IEnumerable<VendasDTO>> ConsultarPedidosPorCliente(string cliente);
        Task<IEnumerable<VendasDTO>> ConsultarTodosPedidos();
        Task<string> ConsultarStatusPedido(int pedidoId);

        // 3. NOTIFICAÇÃO DE VENDA
        Task<bool> NotificarReducaoEstoque(string codigoProduto, int quantidade);
        Task<VendasDTO> ProcessarVendaCompleta(VendasCreateDTO vendaDto);
    }
}
