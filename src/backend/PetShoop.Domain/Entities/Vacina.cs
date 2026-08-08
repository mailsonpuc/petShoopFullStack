
using PetShoop.Domain.Validation;

namespace PetShoop.Domain.Entities;

public sealed class Vacina
{
    public Guid VacinaId { get; private set; }


    //FK
    public Guid PetId { get; private set; }

    public string Nome { get; private set; } = string.Empty;
    public string Fabricante { get; private set; } = string.Empty;
    public DateTime DataAplicacao { get; private set; }
    public DateTime ProximaDose { get; private set; }

    public Vacina(Guid petId, string nome, string fabricante, DateTime dataAplicacao, DateTime proximaDose)
    {
        ValidateDomain(petId, nome, fabricante, dataAplicacao, proximaDose);
        VacinaId = Guid.NewGuid();
    }

    public void Update(Guid petId, string nome, string fabricante, DateTime dataAplicacao, DateTime proximaDose)
    {
        ValidateDomain(petId, nome, fabricante, dataAplicacao, proximaDose);
    }

    private void ValidateDomain(Guid petId, string nome, string fabricante, DateTime dataAplicacao, DateTime proximaDose)
    {
        DomainExceptionValidation.When(petId == Guid.Empty, "Pet inválido. Pet é obrigatório");
        DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. Nome é obrigatório");
        DomainExceptionValidation.When(nome.Length < 3, "Nome inválido. Nome deve ter no mínimo 3 caracteres");
        DomainExceptionValidation.When(string.IsNullOrEmpty(fabricante), "Fabricante inválido. Fabricante é obrigatório");
        DomainExceptionValidation.When(dataAplicacao == DateTime.MinValue, "Data de aplicação inválida. Data de aplicação é obrigatória");
        DomainExceptionValidation.When(proximaDose != DateTime.MinValue && proximaDose < dataAplicacao, "Próxima dose inválida. Próxima dose deve ser posterior à aplicação");

        PetId = petId;
        Nome = nome;
        Fabricante = fabricante;
        DataAplicacao = dataAplicacao;
        ProximaDose = proximaDose;
    }
}
