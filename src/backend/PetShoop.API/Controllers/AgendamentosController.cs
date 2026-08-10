using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;

namespace PetShoop.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class AgendamentosController : ControllerBase
{
    private readonly IAgendamentoService _agendamentoService;

    public AgendamentosController(IAgendamentoService agendamentoService)
    {
        _agendamentoService = agendamentoService;
    }

    /// <summary>
    /// Obtém a lista de todos os agendamentos cadastrados.
    /// </summary>
    /// <returns>Uma coleção de objetos AgendamentoDto.</returns>
    /// <response code="200">Retorna a lista de agendamentos.</response>
    /// <response code="401">Usuário não autenticado.</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AgendamentoDto>>> Get()
    {
        var agendamentos = await _agendamentoService.GetAgendamentos();
        return Ok(agendamentos);
    }

    [HttpGet("{id}", Name = "GetAgendamento")]
    public async Task<ActionResult<AgendamentoDto>> Get(Guid id)
    {
        var agendamento = await _agendamentoService.GetById(id);

        if (agendamento == null)
        {
            return NotFound();
        }

        return Ok(agendamento);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] AgendamentoDto agendamentoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _agendamentoService.Add(agendamentoDto);

        return new CreatedAtRouteResult("GetAgendamento", new { id = agendamentoDto.AgendamentoId }, agendamentoDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] AgendamentoDto agendamentoDto)
    {
        if (id != agendamentoDto.AgendamentoId)
        {
            return BadRequest();
        }

        await _agendamentoService.Update(agendamentoDto);

        return Ok(agendamentoDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<AgendamentoDto>> Delete(Guid id)
    {
        var agendamentoDto = await _agendamentoService.GetById(id);
        if (agendamentoDto == null)
        {
            return NotFound();
        }

        await _agendamentoService.Remove(id);
        return Ok(agendamentoDto);
    }
}
