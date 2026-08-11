

using System.ComponentModel.DataAnnotations;

namespace PetShoop.Application.DTOs;

public class ItemVendaDto
{
    public Guid ItemVendaId { get; set; }

    [Required(ErrorMessage = "Venda é obrigatória.")]
    public Guid VendaId { get; set; }

    [Required(ErrorMessage = "Produto é obrigatório.")]
    public Guid ProdutoId { get; set; }

    [Required(ErrorMessage = "Quantidade é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero.")]
    public int Quantidade { get; set; }

    [Required(ErrorMessage = "Valor unitário é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Valor unitário deve ser maior que zero.")]
    public decimal ValorUnitario { get; set; }

    public string? ProdutoNome { get; set; }
    public string? VendaInfo { get; set; }
    public string? ClienteNome { get; set; }
}
