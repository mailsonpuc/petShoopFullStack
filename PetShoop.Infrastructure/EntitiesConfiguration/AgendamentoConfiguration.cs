using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShoop.Domain.Entities;

namespace PetShoop.Infrastructure.EntitiesConfiguration;

public class AgendamentoConfiguration : IEntityTypeConfiguration<Agendamento>
{
    public void Configure(EntityTypeBuilder<Agendamento> builder)
    {
        builder.HasKey(a => a.AgendamentoId);

        builder.Property(a => a.PetId)
            .IsRequired();

        builder.Property(a => a.ServicoId)
            .IsRequired();

        builder.Property(a => a.FuncionarioId)
            .IsRequired();

        builder.Property(a => a.DataHora)
            .IsRequired();

        builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(a => a.Observacoes)
            .HasMaxLength(500);

        builder.HasOne<Pet>()
            .WithMany()
            .HasForeignKey(a => a.PetId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Servico>()
            .WithMany()
            .HasForeignKey(a => a.ServicoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Funcionario>()
            .WithMany()
            .HasForeignKey(a => a.FuncionarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
