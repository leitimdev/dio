using System.ComponentModel.DataAnnotations;

namespace DesafioTecnico.VendasService.Models;

public class Venda
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string CodigoProduto { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero")]
    public int Quantidade { get; set; }

    [Required]
    [StringLength(200)]
    public string Cliente { get; set; } = string.Empty;

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Valor total deve ser maior ou igual a zero")]
    public decimal ValorTotal { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Pendente"; // Pendente, Confirmada, Cancelada

    public DateTime DataVenda { get; set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;
}