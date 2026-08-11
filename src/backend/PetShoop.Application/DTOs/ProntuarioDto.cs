using System.ComponentModel.DataAnnotations;

namespace PetShoop.Application.DTOs;

public class ProntuarioDto
{
    public Guid ProntuarioId { get; set; }

    [Required(ErrorMessage = "Pet é obrigatório.")]
    public Guid PetId { get; set; }

    [Required(ErrorMessage = "Funcionário é obrigatório.")]
    public Guid FuncionarioId { get; set; }

    [Required(ErrorMessage = "Data de registro é obrigatória.")]
    [DataType(DataType.DateTime, ErrorMessage = "Informe uma data válida.")]
    public DateTime DataRegistro { get; set; }

    [Required(ErrorMessage = "Descrição é obrigatória.")]
    [StringLength(1000, ErrorMessage = "Descrição deve ter no máximo 1000 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    public string? PetNome { get; set; }
    public string? FuncionarioNome { get; set; }
}
