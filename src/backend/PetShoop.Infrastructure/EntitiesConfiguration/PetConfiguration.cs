using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShoop.Domain.Entities;

namespace PetShoop.Infrastructure.EntitiesConfiguration;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.HasKey(p => p.PetId);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Especie)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(p => p.Raca)
            .IsRequired()
            .HasMaxLength(80);

        builder.Property(p => p.Sexo)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(p => p.DataDeNascimento)
            .IsRequired();

        builder.Property(p => p.Peso)
            .IsRequired()
            .HasPrecision(6, 2);

        builder.Property(p => p.Cor)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Porte)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(p => p.Observacoes)
            .HasMaxLength(500);

        builder.Property(p => p.ClienteId)
            .IsRequired();

        builder.HasOne<Cliente>()
            .WithMany()
            .HasForeignKey(p => p.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
