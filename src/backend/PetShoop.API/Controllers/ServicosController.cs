using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Domain.Validation;

namespace PetShoop.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class ServicosController : ControllerBase
{
    private readonly IServicoService _servicoService;

    public ServicosController(IServicoService servicoService)
    {
        _servicoService = servicoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServicoDto>>> Get()
    {
        var servicos = await _servicoService.GetServicos();
        return Ok(servicos);
    }

    [HttpGet("{id}", Name = "GetServico")]
    public async Task<ActionResult<ServicoDto>> Get(Guid id)
    {
        try
        {
            var servico = await _servicoService.GetById(id);
            return Ok(servico);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ServicoDto servicoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _servicoService.Add(servicoDto);

        return new CreatedAtRouteResult("GetServico", new { id = servicoDto.ServicoId }, servicoDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] ServicoDto servicoDto)
    {
        servicoDto.ServicoId = id;

        try
        {
            await _servicoService.Update(servicoDto);
            return Ok(servicoDto);
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
    public async Task<ActionResult<ServicoDto>> Delete(Guid id)
    {
        try
        {
            var servicoDto = await _servicoService.GetById(id);
            await _servicoService.Remove(id);
            return Ok(servicoDto);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
