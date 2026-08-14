using PetShoop.Domain.Entities;
using PetShoop.Domain.Pagination;

namespace PetShoop.Domain.Interfaces;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> GetProdutosAsync();
    Task<Produto?> GetByIdAsync(Guid? id);
    Task<Produto> CreateAsync(Produto produto);
    Task<Produto> UpdateAsync(Produto produto);
    Task<Produto> RemoveAsync(Produto produto);
    Task<bool> HasItensVendaAsync(Guid produtoId);
    Task<PagedList<Produto>> GetProdutosPagedAsync(int pageNumber, int pageSize);
}
