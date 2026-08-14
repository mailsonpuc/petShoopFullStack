using PetShoop.Domain.Entities;
using PetShoop.Domain.Pagination;

namespace PetShoop.Domain.Interfaces;

public interface IVendaRepository
{
    Task<IEnumerable<Venda>> GetVendasAsync();
    Task<Venda?> GetByIdAsync(Guid? id);
    Task<Venda> CreateAsync(Venda venda);
    Task<Venda> UpdateAsync(Venda venda);
    Task<Venda> RemoveAsync(Venda venda);
    Task<bool> HasItensVendaAsync(Guid vendaId);
    Task<PagedList<Venda>> GetVendasPagedAsync(int pageNumber, int pageSize);
}
