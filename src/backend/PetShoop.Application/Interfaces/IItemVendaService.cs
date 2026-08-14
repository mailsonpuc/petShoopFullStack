using PetShoop.Application.DTOs;
using PetShoop.Domain.Pagination;

namespace PetShoop.Application.Interfaces;

public interface IItemVendaService
{
    Task<IEnumerable<ItemVendaDto>> GetItensVendas();
    Task<ItemVendaDto> GetById(Guid? id);
    Task Add(ItemVendaDto itemVendaDto);
    Task Update(ItemVendaDto itemVendaDto);
    Task Remove(Guid? id);
    Task<PagedList<ItemVendaDto>> GetItensVendasPaged(int pageNumber, int pageSize);
}
