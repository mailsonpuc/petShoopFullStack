
using PetShoop.Domain.Enums;
using PetShoop.Domain.Validation;

namespace PetShoop.Domain.Entities;

public sealed class Funcionario
{
    public Guid FuncionarioId { get; private set; }

    public string Nome { get; private set; } = string.Empty;
    public string Cpf { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Telefone { get; private set; } = string.Empty;
    public CargoFuncionario Cargo { get; private set; }
    public decimal Salario { get; private set; }
    public DateTime DataAdmissao { get; private set; }

    public Funcionario(string nome, string cpf, string email, string telefone, CargoFuncionario cargo, decimal salario, DateTime dataAdmissao)
    {
        ValidateDomain(nome, cpf, email, telefone, cargo, salario, dataAdmissao);
        FuncionarioId = Guid.NewGuid();
    }

    public void Update(string nome, string cpf, string email, string telefone, CargoFuncionario cargo, decimal salario, DateTime dataAdmissao)
    {
        ValidateDomain(nome, cpf, email, telefone, cargo, salario, dataAdmissao);
    }

    private void ValidateDomain(string nome, string cpf, string email, string telefone, CargoFuncionario cargo, decimal salario, DateTime dataAdmissao)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. Nome é obrigatório");
        DomainExceptionValidation.When(nome.Length < 3, "Nome inválido. Nome deve ter no mínimo 3 caracteres");
        DomainExceptionValidation.When(string.IsNullOrEmpty(cpf), "CPF inválido. CPF é obrigatório");
        DomainExceptionValidation.When(string.IsNullOrEmpty(email), "Email inválido. Email é obrigatório");
        DomainExceptionValidation.When(string.IsNullOrEmpty(telefone), "Telefone inválido. Telefone é obrigatório");
        DomainExceptionValidation.When(!Enum.IsDefined(cargo), "Cargo inválido");
        DomainExceptionValidation.When(salario <= 0, "Salário inválido. Salário deve ser maior que zero");
        DomainExceptionValidation.When(dataAdmissao == DateTime.MinValue, "Data de admissão inválida. Data de admissão é obrigatória");

        Nome = nome;
        Cpf = cpf;
        Email = email;
        Telefone = telefone;
        Cargo = cargo;
        Salario = salario;
        DataAdmissao = dataAdmissao;
    }
}
