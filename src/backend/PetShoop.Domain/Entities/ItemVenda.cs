
using PetShoop.Domain.Validation;

namespace PetShoop.Domain.Entities;

public sealed class ItemVenda
{
    public Guid ItemVendaId { get; private set; }


    //FK
    public Guid VendaId { get; private set; }
    public Guid ProdutoId { get; private set; }

    public int Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }

    public ItemVenda(Guid vendaId, Guid produtoId, int quantidade, decimal valorUnitario)
    {
        ValidateDomain(vendaId, produtoId, quantidade, valorUnitario);
        ItemVendaId = Guid.NewGuid();
    }

    public void Update(Guid vendaId, Guid produtoId, int quantidade, decimal valorUnitario)
    {
        ValidateDomain(vendaId, produtoId, quantidade, valorUnitario);
    }

    public void SetItemVendaId(Guid itemVendaId)
    {
        ItemVendaId = itemVendaId;
    }

    private void ValidateDomain(Guid vendaId, Guid produtoId, int quantidade, decimal valorUnitario)
    {
        DomainExceptionValidation.When(vendaId == Guid.Empty, "Venda inválida. Venda é obrigatória");
        DomainExceptionValidation.When(produtoId == Guid.Empty, "Produto inválido. Produto é obrigatório");
        DomainExceptionValidation.When(quantidade <= 0, "Quantidade inválida. Quantidade deve ser maior que zero");
        DomainExceptionValidation.When(valorUnitario <= 0, "Valor unitário inválido. Valor unitário deve ser maior que zero");

        VendaId = vendaId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
    }
}
