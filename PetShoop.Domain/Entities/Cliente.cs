

using PetShoop.Domain.Validation;

namespace PetShoop.Domain.Entities;

public sealed class Cliente
{
    public Guid ClienteId { get; private set; }
    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public string Email { get; private set; }
    public string Telefone { get; private set; }
    public DateTime DataDeNacimento { get; private set; }
    public string Endereco { get; private set; }
    public DateTime DataDeCadastro { get; private set; }


    //construtor
    public Cliente(string nome, string cpf, string email, string telefone, DateTime dataDeNacimento, string endereco)
    {
        ValidateDomain(nome, cpf, email, telefone, dataDeNacimento, endereco);
    }



    public void Update(string nome, string cpf, string email, string telefone, DateTime dataDeNacimento, string endereco)
    {
        ValidateDomain(nome, cpf, email, telefone, dataDeNacimento, endereco);
    }


    private void ValidateDomain(string nome, string cpf, string email, string telefone, DateTime dataDeNacimento, string endereco)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. Nome é obrigatório");
        DomainExceptionValidation.When(nome.Length < 3, "Nome inválido. Nome deve ter no mínimo 3 caracteres");
        DomainExceptionValidation.When(string.IsNullOrEmpty(cpf), "CPF inválido. CPF é obrigatório");
        DomainExceptionValidation.When(string.IsNullOrEmpty(email), "Email inválido. Email é obrigatório");
        DomainExceptionValidation.When(string.IsNullOrEmpty(telefone), "Telefone inválido. Telefone é obrigatório");
        DomainExceptionValidation.When(dataDeNacimento == DateTime.MinValue, "Data de nascimento inválida. Data de nascimento é obrigatória");
        DomainExceptionValidation.When(string.IsNullOrEmpty(endereco), "Endereço inválido. Endereço é obrigatório");
    }
}
