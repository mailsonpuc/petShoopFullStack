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
[Authorize]
[EnableRateLimiting("fixedwindow")]
public class ProntuariosController : ControllerBase
{
    private readonly IProntuarioService _prontuarioService;

    public ProntuariosController(IProntuarioService prontuarioService)
    {
        _prontuarioService = prontuarioService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProntuarioDto>>> Get()
    {
        var prontuarios = await _prontuarioService.GetProntuarios();
        return Ok(prontuarios);
    }

    [HttpGet("{id}", Name = "GetProntuario")]
    public async Task<ActionResult<ProntuarioDto>> Get(Guid id)
    {
        var prontuario = await _prontuarioService.GetById(id);
        return Ok(prontuario);
    }

    //paginaçao prontuarios
    [HttpGet("paginacao")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Paginacao([FromQuery] ProntuarioParameters prontuarioParameters)
    {
        var prontuarios = await _prontuarioService.GetProntuariosPaged(prontuarioParameters.PageNumber, prontuarioParameters.PageSize);

        var metadata = new
        {
            prontuarios.TotalCount,
            prontuarios.PageSize,
            prontuarios.CurrentPage,
            prontuarios.TotalPages,
            prontuarios.HasNextPage,
            prontuarios.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));
        return Ok(new { data = prontuarios, pagination = metadata });
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProntuarioDto prontuarioDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _prontuarioService.Add(prontuarioDto);

        return new CreatedAtRouteResult("GetProntuario", new { id = prontuarioDto.ProntuarioId }, prontuarioDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] ProntuarioDto prontuarioDto)
    {
        prontuarioDto.ProntuarioId = id;
        await _prontuarioService.Update(prontuarioDto);
        return Ok(prontuarioDto);
    }


    /// <summary>
    /// Somente Admin pode apagar.
    /// </summary>
    /// <returns>O prontuário excluído.</returns>
    /// <response code="200">Prontuário excluído com sucesso.</response>
    /// <response code="401">Usuário não autenticado.</response>
    /// <response code="403">Apenas administradores podem excluir.</response>
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProntuarioDto>> Delete(Guid id)
    {
        var prontuarioDto = await _prontuarioService.GetById(id);
        await _prontuarioService.Remove(id);
        return Ok(prontuarioDto);
    }
}
