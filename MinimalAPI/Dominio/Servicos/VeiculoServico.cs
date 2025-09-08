using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MinimalAPI.Dominio.Entidades;
using MinimalAPI.Dominio.Interfaces;
using MinimalAPI.Infraestrutura.Db;

namespace MinimalAPI.Dominio.Servicos
{
    public class VeiculoServico : IVeiculoServicos
    {
        private readonly DbContexto _dbContexto;

        public Veiculo? BuscaPorID(int id)
        {
            return _dbContexto.Veiculos.Where(v => v.Id == id).FirstOrDefault();
        }

        public void Incluir(Veiculo veiculo)
        {
            _dbContexto.Veiculos.Add(veiculo);
            _dbContexto.SaveChanges();
        }

        public void Alterar(Veiculo veiculo)
        {
            _dbContexto.Veiculos.Update(veiculo);
            _dbContexto.SaveChanges();
        }

        public void Excluir(Veiculo veiculo)
        {
            _dbContexto.Veiculos.Remove(veiculo);
            _dbContexto.SaveChanges();
        }
        
        public List<Veiculo> Todos(int pagina = 1, string? nome = null, string? marca = null)
        {
            int pageSize = 10;
            var query = _dbContexto.Veiculos.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(v => v.Nome.Contains(nome));
            }

            if (!string.IsNullOrEmpty(marca))
            {
                query = query.Where(v => v.Marca.Contains(marca));
            }

            return query.Skip((pagina - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}