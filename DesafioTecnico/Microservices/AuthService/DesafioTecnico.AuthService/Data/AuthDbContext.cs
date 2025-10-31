using Microsoft.EntityFrameworkCore;
using DesafioTecnico.AuthService.Models;

namespace DesafioTecnico.AuthService.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações das entidades
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Senha).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Perfil).IsRequired().HasMaxLength(10);
            });

            // Dados seed
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Email = "adm@desafiotecnico.com",
                    Senha = "123456",
                    Perfil = "Admin"
                }
            );
        }
    }
}