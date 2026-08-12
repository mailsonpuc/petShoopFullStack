using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;

namespace PetShoop.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class VendasController : ControllerBase
{
    private readonly IVendaService _vendaService;

    public VendasController(IVendaService vendaService)
    {
        _vendaService = vendaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VendaDto>>> Get()
    {
        var vendas = await _vendaService.GetVendas();
        return Ok(vendas);
    }

    [HttpGet("{id}", Name = "GetVenda")]
    public async Task<ActionResult<VendaDto>> Get(Guid id)
    {
        var venda = await _vendaService.GetById(id);

        if (venda == null)
        {
            return NotFound();
        }

        return Ok(venda);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] VendaDto vendaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _vendaService.Add(vendaDto);

        return new CreatedAtRouteResult("GetVenda", new { id = vendaDto.VendaId }, vendaDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] VendaDto vendaDto)
    {
        if (id != vendaDto.VendaId)
        {
            return BadRequest();
        }

        await _vendaService.Update(vendaDto);

        return Ok(vendaDto);
    }

    /// <summary>
    /// Somente Admin pode apagar.
    /// </summary>
    /// <returns>Uma coleção de objetos AgendamentoDto.</returns>
    /// <response code="200">Retorna a lista de agendamentos.</response>
    /// <response code="401">Usuário não autenticado.</response>
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<VendaDto>> Delete(Guid id)
    {
        var vendaDto = await _vendaService.GetById(id);
        if (vendaDto == null)
        {
            return NotFound();
        }

        await _vendaService.Remove(id);
        return Ok(vendaDto);
    }
}
