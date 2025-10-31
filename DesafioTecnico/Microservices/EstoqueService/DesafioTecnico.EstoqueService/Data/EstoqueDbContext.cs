using DesafioTecnico.EstoqueService.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioTecnico.EstoqueService.Data;

public class EstoqueDbContext : DbContext
{
    public EstoqueDbContext(DbContextOptions<EstoqueDbContext> options) : base(options)
    {
    }

    public DbSet<Estoque> Estoques { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurações específicas do modelo Estoque
        modelBuilder.Entity<Estoque>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Preco).HasPrecision(18, 2);
            entity.HasIndex(e => e.Nome);
        });

        // Dados iniciais para teste
        modelBuilder.Entity<Estoque>().HasData(
            new Estoque { Id = 1, Nome = "Produto A", Preco = 10.50m, Quantidade = 100 },
            new Estoque { Id = 2, Nome = "Produto B", Preco = 25.75m, Quantidade = 50 },
            new Estoque { Id = 3, Nome = "Produto C", Preco = 15.30m, Quantidade = 75 }
        );
    }
}