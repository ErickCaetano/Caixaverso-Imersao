namespace DesafioPerfildeRisco.DbContext;
using DesafioPerfildeRisco.Models;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{

    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Simulacao> Simulacoes => Set<Simulacao>();
    public DbSet<Investidor> Investidores => Set<Investidor>();
    
    public DbSet<Telemetria> Telemetrias => Set<Telemetria>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Produto>().HasKey(p => p.IdProduto);
        modelBuilder.Entity<Simulacao>().HasKey(s => s.IdSimulacao);
        modelBuilder.Entity<Investidor>().HasKey(i => i.IdCliente);        
        modelBuilder.Entity<Telemetria>().HasKey(t => t.Id);
        

        modelBuilder.Entity<Simulacao>()
            .HasOne(s => s.Produto)
            .WithMany();
    }
}