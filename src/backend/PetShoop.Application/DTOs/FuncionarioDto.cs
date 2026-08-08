using System.ComponentModel.DataAnnotations;
using PetShoop.Domain.Enums;

namespace PetShoop.Application.DTOs;

public class FuncionarioDto
{
    public Guid FuncionarioId { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório.")]
    [MinLength(3, ErrorMessage = "Nome deve ter no mínimo 3 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "CPF é obrigatório.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter 11 dígitos.")]
    public string Cpf { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefone é obrigatório.")]
    [Phone(ErrorMessage = "Informe um telefone válido.")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cargo é obrigatório.")]
    [EnumDataType(typeof(CargoFuncionario), ErrorMessage = "Cargo inválido.")]
    public CargoFuncionario Cargo { get; set; }

    [Required(ErrorMessage = "Salário é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Salário deve ser maior que zero.")]
    public decimal Salario { get; set; }

    [Required(ErrorMessage = "Data de admissão é obrigatória.")]
    [DataType(DataType.Date, ErrorMessage = "Informe uma data válida.")]
    public DateTime DataAdmissao { get; set; }
}
