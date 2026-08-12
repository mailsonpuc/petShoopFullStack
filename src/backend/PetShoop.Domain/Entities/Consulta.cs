
using PetShoop.Domain.Validation;

namespace PetShoop.Domain.Entities;

public sealed class Consulta
{
    public Guid ConsultaId { get; private set; }


    //FK
    public Guid PetId { get; private set; }
    public Guid FuncionarioId { get; private set; }


    public DateTime DataConsulta { get; private set; }
    public decimal Peso { get; private set; }
    public decimal Temperatura { get; private set; }
    public string Diagnostico { get; private set; } = string.Empty;
    public string Prescricao { get; private set; } = string.Empty;

    public Consulta(Guid petId, Guid funcionarioId, DateTime dataConsulta, decimal peso, decimal temperatura, string diagnostico, string prescricao)
    {
        ValidateDomain(petId, funcionarioId, dataConsulta, peso, temperatura, diagnostico, prescricao);
        ConsultaId = Guid.NewGuid();
    }

    public void Update(Guid petId, Guid funcionarioId, DateTime dataConsulta, decimal peso, decimal temperatura, string diagnostico, string prescricao)
    {
        ValidateDomain(petId, funcionarioId, dataConsulta, peso, temperatura, diagnostico, prescricao);
    }

    public void SetConsultaId(Guid consultaId)
    {
        ConsultaId = consultaId;
    }

    private void ValidateDomain(Guid petId, Guid funcionarioId, DateTime dataConsulta, decimal peso, decimal temperatura, string diagnostico, string prescricao)
    {
        DomainExceptionValidation.When(petId == Guid.Empty, "Pet inválido. Pet é obrigatório");
        DomainExceptionValidation.When(funcionarioId == Guid.Empty, "Funcionário inválido. Funcionário é obrigatório");
        DomainExceptionValidation.When(dataConsulta == DateTime.MinValue, "Data da consulta inválida. Data da consulta é obrigatória");
        DomainExceptionValidation.When(peso <= 0, "Peso inválido. Peso deve ser maior que zero");
        DomainExceptionValidation.When(temperatura <= 0, "Temperatura inválida. Temperatura deve ser maior que zero");
        DomainExceptionValidation.When(string.IsNullOrEmpty(diagnostico), "Diagnóstico inválido. Diagnóstico é obrigatório");
        DomainExceptionValidation.When(string.IsNullOrEmpty(prescricao), "Prescrição inválida. Prescrição é obrigatória");

        PetId = petId;
        FuncionarioId = funcionarioId;
        DataConsulta = dataConsulta;
        Peso = peso;
        Temperatura = temperatura;
        Diagnostico = diagnostico;
        Prescricao = prescricao;
    }
}
