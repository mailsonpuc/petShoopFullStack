using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Mappings;
using PetShoop.Domain.Interfaces;

namespace PetShoop.Application.Services;

public class ItemVendaService : IItemVendaService
{
    private readonly IItemVendaRepository _itemVendaRepository;

    public ItemVendaService(IItemVendaRepository itemVendaRepository)
    {
        _itemVendaRepository = itemVendaRepository;
    }

    public async Task<IEnumerable<ItemVendaDto>> GetItensVendas()
    {
        var itensVenda = await _itemVendaRepository.GetItensVendasAsync();
        return itensVenda.ToItemVendaDtoList();
    }

    public async Task<ItemVendaDto> GetById(Guid? id)
    {
        var itemVenda = await _itemVendaRepository.GetByIdAsync(id);
        var itemVendaDto = itemVenda.ToItemVendaDto();

        if (itemVendaDto is null)
        {
            throw new InvalidOperationException("Item de venda não encontrado.");
        }

        return itemVendaDto;
    }

    public async Task Add(ItemVendaDto itemVendaDto)
    {
        if (itemVendaDto is null)
        {
            throw new ArgumentNullException(nameof(itemVendaDto));
        }

        var itemVenda = itemVendaDto.ToItemVenda();

        if (itemVenda is null)
        {
            throw new ArgumentNullException(nameof(itemVendaDto));
        }

        await _itemVendaRepository.CreateAsync(itemVenda);
    }

    public async Task Update(ItemVendaDto itemVendaDto)
    {
        if (itemVendaDto is null)
        {
            throw new ArgumentNullException(nameof(itemVendaDto));
        }

        var itemVenda = await _itemVendaRepository.GetByIdAsync(itemVendaDto.ItemVendaId);

        if (itemVenda is null)
        {
            throw new InvalidOperationException("Item de venda não encontrado.");
        }

        itemVenda.Update(
            itemVendaDto.VendaId,
            itemVendaDto.ProdutoId,
            itemVendaDto.Quantidade,
            itemVendaDto.ValorUnitario);

        await _itemVendaRepository.UpdateAsync(itemVenda);
    }

    public async Task Remove(Guid? id)
    {
        var itemVenda = await _itemVendaRepository.GetByIdAsync(id);

        if (itemVenda is null)
        {
            throw new InvalidOperationException("Item de venda não encontrado.");
        }

        await _itemVendaRepository.RemoveAsync(itemVenda);
    }
}
