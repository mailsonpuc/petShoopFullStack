

using PetShoop.Application.DTOs;
using PetShoop.Domain.Entities;

namespace PetShoop.Application.Mappings;

public static class ItemVendaDTOMappingExtensions
{
    public static ItemVendaDto? ToItemVendaDto(this ItemVenda itemVenda)
    {
        if (itemVenda is null)
            return null;

        return new ItemVendaDto
        {
            ItemVendaId = itemVenda.ItemVendaId,
            VendaId = itemVenda.VendaId,
            ProdutoId = itemVenda.ProdutoId,
            Quantidade = itemVenda.Quantidade,
            ValorUnitario = itemVenda.ValorUnitario
        };
    }

    public static ItemVenda? ToItemVenda(this ItemVendaDto itemVendaDto)
    {
        if (itemVendaDto is null)
            return null;

        var itemVenda = new ItemVenda(
            itemVendaDto.VendaId,
            itemVendaDto.ProdutoId,
            itemVendaDto.Quantidade,
            itemVendaDto.ValorUnitario);

        itemVenda.SetItemVendaId(itemVendaDto.ItemVendaId);

        return itemVenda;
    }

    public static IEnumerable<ItemVendaDto> ToItemVendaDtoList(this IEnumerable<ItemVenda> itensVenda)
    {
        if (itensVenda is null || !itensVenda.Any())
        {
            return new List<ItemVendaDto>();
        }

        return itensVenda.Select(itemVenda => new ItemVendaDto
        {
            ItemVendaId = itemVenda.ItemVendaId,
            VendaId = itemVenda.VendaId,
            ProdutoId = itemVenda.ProdutoId,
            Quantidade = itemVenda.Quantidade,
            ValorUnitario = itemVenda.ValorUnitario
        }).ToList();
    }
}
