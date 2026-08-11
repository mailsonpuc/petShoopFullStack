using System.ComponentModel.DataAnnotations;
using PetShoop.Domain.Enums;

namespace PetShoop.Application.DTOs;

public class AgendamentoDto
{
    public Guid AgendamentoId { get; set; }

    [Required(ErrorMessage = "Pet é obrigatório.")]
    public Guid PetId { get; set; }

    [Required(ErrorMessage = "Serviço é obrigatório.")]
    public Guid ServicoId { get; set; }

    [Required(ErrorMessage = "Funcionário é obrigatório.")]
    public Guid FuncionarioId { get; set; }

    [Required(ErrorMessage = "Data e hora são obrigatórias.")]
    [DataType(DataType.DateTime, ErrorMessage = "Informe uma data e hora válidas.")]
    public DateTime DataHora { get; set; }

    [Required(ErrorMessage = "Status do agendamento é obrigatório.")]
    [EnumDataType(typeof(StatusAgendamento), ErrorMessage = "Status do agendamento inválido.")]
    public StatusAgendamento Status { get; set; }

    public string? PetNome { get; set; }
    public string? ServicoNome { get; set; }
    public string? FuncionarioNome { get; set; }

    public string Observacoes { get; set; } = string.Empty;
}
