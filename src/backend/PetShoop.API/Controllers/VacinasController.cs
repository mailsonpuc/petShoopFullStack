
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;

namespace PetShoop.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
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
        try
        {
            var vacina = await _vacinaService.GetById(id);
            return Ok(vacina);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
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

        try
        {
            await _vacinaService.Update(vacinaDto);
            return Ok(vacinaDto);
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
    public async Task<ActionResult<VacinaDto>> Delete(Guid id)
    {
        try
        {
            var vacinaDto = await _vacinaService.GetById(id);
            await _vacinaService.Remove(id);
            return Ok(vacinaDto);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

}
