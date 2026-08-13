using PetShoop.Application.DTOs;
using PetShoop.Domain.Entities;

namespace PetShoop.Application.Mappings;

public static class VendaDTOMappingExtensions
{
    public static VendaDto? ToVendaDto(this Venda venda)
    {
        if (venda is null)
            return null;

        return new VendaDto
        {
            VendaId = venda.VendaId,
            ClienteId = venda.ClienteId,
            DataVenda = venda.DataVenda,
            ValorTotal = venda.ValorTotal,
            FormaPagamento = venda.FormaPagamento
        };
    }

    public static Venda? ToVenda(this VendaDto vendaDto)
    {
        if (vendaDto is null)
            return null;

        var venda = new Venda(
            vendaDto.ClienteId,
            vendaDto.DataVenda,
            vendaDto.ValorTotal,
            vendaDto.FormaPagamento);

        if (vendaDto.VendaId != Guid.Empty)
        {
            venda.SetVendaId(vendaDto.VendaId);
        }

        return venda;
    }

    public static IEnumerable<VendaDto> ToVendaDtoList(this IEnumerable<Venda> vendas)
    {
        if (vendas is null || !vendas.Any())
        {
            return new List<VendaDto>();
        }

        return vendas.Select(venda => new VendaDto
        {
            VendaId = venda.VendaId,
            ClienteId = venda.ClienteId,
            DataVenda = venda.DataVenda,
            ValorTotal = venda.ValorTotal,
            FormaPagamento = venda.FormaPagamento
        }).ToList();
    }
}
