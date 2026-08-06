using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Mappings;
using PetShoop.Domain.Interfaces;

namespace PetShoop.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<ProdutoDto>> GetProdutos()
    {
        var produtos = await _produtoRepository.GetProdutosAsync();
        return produtos.ToProdutoDtoList();
    }

    public async Task<ProdutoDto> GetById(Guid? id)
    {
        var produto = await _produtoRepository.GetByIdAsync(id);
        var produtoDto = produto.ToProdutoDto();

        if (produtoDto is null)
        {
            throw new InvalidOperationException("Produto não encontrado.");
        }

        return produtoDto;
    }

    public async Task Add(ProdutoDto produtoDto)
    {
        if (produtoDto is null)
        {
            throw new ArgumentNullException(nameof(produtoDto));
        }

        var produto = produtoDto.ToProduto();

        if (produto is null)
        {
            throw new ArgumentNullException(nameof(produtoDto));
        }

        await _produtoRepository.CreateAsync(produto);
    }

    public async Task Update(ProdutoDto produtoDto)
    {
        if (produtoDto is null)
        {
            throw new ArgumentNullException(nameof(produtoDto));
        }

        var produto = await _produtoRepository.GetByIdAsync(produtoDto.ProdutoId);

        if (produto is null)
        {
            throw new InvalidOperationException("Produto não encontrado.");
        }

        produto.Update(
            produtoDto.Nome,
            produtoDto.Descricao,
            produtoDto.Categoria,
            produtoDto.Marca,
            produtoDto.Preco,
            produtoDto.QuantidadeEmEstoque);

        await _produtoRepository.UpdateAsync(produto);
    }

    public async Task Remove(Guid? id)
    {
        var produto = await _produtoRepository.GetByIdAsync(id);

        if (produto is null)
        {
            throw new InvalidOperationException("Produto não encontrado.");
        }

        await _produtoRepository.RemoveAsync(produto);
    }
}
