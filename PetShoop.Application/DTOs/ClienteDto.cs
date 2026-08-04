using System.ComponentModel.DataAnnotations;

namespace PetShoop.Application.DTOs;

public class ClienteDto
{
    public Guid ClienteId { get; set; }

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

    [Required(ErrorMessage = "Data de nascimento é obrigatória.")]
    [DataType(DataType.Date, ErrorMessage = "Informe uma data válida.")]
    public DateTime DataDeNascimento { get; set; }

    [Required(ErrorMessage = "Endereço é obrigatório.")]
    [MinLength(5, ErrorMessage = "Endereço deve ter no mínimo 5 caracteres.")]
    public string Endereco { get; set; } = string.Empty;
}
