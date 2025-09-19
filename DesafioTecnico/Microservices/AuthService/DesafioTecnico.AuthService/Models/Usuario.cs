using System.ComponentModel.DataAnnotations;

namespace DesafioTecnico.AuthService.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string Senha { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(10)]
        public string Perfil { get; set; } = string.Empty;
    }
}