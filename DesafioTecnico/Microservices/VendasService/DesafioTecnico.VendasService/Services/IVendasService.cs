using DesafioTecnico.VendasService.Models;
using DesafioTecnico.Shared.DTOs;

namespace DesafioTecnico.VendasService.Services;

public interface IVendasService
{
    Task<IEnumerable<VendasDTO>> GetAllAsync();
    Task<VendasDTO?> GetByIdAsync(int id);
    Task<VendasDTO> CreateAsync(VendasDTO vendaDto);
    Task<VendasDTO?> UpdateAsync(int id, VendasDTO vendaDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ConfirmSaleAsync(int id);
    Task<bool> CancelSaleAsync(int id);
    Task<IEnumerable<VendasDTO>> GetByClientAsync(string cliente);
    Task<IEnumerable<VendasDTO>> GetByStatusAsync(string status);
}