using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;

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
        var servico = await _servicoService.GetById(id);

        if (servico == null)
        {
            return NotFound();
        }

        return Ok(servico);
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
        if (id != servicoDto.ServicoId)
        {
            return BadRequest();
        }

        await _servicoService.Update(servicoDto);

        return Ok(servicoDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServicoDto>> Delete(Guid id)
    {
        var servicoDto = await _servicoService.GetById(id);
        if (servicoDto == null)
        {
            return NotFound();
        }

        await _servicoService.Remove(id);
        return Ok(servicoDto);
    }
}
