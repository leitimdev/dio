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

        public Administrador? BuscaPorID(int? pagina)
        {
            return _dbContexto.Administradores.Where(a => a.Id == pagina).FirstOrDefault();
        }

        public Administrador Incluir(Administrador administrador)
        {
            _dbContexto.Administradores.Add(administrador);
            _dbContexto.SaveChanges();
            return administrador;

        }

        public Administrador? Login(LoginDTO loginDTO)
        {
            return _dbContexto.Administradores.FirstOrDefault(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha);
        }

        public List<Administrador> Todos(int? pagina)
        {
            int pageSize = 10;
            var query = _dbContexto.Administradores.AsQueryable();

            if(pagina != null)
            {
                query = query.Skip((pagina.Value - 1) * pageSize).Take(pageSize);
            }

            return query.ToList();
        }
    }
}