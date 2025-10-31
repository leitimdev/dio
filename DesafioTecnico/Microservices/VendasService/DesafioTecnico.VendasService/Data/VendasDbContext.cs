using DesafioTecnico.VendasService.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioTecnico.VendasService.Data;

public class VendasDbContext : DbContext
{
    public VendasDbContext(DbContextOptions<VendasDbContext> options) : base(options)
    {
    }

    public DbSet<Venda> Vendas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurações específicas do modelo Venda
        modelBuilder.Entity<Venda>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.CodigoProduto).IsRequired().HasMaxLength(50);
            entity.Property(v => v.Cliente).IsRequired().HasMaxLength(200);
            entity.Property(v => v.Status).IsRequired().HasMaxLength(20);
            entity.Property(v => v.ValorTotal).HasPrecision(18, 2);
            entity.HasIndex(v => v.CodigoProduto);
            entity.HasIndex(v => v.Cliente);
            entity.HasIndex(v => v.Status);
        });

        // Dados iniciais para teste
        modelBuilder.Entity<Venda>().HasData(
            new Venda 
            { 
                Id = 1, 
                CodigoProduto = "1", 
                Quantidade = 2, 
                Cliente = "Cliente Teste 1", 
                ValorTotal = 21.00m, 
                Status = "Confirmada",
                DataVenda = DateTime.UtcNow.AddDays(-1)
            },
            new Venda 
            { 
                Id = 2, 
                CodigoProduto = "2", 
                Quantidade = 1, 
                Cliente = "Cliente Teste 2", 
                ValorTotal = 25.75m, 
                Status = "Pendente",
                DataVenda = DateTime.UtcNow
            }
        );
    }
}