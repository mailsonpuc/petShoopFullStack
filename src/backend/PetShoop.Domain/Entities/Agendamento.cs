
using PetShoop.Domain.Enums;
using PetShoop.Domain.Validation;

namespace PetShoop.Domain.Entities;

public sealed class Agendamento
{
    public Guid AgendamentoId { get; private set; }

    //FK
    public Guid PetId { get; private set; }
    public Guid ServicoId { get; private set; }
    public Guid FuncionarioId { get; private set; }

    public DateTime DataHora { get; private set; }
    public StatusAgendamento Status { get; private set; }
    public string Observacoes { get; private set; } = string.Empty;

    public Agendamento(Guid petId, Guid servicoId, Guid funcionarioId, DateTime dataHora, StatusAgendamento status, string observacoes)
    {
        ValidateDomain(petId, servicoId, funcionarioId, dataHora, status, observacoes);
        AgendamentoId = Guid.NewGuid();
    }

    public void Update(Guid petId, Guid servicoId, Guid funcionarioId, DateTime dataHora, StatusAgendamento status, string observacoes)
    {
        ValidateDomain(petId, servicoId, funcionarioId, dataHora, status, observacoes);
    }

    public void SetAgendamentoId(Guid agendamentoId)
    {
        AgendamentoId = agendamentoId;
    }

    private void ValidateDomain(Guid petId, Guid servicoId, Guid funcionarioId, DateTime dataHora, StatusAgendamento status, string observacoes)
    {
        DomainExceptionValidation.When(petId == Guid.Empty, "Pet inválido. Pet é obrigatório");
        DomainExceptionValidation.When(servicoId == Guid.Empty, "Serviço inválido. Serviço é obrigatório");
        DomainExceptionValidation.When(funcionarioId == Guid.Empty, "Funcionário inválido. Funcionário é obrigatório");
        DomainExceptionValidation.When(dataHora == DateTime.MinValue, "Data e hora inválidas. Data e hora são obrigatórias");
        DomainExceptionValidation.When(!Enum.IsDefined(status), "Status inválido");

        PetId = petId;
        ServicoId = servicoId;
        FuncionarioId = funcionarioId;
        DataHora = dataHora;
        Status = status;
        Observacoes = observacoes;
    }
}
