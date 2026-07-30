using PetShoop.Domain.Entities;
using PetShoop.Domain.Enums;
using PetShoop.Domain.Validation;

namespace PetShoop.Domain.Tests.Entities;

public class DomainEntitiesTests
{
    private static readonly Guid ClienteId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid PetId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    private static readonly Guid ServicoId = Guid.Parse("33333333-3333-3333-3333-333333333333");
    private static readonly Guid FuncionarioId = Guid.Parse("44444444-4444-4444-4444-444444444444");
    private static readonly Guid VendaId = Guid.Parse("55555555-5555-5555-5555-555555555555");
    private static readonly Guid ProdutoId = Guid.Parse("66666666-6666-6666-6666-666666666666");

    [Fact]
    public void Cliente_DeveCriarComDadosValidos()
    {
        var dataNascimento = new DateTime(1995, 8, 15);

        var cliente = new Cliente("Maria Souza", "123.456.789-34", "maria@gmail.com", "81999999999", dataNascimento, "Rua A");

        Assert.NotEqual(Guid.Empty, cliente.ClienteId);
        Assert.Equal("Maria Souza", cliente.Nome);
        Assert.Equal("123.456.789-34", cliente.Cpf);
        Assert.Equal("maria@gmail.com", cliente.Email);
        Assert.Equal("81999999999", cliente.Telefone);
        Assert.Equal(dataNascimento, cliente.DataDeNascimento);
        Assert.Equal("Rua A", cliente.Endereco);
        Assert.NotEqual(DateTime.MinValue, cliente.DataDeCadastro);
    }

    [Fact]
    public void Cliente_DeveAtualizarDadosValidos()
    {
        var cliente = new Cliente("Maria Souza", "123", "maria@gmail.com", "81", new DateTime(1995, 8, 15), "Rua A");

        cliente.Update("Joao Silva", "456", "joao@gmail.com", "82", new DateTime(1990, 1, 10), "Rua B");

        Assert.Equal("Joao Silva", cliente.Nome);
        Assert.Equal("456", cliente.Cpf);
        Assert.Equal("joao@gmail.com", cliente.Email);
        Assert.Equal("82", cliente.Telefone);
        Assert.Equal(new DateTime(1990, 1, 10), cliente.DataDeNascimento);
        Assert.Equal("Rua B", cliente.Endereco);
    }

    [Theory]
    [MemberData(nameof(ClienteInvalido))]
    public void Cliente_DeveLancarExcecaoParaDadosInvalidos(Action act, string mensagem)
    {
        var exception = Assert.Throws<DomainExceptionValidation>(act);

        Assert.Equal(mensagem, exception.Message);
    }

    public static IEnumerable<object[]> ClienteInvalido()
    {
        yield return Invalid(() => new Cliente("", "123", "email@test.com", "81", new DateTime(1995, 8, 15), "Rua A"), "Nome inválido. Nome é obrigatório");
        yield return Invalid(() => new Cliente("Ma", "123", "email@test.com", "81", new DateTime(1995, 8, 15), "Rua A"), "Nome inválido. Nome deve ter no mínimo 3 caracteres");
        yield return Invalid(() => new Cliente("Maria", "", "email@test.com", "81", new DateTime(1995, 8, 15), "Rua A"), "CPF inválido. CPF é obrigatório");
        yield return Invalid(() => new Cliente("Maria", "123", "", "81", new DateTime(1995, 8, 15), "Rua A"), "Email inválido. Email é obrigatório");
        yield return Invalid(() => new Cliente("Maria", "123", "email@test.com", "", new DateTime(1995, 8, 15), "Rua A"), "Telefone inválido. Telefone é obrigatório");
        yield return Invalid(() => new Cliente("Maria", "123", "email@test.com", "81", DateTime.MinValue, "Rua A"), "Data de nascimento inválida. Data de nascimento é obrigatória");
        yield return Invalid(() => new Cliente("Maria", "123", "email@test.com", "81", new DateTime(1995, 8, 15), ""), "Endereço inválido. Endereço é obrigatório");
    }

    [Fact]
    public void Pet_DeveCriarEAtualizarComDadosValidos()
    {
        var pet = new Pet("Bidu", EspeciePet.Cachorro, "SRD", SexoPet.Macho, new DateTime(2020, 5, 1), 12.5m, "Caramelo", PortePet.Medio, "Calmo", ClienteId);

        pet.Update("Mimi", EspeciePet.Gato, "Siamês", SexoPet.Femea, new DateTime(2021, 2, 3), 4.2m, "Branco", PortePet.Pequeno, "Arisca", ClienteId);

        Assert.NotEqual(Guid.Empty, pet.PetId);
        Assert.Equal("Mimi", pet.Nome);
        Assert.Equal(EspeciePet.Gato, pet.Especie);
        Assert.Equal("Siamês", pet.Raca);
        Assert.Equal(SexoPet.Femea, pet.Sexo);
        Assert.Equal(new DateTime(2021, 2, 3), pet.DataDeNascimento);
        Assert.Equal(4.2m, pet.Peso);
        Assert.Equal("Branco", pet.Cor);
        Assert.Equal(PortePet.Pequeno, pet.Porte);
        Assert.Equal("Arisca", pet.Observacoes);
        Assert.Equal(ClienteId, pet.ClienteId);
    }

    [Theory]
    [MemberData(nameof(PetInvalido))]
    public void Pet_DeveLancarExcecaoParaDadosInvalidos(Action act, string mensagem)
    {
        var exception = Assert.Throws<DomainExceptionValidation>(act);

        Assert.Equal(mensagem, exception.Message);
    }

    public static IEnumerable<object[]> PetInvalido()
    {
        yield return Invalid(() => new Pet("", EspeciePet.Cachorro, "SRD", SexoPet.Macho, new DateTime(2020, 5, 1), 12m, "Caramelo", PortePet.Medio, "", ClienteId), "Nome inválido. Nome é obrigatório");
        yield return Invalid(() => new Pet("A", EspeciePet.Cachorro, "SRD", SexoPet.Macho, new DateTime(2020, 5, 1), 12m, "Caramelo", PortePet.Medio, "", ClienteId), "Nome inválido. Nome deve ter no mínimo 2 caracteres");
        yield return Invalid(() => new Pet("Bidu", (EspeciePet)99, "SRD", SexoPet.Macho, new DateTime(2020, 5, 1), 12m, "Caramelo", PortePet.Medio, "", ClienteId), "Espécie inválida");
        yield return Invalid(() => new Pet("Bidu", EspeciePet.Cachorro, "", SexoPet.Macho, new DateTime(2020, 5, 1), 12m, "Caramelo", PortePet.Medio, "", ClienteId), "Raça inválida. Raça é obrigatória");
        yield return Invalid(() => new Pet("Bidu", EspeciePet.Cachorro, "SRD", (SexoPet)99, new DateTime(2020, 5, 1), 12m, "Caramelo", PortePet.Medio, "", ClienteId), "Sexo inválido");
        yield return Invalid(() => new Pet("Bidu", EspeciePet.Cachorro, "SRD", SexoPet.Macho, DateTime.MinValue, 12m, "Caramelo", PortePet.Medio, "", ClienteId), "Data de nascimento inválida. Data de nascimento é obrigatória");
        yield return Invalid(() => new Pet("Bidu", EspeciePet.Cachorro, "SRD", SexoPet.Macho, new DateTime(2020, 5, 1), 0, "Caramelo", PortePet.Medio, "", ClienteId), "Peso inválido. Peso deve ser maior que zero");
        yield return Invalid(() => new Pet("Bidu", EspeciePet.Cachorro, "SRD", SexoPet.Macho, new DateTime(2020, 5, 1), 12m, "", PortePet.Medio, "", ClienteId), "Cor inválida. Cor é obrigatória");
        yield return Invalid(() => new Pet("Bidu", EspeciePet.Cachorro, "SRD", SexoPet.Macho, new DateTime(2020, 5, 1), 12m, "Caramelo", (PortePet)99, "", ClienteId), "Porte inválido");
        yield return Invalid(() => new Pet("Bidu", EspeciePet.Cachorro, "SRD", SexoPet.Macho, new DateTime(2020, 5, 1), 12m, "Caramelo", PortePet.Medio, "", Guid.Empty), "Cliente inválido. Cliente é obrigatório");
    }

    [Fact]
    public void Funcionario_DeveCriarEAtualizarComDadosValidos()
    {
        var funcionario = new Funcionario("Ana Lima", "123", "ana@test.com", "81", CargoFuncionario.Atendente, 2000m, new DateTime(2024, 1, 2));

        funcionario.Update("Carlos Lima", "456", "carlos@test.com", "82", CargoFuncionario.Veterinario, 5000m, new DateTime(2024, 3, 4));

        Assert.NotEqual(Guid.Empty, funcionario.FuncionarioId);
        Assert.Equal("Carlos Lima", funcionario.Nome);
        Assert.Equal("456", funcionario.Cpf);
        Assert.Equal("carlos@test.com", funcionario.Email);
        Assert.Equal("82", funcionario.Telefone);
        Assert.Equal(CargoFuncionario.Veterinario, funcionario.Cargo);
        Assert.Equal(5000m, funcionario.Salario);
        Assert.Equal(new DateTime(2024, 3, 4), funcionario.DataAdmissao);
    }

    [Theory]
    [MemberData(nameof(FuncionarioInvalido))]
    public void Funcionario_DeveLancarExcecaoParaDadosInvalidos(Action act, string mensagem)
    {
        var exception = Assert.Throws<DomainExceptionValidation>(act);

        Assert.Equal(mensagem, exception.Message);
    }

    public static IEnumerable<object[]> FuncionarioInvalido()
    {
        yield return Invalid(() => new Funcionario("", "123", "ana@test.com", "81", CargoFuncionario.Atendente, 2000m, new DateTime(2024, 1, 2)), "Nome inválido. Nome é obrigatório");
        yield return Invalid(() => new Funcionario("An", "123", "ana@test.com", "81", CargoFuncionario.Atendente, 2000m, new DateTime(2024, 1, 2)), "Nome inválido. Nome deve ter no mínimo 3 caracteres");
        yield return Invalid(() => new Funcionario("Ana", "", "ana@test.com", "81", CargoFuncionario.Atendente, 2000m, new DateTime(2024, 1, 2)), "CPF inválido. CPF é obrigatório");
        yield return Invalid(() => new Funcionario("Ana", "123", "", "81", CargoFuncionario.Atendente, 2000m, new DateTime(2024, 1, 2)), "Email inválido. Email é obrigatório");
        yield return Invalid(() => new Funcionario("Ana", "123", "ana@test.com", "", CargoFuncionario.Atendente, 2000m, new DateTime(2024, 1, 2)), "Telefone inválido. Telefone é obrigatório");
        yield return Invalid(() => new Funcionario("Ana", "123", "ana@test.com", "81", (CargoFuncionario)99, 2000m, new DateTime(2024, 1, 2)), "Cargo inválido");
        yield return Invalid(() => new Funcionario("Ana", "123", "ana@test.com", "81", CargoFuncionario.Atendente, 0, new DateTime(2024, 1, 2)), "Salário inválido. Salário deve ser maior que zero");
        yield return Invalid(() => new Funcionario("Ana", "123", "ana@test.com", "81", CargoFuncionario.Atendente, 2000m, DateTime.MinValue), "Data de admissão inválida. Data de admissão é obrigatória");
    }

    [Fact]
    public void Produto_DeveCriarEAtualizarComDadosValidos()
    {
        var produto = new Produto("Ração", "Ração premium", CategoriaProduto.Racao, "BoaPet", 100m, 5);

        produto.Update("Coleira", "Coleira ajustável", CategoriaProduto.Acessorio, "PetMais", 35m, 8);

        Assert.NotEqual(Guid.Empty, produto.ProdutoId);
        Assert.Equal("Coleira", produto.Nome);
        Assert.Equal("Coleira ajustável", produto.Descricao);
        Assert.Equal(CategoriaProduto.Acessorio, produto.Categoria);
        Assert.Equal("PetMais", produto.Marca);
        Assert.Equal(35m, produto.Preco);
        Assert.Equal(8, produto.QuantidadeEmEstoque);
    }

    [Theory]
    [MemberData(nameof(ProdutoInvalido))]
    public void Produto_DeveLancarExcecaoParaDadosInvalidos(Action act, string mensagem)
    {
        var exception = Assert.Throws<DomainExceptionValidation>(act);

        Assert.Equal(mensagem, exception.Message);
    }

    public static IEnumerable<object[]> ProdutoInvalido()
    {
        yield return Invalid(() => new Produto("", "Desc", CategoriaProduto.Racao, "Marca", 10m, 1), "Nome inválido. Nome é obrigatório");
        yield return Invalid(() => new Produto("Ra", "Desc", CategoriaProduto.Racao, "Marca", 10m, 1), "Nome inválido. Nome deve ter no mínimo 3 caracteres");
        yield return Invalid(() => new Produto("Ração", "", CategoriaProduto.Racao, "Marca", 10m, 1), "Descrição inválida. Descrição é obrigatória");
        yield return Invalid(() => new Produto("Ração", "Desc", (CategoriaProduto)99, "Marca", 10m, 1), "Categoria inválida");
        yield return Invalid(() => new Produto("Ração", "Desc", CategoriaProduto.Racao, "", 10m, 1), "Marca inválida. Marca é obrigatória");
        yield return Invalid(() => new Produto("Ração", "Desc", CategoriaProduto.Racao, "Marca", 0, 1), "Preço inválido. Preço deve ser maior que zero");
        yield return Invalid(() => new Produto("Ração", "Desc", CategoriaProduto.Racao, "Marca", 10m, -1), "Quantidade em estoque inválida. Quantidade não pode ser negativa");
    }

    [Fact]
    public void Servico_DeveCriarEAtualizarComDadosValidos()
    {
        var servico = new Servico("Banho", "Banho simples", 50m, 40);

        servico.Update("Tosa", "Tosa higiênica", 70m, 60);

        Assert.NotEqual(Guid.Empty, servico.ServicoId);
        Assert.Equal("Tosa", servico.Nome);
        Assert.Equal("Tosa higiênica", servico.Descricao);
        Assert.Equal(70m, servico.Preco);
        Assert.Equal(60, servico.DuracaoEmMinutos);
    }

    [Theory]
    [MemberData(nameof(ServicoInvalido))]
    public void Servico_DeveLancarExcecaoParaDadosInvalidos(Action act, string mensagem)
    {
        var exception = Assert.Throws<DomainExceptionValidation>(act);

        Assert.Equal(mensagem, exception.Message);
    }

    public static IEnumerable<object[]> ServicoInvalido()
    {
        yield return Invalid(() => new Servico("", "Desc", 50m, 40), "Nome inválido. Nome é obrigatório");
        yield return Invalid(() => new Servico("Ba", "Desc", 50m, 40), "Nome inválido. Nome deve ter no mínimo 3 caracteres");
        yield return Invalid(() => new Servico("Banho", "", 50m, 40), "Descrição inválida. Descrição é obrigatória");
        yield return Invalid(() => new Servico("Banho", "Desc", 0, 40), "Preço inválido. Preço deve ser maior que zero");
        yield return Invalid(() => new Servico("Banho", "Desc", 50m, 0), "Duração inválida. Duração deve ser maior que zero");
    }

    [Fact]
    public void Agendamento_DeveCriarEAtualizarComDadosValidos()
    {
        var agendamento = new Agendamento(PetId, ServicoId, FuncionarioId, new DateTime(2026, 1, 2, 10, 0, 0), StatusAgendamento.Agendado, "Primeira visita");

        agendamento.Update(PetId, ServicoId, FuncionarioId, new DateTime(2026, 1, 3, 11, 0, 0), StatusAgendamento.Confirmado, "Confirmado");

        Assert.NotEqual(Guid.Empty, agendamento.AgendamentoId);
        Assert.Equal(PetId, agendamento.PetId);
        Assert.Equal(ServicoId, agendamento.ServicoId);
        Assert.Equal(FuncionarioId, agendamento.FuncionarioId);
        Assert.Equal(new DateTime(2026, 1, 3, 11, 0, 0), agendamento.DataHora);
        Assert.Equal(StatusAgendamento.Confirmado, agendamento.Status);
        Assert.Equal("Confirmado", agendamento.Observacoes);
    }

    [Theory]
    [MemberData(nameof(AgendamentoInvalido))]
    public void Agendamento_DeveLancarExcecaoParaDadosInvalidos(Action act, string mensagem)
    {
        var exception = Assert.Throws<DomainExceptionValidation>(act);

        Assert.Equal(mensagem, exception.Message);
    }

    public static IEnumerable<object[]> AgendamentoInvalido()
    {
        yield return Invalid(() => new Agendamento(Guid.Empty, ServicoId, FuncionarioId, new DateTime(2026, 1, 2, 10, 0, 0), StatusAgendamento.Agendado, ""), "Pet inválido. Pet é obrigatório");
        yield return Invalid(() => new Agendamento(PetId, Guid.Empty, FuncionarioId, new DateTime(2026, 1, 2, 10, 0, 0), StatusAgendamento.Agendado, ""), "Serviço inválido. Serviço é obrigatório");
        yield return Invalid(() => new Agendamento(PetId, ServicoId, Guid.Empty, new DateTime(2026, 1, 2, 10, 0, 0), StatusAgendamento.Agendado, ""), "Funcionário inválido. Funcionário é obrigatório");
        yield return Invalid(() => new Agendamento(PetId, ServicoId, FuncionarioId, DateTime.MinValue, StatusAgendamento.Agendado, ""), "Data e hora inválidas. Data e hora são obrigatórias");
        yield return Invalid(() => new Agendamento(PetId, ServicoId, FuncionarioId, new DateTime(2026, 1, 2, 10, 0, 0), (StatusAgendamento)99, ""), "Status inválido");
    }

    [Fact]
    public void Consulta_DeveCriarEAtualizarComDadosValidos()
    {
        var consulta = new Consulta(PetId, FuncionarioId, new DateTime(2026, 1, 4), 12m, 38.5m, "Saudável", "Repouso");

        consulta.Update(PetId, FuncionarioId, new DateTime(2026, 1, 5), 11.8m, 38.2m, "Melhora", "Acompanhamento");

        Assert.NotEqual(Guid.Empty, consulta.ConsultaId);
        Assert.Equal(PetId, consulta.PetId);
        Assert.Equal(FuncionarioId, consulta.FuncionarioId);
        Assert.Equal(new DateTime(2026, 1, 5), consulta.DataConsulta);
        Assert.Equal(11.8m, consulta.Peso);
        Assert.Equal(38.2m, consulta.Temperatura);
        Assert.Equal("Melhora", consulta.Diagnostico);
        Assert.Equal("Acompanhamento", consulta.Prescricao);
    }

    [Theory]
    [MemberData(nameof(ConsultaInvalida))]
    public void Consulta_DeveLancarExcecaoParaDadosInvalidos(Action act, string mensagem)
    {
        var exception = Assert.Throws<DomainExceptionValidation>(act);

        Assert.Equal(mensagem, exception.Message);
    }

    public static IEnumerable<object[]> ConsultaInvalida()
    {
        yield return Invalid(() => new Consulta(Guid.Empty, FuncionarioId, new DateTime(2026, 1, 4), 12m, 38m, "Ok", "Repouso"), "Pet inválido. Pet é obrigatório");
        yield return Invalid(() => new Consulta(PetId, Guid.Empty, new DateTime(2026, 1, 4), 12m, 38m, "Ok", "Repouso"), "Funcionário inválido. Funcionário é obrigatório");
        yield return Invalid(() => new Consulta(PetId, FuncionarioId, DateTime.MinValue, 12m, 38m, "Ok", "Repouso"), "Data da consulta inválida. Data da consulta é obrigatória");
        yield return Invalid(() => new Consulta(PetId, FuncionarioId, new DateTime(2026, 1, 4), 0, 38m, "Ok", "Repouso"), "Peso inválido. Peso deve ser maior que zero");
        yield return Invalid(() => new Consulta(PetId, FuncionarioId, new DateTime(2026, 1, 4), 12m, 0, "Ok", "Repouso"), "Temperatura inválida. Temperatura deve ser maior que zero");
        yield return Invalid(() => new Consulta(PetId, FuncionarioId, new DateTime(2026, 1, 4), 12m, 38m, "", "Repouso"), "Diagnóstico inválido. Diagnóstico é obrigatório");
        yield return Invalid(() => new Consulta(PetId, FuncionarioId, new DateTime(2026, 1, 4), 12m, 38m, "Ok", ""), "Prescrição inválida. Prescrição é obrigatória");
    }

    [Fact]
    public void ItemVenda_DeveCriarEAtualizarComDadosValidos()
    {
        var itemVenda = new ItemVenda(VendaId, ProdutoId, 2, 25m);

        itemVenda.Update(VendaId, ProdutoId, 3, 30m);

        Assert.NotEqual(Guid.Empty, itemVenda.ItemVendaId);
        Assert.Equal(VendaId, itemVenda.VendaId);
        Assert.Equal(ProdutoId, itemVenda.ProdutoId);
        Assert.Equal(3, itemVenda.Quantidade);
        Assert.Equal(30m, itemVenda.ValorUnitario);
    }

    [Theory]
    [MemberData(nameof(ItemVendaInvalido))]
    public void ItemVenda_DeveLancarExcecaoParaDadosInvalidos(Action act, string mensagem)
    {
        var exception = Assert.Throws<DomainExceptionValidation>(act);

        Assert.Equal(mensagem, exception.Message);
    }

    public static IEnumerable<object[]> ItemVendaInvalido()
    {
        yield return Invalid(() => new ItemVenda(Guid.Empty, ProdutoId, 1, 10m), "Venda inválida. Venda é obrigatória");
        yield return Invalid(() => new ItemVenda(VendaId, Guid.Empty, 1, 10m), "Produto inválido. Produto é obrigatório");
        yield return Invalid(() => new ItemVenda(VendaId, ProdutoId, 0, 10m), "Quantidade inválida. Quantidade deve ser maior que zero");
        yield return Invalid(() => new ItemVenda(VendaId, ProdutoId, 1, 0), "Valor unitário inválido. Valor unitário deve ser maior que zero");
    }

    [Fact]
    public void Venda_DeveCriarEAtualizarComDadosValidos()
    {
        var venda = new Venda(ClienteId, new DateTime(2026, 1, 6), 100m, FormaPagamento.Pix);

        venda.Update(ClienteId, new DateTime(2026, 1, 7), 0m, FormaPagamento.CartaoCredito);

        Assert.NotEqual(Guid.Empty, venda.VendaId);
        Assert.Equal(ClienteId, venda.ClienteId);
        Assert.Equal(new DateTime(2026, 1, 7), venda.DataVenda);
        Assert.Equal(0m, venda.ValorTotal);
        Assert.Equal(FormaPagamento.CartaoCredito, venda.FormaPagamento);
    }

    [Theory]
    [MemberData(nameof(VendaInvalida))]
    public void Venda_DeveLancarExcecaoParaDadosInvalidos(Action act, string mensagem)
    {
        var exception = Assert.Throws<DomainExceptionValidation>(act);

        Assert.Equal(mensagem, exception.Message);
    }

    public static IEnumerable<object[]> VendaInvalida()
    {
        yield return Invalid(() => new Venda(Guid.Empty, new DateTime(2026, 1, 6), 100m, FormaPagamento.Pix), "Cliente inválido. Cliente é obrigatório");
        yield return Invalid(() => new Venda(ClienteId, DateTime.MinValue, 100m, FormaPagamento.Pix), "Data da venda inválida. Data da venda é obrigatória");
        yield return Invalid(() => new Venda(ClienteId, new DateTime(2026, 1, 6), -1m, FormaPagamento.Pix), "Valor total inválido. Valor total não pode ser negativo");
        yield return Invalid(() => new Venda(ClienteId, new DateTime(2026, 1, 6), 100m, (FormaPagamento)99), "Forma de pagamento inválida");
    }

    [Fact]
    public void Vacina_DeveCriarEAtualizarComDadosValidos()
    {
        var vacina = new Vacina(PetId, "V10", "LabPet", new DateTime(2026, 1, 8), DateTime.MinValue);

        vacina.Update(PetId, "Raiva", "VetLab", new DateTime(2026, 1, 9), new DateTime(2026, 2, 9));

        Assert.NotEqual(Guid.Empty, vacina.VacinaId);
        Assert.Equal(PetId, vacina.PetId);
        Assert.Equal("Raiva", vacina.Nome);
        Assert.Equal("VetLab", vacina.Fabricante);
        Assert.Equal(new DateTime(2026, 1, 9), vacina.DataAplicacao);
        Assert.Equal(new DateTime(2026, 2, 9), vacina.ProximaDose);
    }

    [Theory]
    [MemberData(nameof(VacinaInvalida))]
    public void Vacina_DeveLancarExcecaoParaDadosInvalidos(Action act, string mensagem)
    {
        var exception = Assert.Throws<DomainExceptionValidation>(act);

        Assert.Equal(mensagem, exception.Message);
    }

    public static IEnumerable<object[]> VacinaInvalida()
    {
        yield return Invalid(() => new Vacina(Guid.Empty, "V10", "LabPet", new DateTime(2026, 1, 8), DateTime.MinValue), "Pet inválido. Pet é obrigatório");
        yield return Invalid(() => new Vacina(PetId, "", "LabPet", new DateTime(2026, 1, 8), DateTime.MinValue), "Nome inválido. Nome é obrigatório");
        yield return Invalid(() => new Vacina(PetId, "V1", "LabPet", new DateTime(2026, 1, 8), DateTime.MinValue), "Nome inválido. Nome deve ter no mínimo 3 caracteres");
        yield return Invalid(() => new Vacina(PetId, "V10", "", new DateTime(2026, 1, 8), DateTime.MinValue), "Fabricante inválido. Fabricante é obrigatório");
        yield return Invalid(() => new Vacina(PetId, "V10", "LabPet", DateTime.MinValue, DateTime.MinValue), "Data de aplicação inválida. Data de aplicação é obrigatória");
        yield return Invalid(() => new Vacina(PetId, "V10", "LabPet", new DateTime(2026, 1, 8), new DateTime(2026, 1, 7)), "Próxima dose inválida. Próxima dose deve ser posterior à aplicação");
    }

    [Fact]
    public void Prontuario_DeveCriarEAtualizarComDadosValidos()
    {
        var prontuario = new Prontuario(PetId, FuncionarioId, new DateTime(2026, 1, 10), "Registro inicial");

        prontuario.Update(PetId, FuncionarioId, new DateTime(2026, 1, 11), "Registro atualizado");

        Assert.NotEqual(Guid.Empty, prontuario.ProntuarioId);
        Assert.Equal(PetId, prontuario.PetId);
        Assert.Equal(FuncionarioId, prontuario.FuncionarioId);
        Assert.Equal(new DateTime(2026, 1, 11), prontuario.DataRegistro);
        Assert.Equal("Registro atualizado", prontuario.Descricao);
    }

    [Theory]
    [MemberData(nameof(ProntuarioInvalido))]
    public void Prontuario_DeveLancarExcecaoParaDadosInvalidos(Action act, string mensagem)
    {
        var exception = Assert.Throws<DomainExceptionValidation>(act);

        Assert.Equal(mensagem, exception.Message);
    }

    public static IEnumerable<object[]> ProntuarioInvalido()
    {
        yield return Invalid(() => new Prontuario(Guid.Empty, FuncionarioId, new DateTime(2026, 1, 10), "Registro"), "Pet inválido. Pet é obrigatório");
        yield return Invalid(() => new Prontuario(PetId, Guid.Empty, new DateTime(2026, 1, 10), "Registro"), "Funcionário inválido. Funcionário é obrigatório");
        yield return Invalid(() => new Prontuario(PetId, FuncionarioId, DateTime.MinValue, "Registro"), "Data de registro inválida. Data de registro é obrigatória");
        yield return Invalid(() => new Prontuario(PetId, FuncionarioId, new DateTime(2026, 1, 10), ""), "Descrição inválida. Descrição é obrigatória");
    }

    private static object[] Invalid(Action act, string mensagem)
    {
        return new object[] { act, mensagem };
    }
}
