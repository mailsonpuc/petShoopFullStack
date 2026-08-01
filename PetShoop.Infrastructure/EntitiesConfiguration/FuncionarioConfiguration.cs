using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShoop.Domain.Entities;

namespace PetShoop.Infrastructure.EntitiesConfiguration;

public class FuncionarioConfiguration : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> builder)
    {
        builder.HasKey(f => f.FuncionarioId);

        builder.Property(f => f.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(f => f.Cpf)
            .IsRequired()
            .HasMaxLength(14);

        builder.Property(f => f.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(f => f.Telefone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(f => f.Cargo)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(f => f.Salario)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(f => f.DataAdmissao)
            .IsRequired();

        builder.HasIndex(f => f.Cpf)
            .IsUnique();

        builder.HasIndex(f => f.Email)
            .IsUnique();
    }
}
