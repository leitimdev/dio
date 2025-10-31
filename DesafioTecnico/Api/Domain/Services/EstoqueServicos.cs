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
    public class EstoqueServicos : IEstoqueServicos
    {
        private readonly DbContexto _context;

        public EstoqueServicos(DbContexto context)
        {
            _context = context;
        }
        // 1. CADASTRO DE PRODUTOS
        public async Task<EstoqueDTO> CadastrarProduto(EstoqueCreateDTO createDto)
        {
            try
            {
                // Verificar se produto já existe
                var produtoExistente = await _context.Estoques
                    .FirstOrDefaultAsync(e => e.CodigoProduto == createDto.CodigoProduto);
                
                if (produtoExistente != null)
                    throw new InvalidOperationException($"Produto com código {createDto.CodigoProduto} já existe.");

                // Criar nova entidade
                var novoEstoque = new Estoque
                {
                    CodigoProduto = createDto.CodigoProduto,
                    NomeProduto = createDto.NomeProduto,
                    Descricao = createDto.Descricao,
                    Categoria = createDto.Categoria,
                    Unidade = createDto.Unidade,
                    QuantidadeAtual = createDto.QuantidadeAtual,
                    EstoqueMinimo = createDto.EstoqueMinimo,
                    EstoqueMaximo = createDto.EstoqueMaximo,
                    CodigoBarras = createDto.CodigoBarras,
                    Ativo = createDto.Ativo
                };

                _context.Estoques.Add(novoEstoque);
                await _context.SaveChangesAsync();

                return MapearParaDTO(novoEstoque);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar produto: {ex.Message}", ex);
            }
        }

        public async Task<EstoqueDTO> AtualizarProduto(int id, EstoqueCreateDTO updateDto)
        {
            try
            {
                var estoque = await _context.Estoques.FindAsync(id);
                
                if (estoque == null)
                    throw new ArgumentException($"Produto com ID {id} não encontrado.");

                // Verificar se não há conflito de código
                var conflito = await _context.Estoques
                    .AnyAsync(e => e.CodigoProduto == updateDto.CodigoProduto && e.Id != id);
                
                if (conflito)
                    throw new InvalidOperationException($"Código {updateDto.CodigoProduto} já está em uso por outro produto.");

                // Atualizar dados
                estoque.CodigoProduto = updateDto.CodigoProduto;
                estoque.NomeProduto = updateDto.NomeProduto;
                estoque.Descricao = updateDto.Descricao;
                estoque.Categoria = updateDto.Categoria;
                estoque.Unidade = updateDto.Unidade;
                estoque.QuantidadeAtual = updateDto.QuantidadeAtual;
                estoque.EstoqueMinimo = updateDto.EstoqueMinimo;
                estoque.EstoqueMaximo = updateDto.EstoqueMaximo;
                estoque.CodigoBarras = updateDto.CodigoBarras;
                estoque.Ativo = updateDto.Ativo;

                await _context.SaveChangesAsync();

                return MapearParaDTO(estoque);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar produto: {ex.Message}", ex);
            }
        }

        public async Task<bool> ExcluirProduto(int id)
        {
            try
            {
                var estoque = await _context.Estoques.FindAsync(id);
                
                if (estoque == null)
                    return false;

                _context.Estoques.Remove(estoque);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir produto: {ex.Message}", ex);
            }
        }

        // 2. CONSULTA DE PRODUTOS
        public async Task<IEnumerable<EstoqueDTO>> ConsultarCatalogo()
        {
            try
            {
                var estoques = await _context.Estoques
                    .Where(e => e.Ativo)
                    .OrderBy(e => e.NomeProduto)
                    .ToListAsync();

                return estoques.Select(MapearParaDTO);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar catálogo: {ex.Message}", ex);
            }
        }

        public async Task<EstoqueDTO> ConsultarProdutoPorId(int id)
        {
            try
            {
                var estoque = await _context.Estoques.FindAsync(id);
                
                if (estoque == null)
                    throw new ArgumentException($"Produto com ID {id} não encontrado.");

                return MapearParaDTO(estoque);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar produto por ID: {ex.Message}", ex);
            }
        }

        public async Task<EstoqueDTO> ConsultarProdutoPorCodigo(string codigoProduto)
        {
            try
            {
                var estoque = await _context.Estoques
                    .FirstOrDefaultAsync(e => e.CodigoProduto == codigoProduto);
                
                if (estoque == null)
                    throw new ArgumentException($"Produto com código {codigoProduto} não encontrado.");

                return MapearParaDTO(estoque);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar produto por código: {ex.Message}", ex);
            }
        }

        public async Task<int> ConsultarQuantidadeDisponivel(string codigoProduto)
        {
            try
            {
                var estoque = await _context.Estoques
                    .FirstOrDefaultAsync(e => e.CodigoProduto == codigoProduto && e.Ativo);
                
                return estoque?.QuantidadeAtual ?? 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar quantidade disponível: {ex.Message}", ex);
            }
        }

        // 3. ATUALIZAÇÃO DE ESTOQUE (Integração com Vendas)
        public async Task<bool> AtualizarEstoqueVenda(string codigoProduto, int quantidadeVendida)
        {
            try
            {
                var estoque = await _context.Estoques
                    .FirstOrDefaultAsync(e => e.CodigoProduto == codigoProduto && e.Ativo);
                
                if (estoque == null)
                    throw new ArgumentException($"Produto com código {codigoProduto} não encontrado.");

                if (estoque.QuantidadeAtual < quantidadeVendida)
                    throw new InvalidOperationException($"Quantidade insuficiente em estoque. Disponível: {estoque.QuantidadeAtual}, Necessário: {quantidadeVendida}");

                // Reduzir quantidade do estoque
                estoque.QuantidadeAtual -= quantidadeVendida;
                
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar estoque para venda: {ex.Message}", ex);
            }
        }

        public async Task<bool> ValidarDisponibilidade(string codigoProduto, int quantidadeNecessaria)
        {
            try
            {
                var quantidadeDisponivel = await ConsultarQuantidadeDisponivel(codigoProduto);
                
                return quantidadeDisponivel >= quantidadeNecessaria;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao validar disponibilidade: {ex.Message}", ex);
            }
        }

        // Método auxiliar de mapeamento
        private static EstoqueDTO MapearParaDTO(Estoque estoque)
        {
            return new EstoqueDTO(
                estoque.Id,
                estoque.CodigoProduto,
                estoque.NomeProduto,
                estoque.Descricao,
                estoque.Categoria,
                estoque.Unidade,
                estoque.QuantidadeAtual,
                estoque.EstoqueMinimo,
                estoque.EstoqueMaximo,
                estoque.CodigoBarras,
                estoque.Ativo,
                estoque.EstoqueBaixo,
                estoque.EstoqueEsgotado
            );
        }
    }
}