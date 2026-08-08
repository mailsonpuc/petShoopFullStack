using System.ComponentModel.DataAnnotations;

namespace PetShoop.Application.DTOs;

public class ConsultaDto
{
    public Guid ConsultaId { get; set; }

    [Required(ErrorMessage = "Pet é obrigatório.")]
    public Guid PetId { get; set; }

    [Required(ErrorMessage = "Funcionário é obrigatório.")]
    public Guid FuncionarioId { get; set; }

    [Required(ErrorMessage = "Data da consulta é obrigatória.")]
    [DataType(DataType.DateTime, ErrorMessage = "Informe uma data válida.")]
    public DateTime DataConsulta { get; set; }

    [Required(ErrorMessage = "Peso é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Peso deve ser maior que zero.")]
    public decimal Peso { get; set; }

    [Required(ErrorMessage = "Temperatura é obrigatória.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Temperatura deve ser maior que zero.")]
    public decimal Temperatura { get; set; }

    [Required(ErrorMessage = "Diagnóstico é obrigatório.")]
    [StringLength(500, ErrorMessage = "Diagnóstico deve ter no máximo 500 caracteres.")]
    public string Diagnostico { get; set; } = string.Empty;

    [Required(ErrorMessage = "Prescrição é obrigatória.")]
    [StringLength(1000, ErrorMessage = "Prescrição deve ter no máximo 1000 caracteres.")]
    public string Prescricao { get; set; } = string.Empty;
}
