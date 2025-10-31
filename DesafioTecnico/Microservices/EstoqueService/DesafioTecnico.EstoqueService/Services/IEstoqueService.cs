using DesafioTecnico.EstoqueService.Models;
using DesafioTecnico.Shared.DTOs;

namespace DesafioTecnico.EstoqueService.Services;

public interface IEstoqueService
{
    Task<IEnumerable<EstoqueDTO>> GetAllAsync();
    Task<EstoqueDTO?> GetByIdAsync(int id);
    Task<EstoqueDTO> CreateAsync(EstoqueDTO estoqueDto);
    Task<EstoqueDTO?> UpdateAsync(int id, EstoqueDTO estoqueDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ValidateStockAsync(int id, int quantity);
    Task<bool> UpdateStockAsync(int id, int quantity);
}