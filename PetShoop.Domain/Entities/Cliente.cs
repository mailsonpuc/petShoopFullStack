
using PetShoop.Domain.Validation;

namespace PetShoop.Domain.Entities;

public sealed class Cliente
{
    public Guid ClienteId { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Cpf { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Telefone { get; private set; } = string.Empty;
    public DateTime DataDeNascimento { get; private set; }
    public string Endereco { get; private set; } = string.Empty;
    public DateTime DataDeCadastro { get; private set; }


    //construtor
    public Cliente(string nome, string cpf, string email, string telefone, DateTime dataDeNascimento, string endereco)
    {
        ValidateDomain(nome, cpf, email, telefone, dataDeNascimento, endereco);
        ClienteId = Guid.NewGuid();
        DataDeCadastro = DateTime.UtcNow;
    }



    public void Update(string nome, string cpf, string email, string telefone, DateTime dataDeNascimento, string endereco)
    {
        ValidateDomain(nome, cpf, email, telefone, dataDeNascimento, endereco);
    }


    private void ValidateDomain(string nome, string cpf, string email, string telefone, DateTime dataDeNascimento, string endereco)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. Nome é obrigatório");
        DomainExceptionValidation.When(nome.Length < 3, "Nome inválido. Nome deve ter no mínimo 3 caracteres");
        DomainExceptionValidation.When(string.IsNullOrEmpty(cpf), "CPF inválido. CPF é obrigatório");
        DomainExceptionValidation.When(string.IsNullOrEmpty(email), "Email inválido. Email é obrigatório");
        DomainExceptionValidation.When(string.IsNullOrEmpty(telefone), "Telefone inválido. Telefone é obrigatório");
        DomainExceptionValidation.When(dataDeNascimento == DateTime.MinValue, "Data de nascimento inválida. Data de nascimento é obrigatória");
        DomainExceptionValidation.When(string.IsNullOrEmpty(endereco), "Endereço inválido. Endereço é obrigatório");

        Nome = nome;
        Cpf = cpf;
        Email = email;
        Telefone = telefone;
        DataDeNascimento = dataDeNascimento;
        Endereco = endereco;
    }
}
