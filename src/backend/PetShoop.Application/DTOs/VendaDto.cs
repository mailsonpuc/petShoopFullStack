using System.ComponentModel.DataAnnotations;
using PetShoop.Domain.Enums;

namespace PetShoop.Application.DTOs;

public class VendaDto
{
    public Guid VendaId { get; set; }

    [Required(ErrorMessage = "Cliente é obrigatório.")]
    public Guid ClienteId { get; set; }

    [Required(ErrorMessage = "Data da venda é obrigatória.")]
    [DataType(DataType.DateTime, ErrorMessage = "Informe uma data válida.")]
    public DateTime DataVenda { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Valor total não pode ser negativo.")]
    public decimal ValorTotal { get; set; }

    [Required(ErrorMessage = "Forma de pagamento é obrigatória.")]
    [EnumDataType(typeof(FormaPagamento), ErrorMessage = "Forma de pagamento inválida.")]
    public FormaPagamento FormaPagamento { get; set; }
}
