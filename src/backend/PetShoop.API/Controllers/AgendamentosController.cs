using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.CrossCutting.Pagination;
using System.Text.Json;

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
        try
        {
            var agendamento = await _agendamentoService.GetById(id);
            return Ok(agendamento);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    //paginaçao agendamentos
    [HttpGet("paginacao")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Paginacao([FromQuery] AgendamentoParameters agendamentoParameters)
    {
        var agendamentos = await _agendamentoService.GetAgendamentosPaged(agendamentoParameters.PageNumber, agendamentoParameters.PageSize);

        var metadata = new
        {
            agendamentos.TotalCount,
            agendamentos.PageSize,
            agendamentos.CurrentPage,
            agendamentos.TotalPages,
            agendamentos.HasNextPage,
            agendamentos.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));
        return Ok(new { data = agendamentos, pagination = metadata });
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
        agendamentoDto.AgendamentoId = id;

        try
        {
            await _agendamentoService.Update(agendamentoDto);
            return Ok(agendamentoDto);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }


    /// <summary>
    /// Somente Admin pode apagar.
    /// </summary>
    /// <returns>Uma coleção de objetos AgendamentoDto.</returns>
    /// <response code="200">Retorna a lista de agendamentos.</response>
    /// <response code="401">Usuário não autenticado.</response>
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<AgendamentoDto>> Delete(Guid id)
    {
        try
        {
            var agendamentoDto = await _agendamentoService.GetById(id);
            await _agendamentoService.Remove(id);
            return Ok(agendamentoDto);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
