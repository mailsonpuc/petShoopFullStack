using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShoop.Domain.Entities;

namespace PetShoop.Infrastructure.EntitiesConfiguration;

public class ItemVendaConfiguration : IEntityTypeConfiguration<ItemVenda>
{
    public void Configure(EntityTypeBuilder<ItemVenda> builder)
    {
        builder.HasKey(i => i.ItemVendaId);

        builder.Property(i => i.VendaId)
            .IsRequired();

        builder.Property(i => i.ProdutoId)
            .IsRequired();

        builder.Property(i => i.Quantidade)
            .IsRequired();

        builder.Property(i => i.ValorUnitario)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.HasOne<Venda>()
            .WithMany()
            .HasForeignKey(i => i.VendaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Produto>()
            .WithMany()
            .HasForeignKey(i => i.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
