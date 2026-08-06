using PetShoop.Application.DTOs;

namespace PetShoop.Application.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoDto>> GetProdutos();
    Task<ProdutoDto> GetById(Guid? id);
    Task Add(ProdutoDto produtoDto);
    Task Update(ProdutoDto produtoDto);
    Task Remove(Guid? id);
}
