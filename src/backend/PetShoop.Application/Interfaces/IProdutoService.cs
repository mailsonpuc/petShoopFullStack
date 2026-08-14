using PetShoop.Application.DTOs;
using PetShoop.Domain.Pagination;

namespace PetShoop.Application.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoDto>> GetProdutos();
    Task<ProdutoDto> GetById(Guid? id);
    Task Add(ProdutoDto produtoDto);
    Task Update(ProdutoDto produtoDto);
    Task Remove(Guid? id);
    Task<PagedList<ProdutoDto>> GetProdutosPaged(int pageNumber, int pageSize);
    Task AtualizarEstoqueAsync(Guid produtoId, int quantidade);
}
