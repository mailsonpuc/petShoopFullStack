using System.ComponentModel.DataAnnotations;

namespace PetShoop.Application.DTOs;

public class VacinaDto
{
    public Guid VacinaId { get; set; }

    [Required(ErrorMessage = "Pet é obrigatório.")]
    public Guid PetId { get; set; }

    [Required(ErrorMessage = "Nome da vacina é obrigatório.")]
    [MinLength(3, ErrorMessage = "Nome da vacina deve ter no mínimo 3 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Fabricante é obrigatório.")]
    public string Fabricante { get; set; } = string.Empty;

    [Required(ErrorMessage = "Data de aplicação é obrigatória.")]
    [DataType(DataType.Date, ErrorMessage = "Informe uma data válida.")]
    public DateTime DataAplicacao { get; set; }

    [DataType(DataType.Date, ErrorMessage = "Informe uma data válida.")]
    public DateTime ProximaDose { get; set; }

    public string? PetNome { get; set; }
}
