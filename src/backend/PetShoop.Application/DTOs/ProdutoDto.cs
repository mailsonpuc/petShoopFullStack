using System.ComponentModel.DataAnnotations;
using PetShoop.Domain.Enums;

namespace PetShoop.Application.DTOs;

public class ProdutoDto
{
    public Guid ProdutoId { get; set; }

    [Required(ErrorMessage = "Nome do produto é obrigatório.")]
    [MinLength(3, ErrorMessage = "Nome do produto deve ter no mínimo 3 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição é obrigatória.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Categoria é obrigatória.")]
    public CategoriaProduto Categoria { get; set; }

    [Required(ErrorMessage = "Marca é obrigatória.")]
    public string Marca { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero.")]
    public decimal Preco { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantidade em estoque não pode ser negativa.")]
    public int QuantidadeEmEstoque { get; set; }
}
