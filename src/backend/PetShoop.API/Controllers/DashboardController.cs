using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;

namespace PetShoop.API.Controllers;

[Route("api/v1/dashboard")]
[ApiController]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IClienteService _clienteService;
    private readonly IPetService _petService;
    private readonly IFuncionarioService _funcionarioService;
    private readonly IProdutoService _produtoService;
    private readonly IAgendamentoService _agendamentoService;
    private readonly IVendaService _vendaService;

    public DashboardController(
        IClienteService clienteService,
        IPetService petService,
        IFuncionarioService funcionarioService,
        IProdutoService produtoService,
        IAgendamentoService agendamentoService,
        IVendaService vendaService)
    {
        _clienteService = clienteService;
        _petService = petService;
        _funcionarioService = funcionarioService;
        _produtoService = produtoService;
        _agendamentoService = agendamentoService;
        _vendaService = vendaService;
    }

    /// <summary>
    /// Somente Admin pode apagar.
    /// </summary>
    /// <returns>Uma coleção de objetos AgendamentoDto.</returns>
    /// <response code="200">Retorna a lista de agendamentos.</response>
    /// <response code="401">Usuário não autenticado.</response>
    [Authorize(Roles = "admin")]
    [HttpGet]
    public async Task<ActionResult<DashboardDto>> Get()
    {
        var clientes = await _clienteService.GetClientes();
        var pets = await _petService.GetPets();
        var funcionarios = await _funcionarioService.GetFuncionarios();
        var produtos = await _produtoService.GetProdutos();
        var agendamentos = await _agendamentoService.GetAgendamentos();
        var vendas = await _vendaService.GetVendas();

        var hoje = DateTime.Now.Date;

        var dashboard = new DashboardDto
        {
            TotalClientes = clientes.Count(),
            TotalPets = pets.Count(),
            TotalFuncionarios = funcionarios.Count(),
            TotalProdutos = produtos.Count(),
            TotalAgendamentos = agendamentos.Count(),
            TotalVendas = vendas.Count(),
            ReceitaTotal = vendas.Sum(v => v.ValorTotal),
            AgendamentosHoje = agendamentos.Count(a => a.DataHora.Date == hoje),
            AgendamentosPendentes = agendamentos.Count(a => a.Status == Domain.Enums.StatusAgendamento.Agendado)
        };

        return Ok(dashboard);
    }
}
