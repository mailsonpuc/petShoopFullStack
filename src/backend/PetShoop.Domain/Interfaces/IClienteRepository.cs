using PetShoop.Domain.Pagination;
using PetShoop.Domain.Entities;

namespace PetShoop.Domain.Interfaces;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> GetClientesAsync();
    Task<Cliente?> GetByIdAsync(Guid? id);
    Task<Cliente> CreateAsync(Cliente cliente);
    Task<Cliente> UpdateAsync(Cliente cliente);
    Task<Cliente> RemoveAsync(Cliente cliente);
    Task<bool> HasPetsAsync(Guid clienteId);
    Task<bool> HasVendasAsync(Guid clienteId);
    Task<PagedList<Cliente>> GetClientesPagedAsync(int pageNumber, int pageSize);
}
