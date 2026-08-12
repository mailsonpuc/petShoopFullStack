
using PetShoop.Domain.Validation;

namespace PetShoop.Domain.Entities;

public sealed class Servico
{
    public Guid ServicoId { get; private set; }


    public string Nome { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public decimal Preco { get; private set; }
    public int DuracaoEmMinutos { get; private set; }

    public Servico(string nome, string descricao, decimal preco, int duracaoEmMinutos)
    {
        ValidateDomain(nome, descricao, preco, duracaoEmMinutos);
        ServicoId = Guid.NewGuid();
    }

    public void Update(string nome, string descricao, decimal preco, int duracaoEmMinutos)
    {
        ValidateDomain(nome, descricao, preco, duracaoEmMinutos);
    }

    public void SetServicoId(Guid servicoId)
    {
        ServicoId = servicoId;
    }

    private void ValidateDomain(string nome, string descricao, decimal preco, int duracaoEmMinutos)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome inválido. Nome é obrigatório");
        DomainExceptionValidation.When(nome.Length < 3, "Nome inválido. Nome deve ter no mínimo 3 caracteres");
        DomainExceptionValidation.When(string.IsNullOrEmpty(descricao), "Descrição inválida. Descrição é obrigatória");
        DomainExceptionValidation.When(preco <= 0, "Preço inválido. Preço deve ser maior que zero");
        DomainExceptionValidation.When(duracaoEmMinutos <= 0, "Duração inválida. Duração deve ser maior que zero");

        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        DuracaoEmMinutos = duracaoEmMinutos;
    }
}
