using System.ComponentModel.DataAnnotations;

namespace DesafioTecnico.Domain.Entities
{
    public class Estoque
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string CodigoProduto { get; set; } = string.Empty;
        
        [Required]
        [StringLength(200)]
        public string NomeProduto { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Descricao { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Categoria { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string Unidade { get; set; } = string.Empty; // Ex: UN, KG, L, M, etc.
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantidade deve ser maior ou igual a zero")]
        public int QuantidadeAtual { get; set; }
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Estoque mínimo deve ser maior ou igual a zero")]
        public int EstoqueMinimo { get; set; }
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Estoque máximo deve ser maior ou igual a zero")]
        public int EstoqueMaximo { get; set; }
        
        [StringLength(50)]
        public string CodigoBarras { get; set; } = string.Empty;
        
        public bool Ativo { get; set; } = true;
                
        public bool EstoqueBaixo => QuantidadeAtual <= EstoqueMinimo;
        
        public bool EstoqueEsgotado => QuantidadeAtual == 0;
    }
}
