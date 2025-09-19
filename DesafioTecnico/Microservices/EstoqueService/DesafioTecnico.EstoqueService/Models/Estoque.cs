using System.ComponentModel.DataAnnotations;

namespace DesafioTecnico.EstoqueService.Models;

public class Estoque
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Preço deve ser maior ou igual a zero")]
    public decimal Preco { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Quantidade deve ser maior ou igual a zero")]
    public int Quantidade { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;
}