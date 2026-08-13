using PetShoop.Application.DTOs;
using PetShoop.Domain.Entities;

namespace PetShoop.Application.Mappings;

public static class ProdutoDTOMappingExtensions
{
    public static ProdutoDto? ToProdutoDto(this Produto produto)
    {
        if (produto is null)
            return null;

        return new ProdutoDto
        {
            ProdutoId = produto.ProdutoId,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Categoria = produto.Categoria,
            Marca = produto.Marca,
            Preco = produto.Preco,
            QuantidadeEmEstoque = produto.QuantidadeEmEstoque
        };
    }

    public static Produto? ToProduto(this ProdutoDto produtoDto)
    {
        if (produtoDto is null)
            return null;

        var produto = new Produto(
            produtoDto.Nome,
            produtoDto.Descricao,
            produtoDto.Categoria,
            produtoDto.Marca,
            produtoDto.Preco,
            produtoDto.QuantidadeEmEstoque);

        if (produtoDto.ProdutoId != Guid.Empty)
        {
            produto.SetProdutoId(produtoDto.ProdutoId);
        }

        return produto;
    }

    public static IEnumerable<ProdutoDto> ToProdutoDtoList(this IEnumerable<Produto> produtos)
    {
        if (produtos is null || !produtos.Any())
        {
            return new List<ProdutoDto>();
        }

        return produtos.Select(produto => new ProdutoDto
        {
            ProdutoId = produto.ProdutoId,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Categoria = produto.Categoria,
            Marca = produto.Marca,
            Preco = produto.Preco,
            QuantidadeEmEstoque = produto.QuantidadeEmEstoque
        }).ToList();
    }
}
