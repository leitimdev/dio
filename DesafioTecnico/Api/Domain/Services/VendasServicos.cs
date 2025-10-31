using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DesafioTecnico.Domain.DTOs;
using DesafioTecnico.Domain.Entities;
using DesafioTecnico.Domain.Interfaces;
using DesafioTecnico.Infraestrutura.Db;

namespace DesafioTecnico.Domain.Services
{
    public class VendasServicos : IVendasServicos
    {
        private readonly DbContexto _context;
        private readonly IEstoqueServicos _estoqueServicos;

        public VendasServicos(DbContexto context, IEstoqueServicos estoqueServicos)
        {
            _context = context;
            _estoqueServicos = estoqueServicos;
        }
        // 1. CRIAÇÃO DE PEDIDOS
        public async Task<VendasDTO> CriarPedido(VendasCreateDTO createDto)
        {
            try
            {
                // Verificar se número da venda já existe
                var vendaExistente = await _context.Vendas
                    .FirstOrDefaultAsync(v => v.NumeroVenda == createDto.NumeroVenda);

                if (vendaExistente != null)
                    throw new InvalidOperationException($"Número de venda {createDto.NumeroVenda} já existe.");

                // Criar nova venda com status "Pendente" inicialmente
                var novaVenda = new Vendas
                {
                    NumeroVenda = createDto.NumeroVenda,
                    CodigoProduto = createDto.CodigoProduto,
                    NomeProduto = createDto.NomeProduto,
                    Quantidade = createDto.Quantidade,
                    PrecoUnitario = createDto.PrecoUnitario,
                    Cliente = createDto.Cliente,
                    Vendedor = createDto.Vendedor,
                    DataVenda = DateTime.Now,
                    StatusVenda = "Pendente" // Criado como pendente até validação
                };

                _context.Vendas.Add(novaVenda);
                await _context.SaveChangesAsync();

                return MapearParaDTO(novaVenda);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar pedido: {ex.Message}", ex);
            }
        }

        public async Task<bool> ValidarEstoqueParaPedido(string codigoProduto, int quantidade)
        {
            try
            {
                // Chamar serviço de estoque para validar disponibilidade
                return await _estoqueServicos.ValidarDisponibilidade(codigoProduto, quantidade);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao validar estoque: {ex.Message}", ex);
            }
        }

        public async Task<bool> ConfirmarPedido(int pedidoId)
        {
            try
            {
                var venda = await _context.Vendas.FindAsync(pedidoId);

                if (venda == null)
                    throw new ArgumentException($"Pedido com ID {pedidoId} não encontrado.");

                if (venda.StatusVenda != "Pendente")
                    throw new InvalidOperationException($"Pedido já foi processado. Status atual: {venda.StatusVenda}");

                // Validar estoque novamente antes de confirmar
                var estoqueDisponivel = await _estoqueServicos.ValidarDisponibilidade(venda.CodigoProduto, venda.Quantidade);

                if (!estoqueDisponivel)
                {
                    venda.StatusVenda = "Cancelada";
                    await _context.SaveChangesAsync();
                    throw new InvalidOperationException("Estoque insuficiente para confirmar o pedido.");
                }

                // Confirmar pedido
                venda.StatusVenda = "Concluída";
                await _context.SaveChangesAsync();

                // Notificar redução do estoque
                await NotificarReducaoEstoque(venda.CodigoProduto, venda.Quantidade);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao confirmar pedido: {ex.Message}", ex);
            }
        }

        // 2. CONSULTA DE PEDIDOS
        public async Task<VendasDTO> ConsultarPedidoPorId(int id)
        {
            try
            {
                var venda = await _context.Vendas.FindAsync(id);

                if (venda == null)
                    throw new ArgumentException($"Pedido com ID {id} não encontrado.");

                return MapearParaDTO(venda);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar pedido por ID: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<VendasDTO>> ConsultarPedidosPorCliente(string cliente)
        {
            try
            {
                var vendas = await _context.Vendas
                    .Where(v => v.Cliente.Contains(cliente))
                    .OrderByDescending(v => v.DataVenda)
                    .ToListAsync();

                return vendas.Select(MapearParaDTO);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar pedidos por cliente: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<VendasDTO>> ConsultarTodosPedidos()
        {
            try
            {
                var vendas = await _context.Vendas
                    .OrderByDescending(v => v.DataVenda)
                    .ToListAsync();

                return vendas.Select(MapearParaDTO);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar todos os pedidos: {ex.Message}", ex);
            }
        }

        public async Task<string> ConsultarStatusPedido(int pedidoId)
        {
            try
            {
                var venda = await _context.Vendas.FindAsync(pedidoId);

                if (venda == null)
                    throw new ArgumentException($"Pedido com ID {pedidoId} não encontrado.");

                return venda.StatusVenda;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar status do pedido: {ex.Message}", ex);
            }
        }

        // 3. NOTIFICAÇÃO DE VENDA
        public async Task<bool> NotificarReducaoEstoque(string codigoProduto, int quantidade)
        {
            try
            {
                // Chamar serviço de estoque para atualizar quantidade após venda
                return await _estoqueServicos.AtualizarEstoqueVenda(codigoProduto, quantidade);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao notificar redução de estoque: {ex.Message}", ex);
            }
        }

        public async Task<VendasDTO> ProcessarVendaCompleta(VendasCreateDTO vendaDto)
        {
            try
            {
                // 1. Validar estoque
                var estoqueDisponivel = await ValidarEstoqueParaPedido(vendaDto.CodigoProduto, vendaDto.Quantidade);

                if (!estoqueDisponivel)
                    throw new InvalidOperationException($"Estoque insuficiente para o produto {vendaDto.CodigoProduto}. Quantidade solicitada: {vendaDto.Quantidade}");

                // 2. Criar pedido
                var novoPedido = await CriarPedido(vendaDto);

                // 3. Confirmar pedido (que automaticamente notifica o estoque)
                var confirmado = await ConfirmarPedido(novoPedido.Id);

                if (!confirmado)
                    throw new Exception("Erro ao confirmar o pedido.");

                // 4. Retornar pedido atualizado
                return await ConsultarPedidoPorId(novoPedido.Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao processar venda completa: {ex.Message}", ex);
            }
        }

        // Método auxiliar de mapeamento
        private static VendasDTO MapearParaDTO(Vendas venda)
        {
            return new VendasDTO(
                venda.Id,
                venda.NumeroVenda,
                venda.CodigoProduto,
                venda.NomeProduto,
                venda.Quantidade,
                venda.PrecoUnitario,
                venda.Cliente,
                venda.Vendedor,
                venda.DataVenda,
                venda.StatusVenda,
                venda.ValorTotal,
                venda.VendaCancelada,
                venda.VendaPendente
            );
        }
    }
}