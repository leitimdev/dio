using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MinimalAPI.Dominio.Enuns;

namespace MinimalAPI.Dominio.DTOs
{
    public class AdministradorDTO
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public Perfil? Perfil { get; set; }
    }
}