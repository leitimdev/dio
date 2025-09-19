using System.ComponentModel.DataAnnotations;

namespace DesafioTecnico.Domain.Entities
{
    public class Vendas
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(20)]
        public string NumeroVenda { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string CodigoProduto { get; set; } = string.Empty;
        
        [Required]
        [StringLength(200)]
        public string NomeProduto { get; set; } = string.Empty;
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero")]
        public int Quantidade { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Preço unitário deve ser maior que zero")]
        public decimal PrecoUnitario { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Cliente { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Vendedor { get; set; } = string.Empty;
        
        public DateTime DataVenda { get; set; } = DateTime.Now;
        
        [StringLength(20)]
        public string StatusVenda { get; set; } = "Concluída"; // Concluída, Cancelada, Pendente
        
        // Propriedades calculadas
        public decimal ValorTotal => Quantidade * PrecoUnitario;
        
        public bool VendaCancelada => StatusVenda == "Cancelada";
        
        public bool VendaPendente => StatusVenda == "Pendente";
    }
}
