using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShoop.Domain.Entities;

namespace PetShoop.Infrastructure.EntitiesConfiguration;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(c => c.ClienteId);

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Cpf)
            .IsRequired()
            .HasMaxLength(14);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.Telefone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.DataDeNascimento)
            .IsRequired();

        builder.Property(c => c.Endereco)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.DataDeCadastro)
            .IsRequired();

        builder.HasIndex(c => c.Cpf)
            .IsUnique();

        builder.HasIndex(c => c.Email)
            .IsUnique();
    }
}
