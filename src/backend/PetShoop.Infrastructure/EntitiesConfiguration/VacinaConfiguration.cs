using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShoop.Domain.Entities;

namespace PetShoop.Infrastructure.EntitiesConfiguration;

public class VacinaConfiguration : IEntityTypeConfiguration<Vacina>
{
    public void Configure(EntityTypeBuilder<Vacina> builder)
    {
        builder.HasKey(v => v.VacinaId);

        builder.Property(v => v.PetId)
            .IsRequired();

        builder.Property(v => v.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.Fabricante)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.DataAplicacao)
            .IsRequired();

        builder.Property(v => v.ProximaDose);

        builder.HasOne<Pet>()
            .WithMany()
            .HasForeignKey(v => v.PetId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
