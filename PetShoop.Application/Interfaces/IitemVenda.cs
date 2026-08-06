using PetShoop.Application.DTOs;

namespace PetShoop.Application.Interfaces;

public interface IitemVenda
{
    Task<IEnumerable<ItemVendaDto>> GetItemVendas();
    Task<ItemVendaDto> GetById(Guid? id);
    Task Add(ItemVendaDto itemVendaDto);
    Task Update(ItemVendaDto itemVendaDto);
    Task Remove(Guid? id);
}
