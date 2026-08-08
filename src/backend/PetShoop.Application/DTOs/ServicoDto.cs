using System.ComponentModel.DataAnnotations;

namespace PetShoop.Application.DTOs;

public class ServicoDto
{
    public Guid ServicoId { get; set; }

    [Required(ErrorMessage = "Nome do serviço é obrigatório.")]
    [MinLength(3, ErrorMessage = "Nome do serviço deve ter no mínimo 3 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição é obrigatória.")]
    public string Descricao { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero.")]
    public decimal Preco { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Duração deve ser maior que zero.")]
    public int DuracaoEmMinutos { get; set; }
}
