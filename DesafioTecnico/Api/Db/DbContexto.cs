using Microsoft.EntityFrameworkCore;
using DesafioTecnico.Domain.Entities;

namespace DesafioTecnico.Infraestrutura.Db
{
    public class DbContexto : DbContext
    {
        public DbContexto() { }
        
        public DbContexto(DbContextOptions<DbContexto> options) : base(options)
        {
        }

        public DbSet<Usuarios> Usuarios { get; set; } = default!;
        public DbSet<Estoque> Estoques { get; set; } = default!;
        public DbSet<Vendas> Vendas { get; set; } = default!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Usuário padrão com senha em texto simples
            modelBuilder.Entity<Usuarios>().HasData(
                new Usuarios
                {
                    Id = 1,
                    Email = "adm@desafiotecnico.com",
                    Senha = "123456", // Senha em texto simples
                    Perfil = "Admin"
                }
            );
        }           
    }
}