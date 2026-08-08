using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShoop.Domain.Entities;

namespace PetShoop.Infrastructure.EntitiesConfiguration;

public class ServicoConfiguration : IEntityTypeConfiguration<Servico>
{
    public void Configure(EntityTypeBuilder<Servico> builder)
    {
        builder.HasKey(s => s.ServicoId);

        builder.Property(s => s.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Descricao)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(s => s.Preco)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(s => s.DuracaoEmMinutos)
            .IsRequired();
    }
}
