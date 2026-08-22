
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.CrossCutting.Pagination;
using System.Text.Json;

namespace PetShoop.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
[EnableRateLimiting("fixedwindow")]
public class VacinasController : ControllerBase
{
    private readonly IVacinaService _vacinaService;

    public VacinasController(IVacinaService vacinaService)
    {
        _vacinaService = vacinaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VacinaDto>>> Get()
    {
        var vacinas = await _vacinaService.GetVacinas();
        return Ok(vacinas);
    }

    [HttpGet("{id}", Name = "GetVacina")]
    public async Task<ActionResult<VacinaDto>> Get(Guid id)
    {
        var vacina = await _vacinaService.GetById(id);
        return Ok(vacina);
    }

    //paginaçao vacinas
    [HttpGet("paginacao")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Paginacao([FromQuery] VacinaParameters vacinaParameters)
    {
        var vacinas = await _vacinaService.GetVacinasPaged(vacinaParameters.PageNumber, vacinaParameters.PageSize);

        var metadata = new
        {
            vacinas.TotalCount,
            vacinas.PageSize,
            vacinas.CurrentPage,
            vacinas.TotalPages,
            vacinas.HasNextPage,
            vacinas.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));
        return Ok(new { data = vacinas, pagination = metadata });
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] VacinaDto vacinaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _vacinaService.Add(vacinaDto);

        return new CreatedAtRouteResult("GetVacina", new { id = vacinaDto.VacinaId }, vacinaDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] VacinaDto vacinaDto)
    {
        vacinaDto.VacinaId = id;
        await _vacinaService.Update(vacinaDto);
        return Ok(vacinaDto);
    }

    /// <summary>
    /// Somente Admin pode apagar.
    /// </summary>
    /// <returns>A vacina excluída.</returns>
    /// <response code="200">Vacina excluída com sucesso.</response>
    /// <response code="401">Usuário não autenticado.</response>
    /// <response code="403">Apenas administradores podem excluir.</response>
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<VacinaDto>> Delete(Guid id)
    {
        var vacinaDto = await _vacinaService.GetById(id);
        await _vacinaService.Remove(id);
        return Ok(vacinaDto);
    }

}
