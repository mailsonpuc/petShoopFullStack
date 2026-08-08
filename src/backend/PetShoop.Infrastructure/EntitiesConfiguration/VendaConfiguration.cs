using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShoop.Domain.Entities;

namespace PetShoop.Infrastructure.EntitiesConfiguration;

public class VendaConfiguration : IEntityTypeConfiguration<Venda>
{
    public void Configure(EntityTypeBuilder<Venda> builder)
    {
        builder.HasKey(v => v.VendaId);

        builder.Property(v => v.ClienteId)
            .IsRequired();

        builder.Property(v => v.DataVenda)
            .IsRequired();

        builder.Property(v => v.ValorTotal)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(v => v.FormaPagamento)
            .IsRequired()
            .HasConversion<int>();

        builder.HasOne<Cliente>()
            .WithMany()
            .HasForeignKey(v => v.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
