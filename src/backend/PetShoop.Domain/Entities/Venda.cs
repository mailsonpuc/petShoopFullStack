
using PetShoop.Domain.Enums;
using PetShoop.Domain.Validation;

namespace PetShoop.Domain.Entities;

public sealed class Venda
{
    public Guid VendaId { get; private set; }


    //FK
    public Guid ClienteId { get; private set; }



    public DateTime DataVenda { get; private set; }
    public decimal ValorTotal { get; private set; }
    public FormaPagamento FormaPagamento { get; private set; }

    public Venda(Guid clienteId, DateTime dataVenda, decimal valorTotal, FormaPagamento formaPagamento)
    {
        ValidateDomain(clienteId, dataVenda, valorTotal, formaPagamento);
        VendaId = Guid.NewGuid();
    }

    public void Update(Guid clienteId, DateTime dataVenda, decimal valorTotal, FormaPagamento formaPagamento)
    {
        ValidateDomain(clienteId, dataVenda, valorTotal, formaPagamento);
    }

    private void ValidateDomain(Guid clienteId, DateTime dataVenda, decimal valorTotal, FormaPagamento formaPagamento)
    {
        DomainExceptionValidation.When(clienteId == Guid.Empty, "Cliente inválido. Cliente é obrigatório");
        DomainExceptionValidation.When(dataVenda == DateTime.MinValue, "Data da venda inválida. Data da venda é obrigatória");
        DomainExceptionValidation.When(valorTotal < 0, "Valor total inválido. Valor total não pode ser negativo");
        DomainExceptionValidation.When(!Enum.IsDefined(formaPagamento), "Forma de pagamento inválida");

        ClienteId = clienteId;
        DataVenda = dataVenda;
        ValorTotal = valorTotal;
        FormaPagamento = formaPagamento;
    }
}
