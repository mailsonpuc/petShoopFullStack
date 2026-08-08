
using PetShoop.Domain.Validation;

namespace PetShoop.Domain.Entities;

public sealed class Prontuario
{
    public Guid ProntuarioId { get; private set; }


    //FK
    public Guid PetId { get; private set; }
    public Guid FuncionarioId { get; private set; }

    public DateTime DataRegistro { get; private set; }
    public string Descricao { get; private set; } = string.Empty;

    public Prontuario(Guid petId, Guid funcionarioId, DateTime dataRegistro, string descricao)
    {
        ValidateDomain(petId, funcionarioId, dataRegistro, descricao);
        ProntuarioId = Guid.NewGuid();
    }

    public void Update(Guid petId, Guid funcionarioId, DateTime dataRegistro, string descricao)
    {
        ValidateDomain(petId, funcionarioId, dataRegistro, descricao);
    }

    private void ValidateDomain(Guid petId, Guid funcionarioId, DateTime dataRegistro, string descricao)
    {
        DomainExceptionValidation.When(petId == Guid.Empty, "Pet inválido. Pet é obrigatório");
        DomainExceptionValidation.When(funcionarioId == Guid.Empty, "Funcionário inválido. Funcionário é obrigatório");
        DomainExceptionValidation.When(dataRegistro == DateTime.MinValue, "Data de registro inválida. Data de registro é obrigatória");
        DomainExceptionValidation.When(string.IsNullOrEmpty(descricao), "Descrição inválida. Descrição é obrigatória");

        PetId = petId;
        FuncionarioId = funcionarioId;
        DataRegistro = dataRegistro;
        Descricao = descricao;
    }
}
