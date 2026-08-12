using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;

namespace PetShoop.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
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

        if (prontuario == null)
        {
            return NotFound();
        }

        return Ok(prontuario);
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
        if (id != prontuarioDto.ProntuarioId)
        {
            return BadRequest();
        }

        await _prontuarioService.Update(prontuarioDto);

        return Ok(prontuarioDto);
    }


    /// <summary>
    /// Somente Admin pode apagar.
    /// </summary>
    /// <returns>Uma coleção de objetos AgendamentoDto.</returns>
    /// <response code="200">Retorna a lista de agendamentos.</response>
    /// <response code="401">Usuário não autenticado.</response>
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProntuarioDto>> Delete(Guid id)
    {
        var prontuarioDto = await _prontuarioService.GetById(id);
        if (prontuarioDto == null)
        {
            return NotFound();
        }

        await _prontuarioService.Remove(id);
        return Ok(prontuarioDto);
    }
}
