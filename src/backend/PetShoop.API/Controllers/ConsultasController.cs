
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.CrossCutting.Pagination;
using System.Text.Json;

namespace PetShoop.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize(Roles = "admin")]
[EnableRateLimiting("fixedwindow")]
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
        var consulta = await _consultaService.GetById(id);
        return Ok(consulta);
    }

    //paginaçao consultas
    [HttpGet("paginacao")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Paginacao([FromQuery] ConsultaParameters consultaParameters)
    {
        var consultas = await _consultaService.GetConsultasPaged(consultaParameters.PageNumber, consultaParameters.PageSize);

        var metadata = new
        {
            consultas.TotalCount,
            consultas.PageSize,
            consultas.CurrentPage,
            consultas.TotalPages,
            consultas.HasNextPage,
            consultas.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));
        return Ok(new { data = consultas, pagination = metadata });
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
        await _consultaService.Update(consultaDto);
        return Ok(consultaDto);
    }


    /// <summary>
    /// Somente Admin pode apagar.
    /// </summary>
    /// <returns>A consulta excluída.</returns>
    /// <response code="200">Consulta excluída com sucesso.</response>
    /// <response code="401">Usuário não autenticado.</response>
    /// <response code="403">Apenas administradores podem excluir.</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ConsultaDto>> Delete(Guid id)
    {
        var consultaDto = await _consultaService.GetById(id);
        await _consultaService.Remove(id);
        return Ok(consultaDto);
    }
}
