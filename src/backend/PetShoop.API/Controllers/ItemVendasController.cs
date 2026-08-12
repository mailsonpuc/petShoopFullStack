using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;

namespace PetShoop.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class ItemVendasController : ControllerBase
{
    private readonly IItemVendaService _itemVendaService;

    public ItemVendasController(IItemVendaService itemVendaService)
    {
        _itemVendaService = itemVendaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemVendaDto>>> Get()
    {
        var itensVenda = await _itemVendaService.GetItensVendas();
        return Ok(itensVenda);
    }

    [HttpGet("{id}", Name = "GetItemVenda")]
    public async Task<ActionResult<ItemVendaDto>> Get(Guid id)
    {
        var itemVenda = await _itemVendaService.GetById(id);

        if (itemVenda == null)
        {
            return NotFound();
        }

        return Ok(itemVenda);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ItemVendaDto itemVendaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _itemVendaService.Add(itemVendaDto);

        return new CreatedAtRouteResult("GetItemVenda", new { id = itemVendaDto.ItemVendaId }, itemVendaDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] ItemVendaDto itemVendaDto)
    {
        if (id != itemVendaDto.ItemVendaId)
        {
            return BadRequest();
        }

        await _itemVendaService.Update(itemVendaDto);

        return Ok(itemVendaDto);
    }


    /// <summary>
    /// Somente Admin pode apagar.
    /// </summary>
    /// <returns>Uma coleção de objetos AgendamentoDto.</returns>
    /// <response code="200">Retorna a lista de agendamentos.</response>
    /// <response code="401">Usuário não autenticado.</response>
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ItemVendaDto>> Delete(Guid id)
    {
        var itemVendaDto = await _itemVendaService.GetById(id);
        if (itemVendaDto == null)
        {
            return NotFound();
        }

        await _itemVendaService.Remove(id);
        return Ok(itemVendaDto);
    }
}
