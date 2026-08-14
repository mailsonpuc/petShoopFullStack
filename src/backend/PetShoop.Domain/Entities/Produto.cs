
using PetShoop.Domain.Enums;
using PetShoop.Domain.Validation;

namespace PetShoop.Domain.Entities;

public sealed class Produto
{
    public Guid ProdutoId { get; private set; }


    public string Nome { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public CategoriaProduto Categoria { get; private set; }
    public string Marca { get; private set; } = string.Empty;
    public decimal Preco { get; private set; }
    public int QuantidadeEmEstoque { get; private set; }

    public Produto(string nome, string descricao, CategoriaProduto categoria, string marca, decimal preco, int quantidadeEmEstoque)
    {
        ValidateDomain(nome, descricao, categoria, marca, preco, quantidadeEmEstoque);
        ProdutoId = Guid.NewGuid();
    }

    public void Update(string nome, string descricao, CategoriaProduto categoria, string marca, decimal preco, int quantidadeEmEstoque)
    {
        ValidateDomain(nome, descricao, categoria, marca, preco, quantidadeEmEstoque);
    }

    public void SetProdutoId(Guid produtoId)
    {
        ProdutoId = produtoId;
    }

    public void DebitStock(int quantidade)
    {
        DomainExceptionValidation.When(quantidade <= 0, "Quantidade inválida. Quantidade deve ser maior que zero");
        DomainExceptionValidation.When(QuantidadeEmEstoque < quantidade, "Estoque insuficiente.");
        QuantidadeEmEstoque -= quantidade;
    }

    private void ValidateDomain(string nome, string descricao, CategoriaProduto categoria, string marca, decimal preco, int quantidadeEmEstoque)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. Nome é obrigatório");
        DomainExceptionValidation.When(nome.Length < 3, "Nome inválido. Nome deve ter no mínimo 3 caracteres");
        DomainExceptionValidation.When(string.IsNullOrEmpty(descricao), "Descrição inválida. Descrição é obrigatória");
        DomainExceptionValidation.When(!Enum.IsDefined(categoria), "Categoria inválida");
        DomainExceptionValidation.When(string.IsNullOrEmpty(marca), "Marca inválida. Marca é obrigatória");
        DomainExceptionValidation.When(preco <= 0, "Preço inválido. Preço deve ser maior que zero");
        DomainExceptionValidation.When(quantidadeEmEstoque < 0, "Quantidade em estoque inválida. Quantidade não pode ser negativa");

        Nome = nome;
        Descricao = descricao;
        Categoria = categoria;
        Marca = marca;
        Preco = preco;
        QuantidadeEmEstoque = quantidadeEmEstoque;
    }
}
