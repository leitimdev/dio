using MinimalAPI.Dominio.DTOs;
using MinimalAPI.Dominio.Entidades;
using MinimalAPI.Dominio.Interfaces;
using MinimalAPI.Infraestrutura.Db;

namespace MinimalAPI.Dominio.Servicos
{
    public class AdministradorServicos : IAdministradorServicos
    {
        private readonly DbContexto _dbContexto;

        public AdministradorServicos(DbContexto db)
        {
            _dbContexto = db;
        }

        public Administrador? Login(LoginDTO loginDTO)
        {
            return _dbContexto.Administradores.FirstOrDefault(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha);
        }
    }
}