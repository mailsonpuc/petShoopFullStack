using PetShoop.Domain.Entities;
using PetShoop.Domain.Pagination;

namespace PetShoop.Domain.Interfaces;

public interface IItemVendaRepository
{
    Task<IEnumerable<ItemVenda>> GetItensVendasAsync();
    Task<ItemVenda?> GetByIdAsync(Guid? id);
    Task<ItemVenda> CreateAsync(ItemVenda itemVenda);
    Task<ItemVenda> UpdateAsync(ItemVenda itemVenda);
    Task<ItemVenda> RemoveAsync(ItemVenda itemVenda);
    Task<PagedList<ItemVenda>> GetItensVendasPagedAsync(int pageNumber, int pageSize);
}
