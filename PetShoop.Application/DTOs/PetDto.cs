using System.ComponentModel.DataAnnotations;
using PetShoop.Domain.Enums;

namespace PetShoop.Application.DTOs;

public class PetDto
{
    public Guid PetId { get; set; }

    [Required(ErrorMessage = "Nome do pet é obrigatório.")]
    [MinLength(2, ErrorMessage = "Nome do pet deve ter no mínimo 2 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Espécie é obrigatória.")]
    public EspeciePet Especie { get; set; }

    [Required(ErrorMessage = "Raça é obrigatória.")]
    public string Raca { get; set; } = string.Empty;

    [Required(ErrorMessage = "Sexo é obrigatório.")]
    public SexoPet Sexo { get; set; }

    [Required(ErrorMessage = "Data de nascimento é obrigatória.")]
    [DataType(DataType.Date, ErrorMessage = "Informe uma data válida.")]
    public DateTime DataDeNascimento { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Peso deve ser maior que zero.")]
    public decimal Peso { get; set; }

    [Required(ErrorMessage = "Cor é obrigatória.")]
    public string Cor { get; set; } = string.Empty;

    [Required(ErrorMessage = "Porte é obrigatório.")]
    public PortePet Porte { get; set; }

    public string Observacoes { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cliente é obrigatório.")]
    public Guid ClienteId { get; set; }
}
