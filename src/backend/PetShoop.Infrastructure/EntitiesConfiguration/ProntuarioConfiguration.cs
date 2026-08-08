using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShoop.Domain.Entities;

namespace PetShoop.Infrastructure.EntitiesConfiguration;

public class ProntuarioConfiguration : IEntityTypeConfiguration<Prontuario>
{
    public void Configure(EntityTypeBuilder<Prontuario> builder)
    {
        builder.HasKey(p => p.ProntuarioId);

        builder.Property(p => p.PetId)
            .IsRequired();

        builder.Property(p => p.FuncionarioId)
            .IsRequired();

        builder.Property(p => p.DataRegistro)
            .IsRequired();

        builder.Property(p => p.Descricao)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasOne<Pet>()
            .WithMany()
            .HasForeignKey(p => p.PetId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Funcionario>()
            .WithMany()
            .HasForeignKey(p => p.FuncionarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
