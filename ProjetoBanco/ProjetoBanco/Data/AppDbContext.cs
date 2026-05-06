using Microsoft.EntityFrameworkCore;
using ProjetoBanco.API.Models;

namespace ProjetoBanco.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<PessoaFisica> PessoasFisicas { get; set; }
    public DbSet<PessoaJuridica> PessoasJuridicas { get; set; }
    public DbSet<Agencia> Agencias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Emprestimo> Emprestimos { get; set; }
    public DbSet<Contratacao> Contratacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Herança por discriminator (TPH)
        modelBuilder.Entity<Cliente>()
            .HasDiscriminator<string>("TipoCliente")
            .HasValue<PessoaFisica>("PF")
            .HasValue<PessoaJuridica>("PJ");

        modelBuilder.Entity<Produto>()
            .HasDiscriminator<string>("TipoProduto")
            .HasValue<Emprestimo>("EMPRESTIMO");

        // Seed de produto
        modelBuilder.Entity<Emprestimo>().HasData(new Emprestimo
        {
            Id = 1,
            Nome = "Empréstimo Pessoal",
            Descricao = "Empréstimo pessoal com análise de crédito",
            TipoProduto = "EMPRESTIMO",
            ValorMaximo = 50000,
            TaxaJurosAnual = 18.5m,
            PrazoMaximoMeses = 60
        });
    }
}