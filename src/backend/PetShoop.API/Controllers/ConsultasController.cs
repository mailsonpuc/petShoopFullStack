
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;

namespace PetShoop.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class ConsultasController : ControllerBase
{
    private readonly IConsultaService _consultaService;

    public ConsultasController(IConsultaService consultaService)
    {
        _consultaService = consultaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConsultaDto>>> Get()
    {
        var consultas = await _consultaService.GetConsultas();
        return Ok(consultas);
    }

    [HttpGet("{id}", Name = "GetConsulta")]
    public async Task<ActionResult<ConsultaDto>> Get(Guid id)
    {
        try
        {
            var consulta = await _consultaService.GetById(id);
            return Ok(consulta);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ConsultaDto consultaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _consultaService.Add(consultaDto);

        return new CreatedAtRouteResult("GetConsulta", new { id = consultaDto.ConsultaId }, consultaDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] ConsultaDto consultaDto)
    {
        consultaDto.ConsultaId = id;

        try
        {
            await _consultaService.Update(consultaDto);
            return Ok(consultaDto);
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
    public async Task<ActionResult<ConsultaDto>> Delete(Guid id)
    {
        try
        {
            var consultaDto = await _consultaService.GetById(id);
            await _consultaService.Remove(id);
            return Ok(consultaDto);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
