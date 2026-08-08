using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShoop.Domain.Entities;

namespace PetShoop.Infrastructure.EntitiesConfiguration;

public class ConsultaConfiguration : IEntityTypeConfiguration<Consulta>
{
    public void Configure(EntityTypeBuilder<Consulta> builder)
    {
        builder.HasKey(c => c.ConsultaId);

        builder.Property(c => c.PetId)
            .IsRequired();

        builder.Property(c => c.FuncionarioId)
            .IsRequired();

        builder.Property(c => c.DataConsulta)
            .IsRequired();

        builder.Property(c => c.Peso)
            .IsRequired()
            .HasPrecision(6, 2);

        builder.Property(c => c.Temperatura)
            .IsRequired()
            .HasPrecision(4, 1);

        builder.Property(c => c.Diagnostico)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(c => c.Prescricao)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasOne<Pet>()
            .WithMany()
            .HasForeignKey(c => c.PetId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Funcionario>()
            .WithMany()
            .HasForeignKey(c => c.FuncionarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
