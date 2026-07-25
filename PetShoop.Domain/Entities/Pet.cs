
using PetShoop.Domain.Enums;
using PetShoop.Domain.Validation;

namespace PetShoop.Domain.Entities;

public sealed class Pet
{
    public Guid PetId { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public EspeciePet Especie { get; private set; }
    public string Raca { get; private set; } = string.Empty;
    public SexoPet Sexo { get; private set; }
    public DateTime DataDeNascimento { get; private set; }
    public decimal Peso { get; private set; }
    public string Cor { get; private set; } = string.Empty;
    public PortePet Porte { get; private set; }
    public string Observacoes { get; private set; } = string.Empty;



    //FK
    public Guid ClienteId { get; private set; }

    public Pet(string nome, EspeciePet especie, string raca, SexoPet sexo, DateTime dataDeNascimento, decimal peso, string cor, PortePet porte, string observacoes, Guid clienteId)
    {
        ValidateDomain(nome, especie, raca, sexo, dataDeNascimento, peso, cor, porte, observacoes, clienteId);
        PetId = Guid.NewGuid();
    }

    public void Update(string nome, EspeciePet especie, string raca, SexoPet sexo, DateTime dataDeNascimento, decimal peso, string cor, PortePet porte, string observacoes, Guid clienteId)
    {
        ValidateDomain(nome, especie, raca, sexo, dataDeNascimento, peso, cor, porte, observacoes, clienteId);
    }

    private void ValidateDomain(string nome, EspeciePet especie, string raca, SexoPet sexo, DateTime dataDeNascimento, decimal peso, string cor, PortePet porte, string observacoes, Guid clienteId)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. Nome é obrigatório");
        DomainExceptionValidation.When(nome.Length < 2, "Nome inválido. Nome deve ter no mínimo 2 caracteres");
        DomainExceptionValidation.When(!Enum.IsDefined(especie), "Espécie inválida");
        DomainExceptionValidation.When(string.IsNullOrEmpty(raca), "Raça inválida. Raça é obrigatória");
        DomainExceptionValidation.When(!Enum.IsDefined(sexo), "Sexo inválido");
        DomainExceptionValidation.When(dataDeNascimento == DateTime.MinValue, "Data de nascimento inválida. Data de nascimento é obrigatória");
        DomainExceptionValidation.When(peso <= 0, "Peso inválido. Peso deve ser maior que zero");
        DomainExceptionValidation.When(string.IsNullOrEmpty(cor), "Cor inválida. Cor é obrigatória");
        DomainExceptionValidation.When(!Enum.IsDefined(porte), "Porte inválido");
        DomainExceptionValidation.When(clienteId == Guid.Empty, "Cliente inválido. Cliente é obrigatório");

        Nome = nome;
        Especie = especie;
        Raca = raca;
        Sexo = sexo;
        DataDeNascimento = dataDeNascimento;
        Peso = peso;
        Cor = cor;
        Porte = porte;
        Observacoes = observacoes;
        ClienteId = clienteId;
    }
}
