
using Microsoft.EntityFrameworkCore;
using PetShoop.Domain.Entities;

namespace PetShoop.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }


    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }
    public DbSet<Consulta> Consultas { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<ItemVenda> ItensVendas { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Prontuario> Prontuarios { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Servico> Servicos { get; set; }
    public DbSet<Vacina> Vacinas { get; set; }
    public DbSet<Venda> Vendas { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext)
            .Assembly);
    }

}
