using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.CrossCutting.Pagination;
using PetShoop.Domain.Validation;
using System.Text.Json;

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
        try
        {
            var venda = await _vendaService.GetById(id);
            return Ok(venda);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    //paginaçao vendas
    [HttpGet("paginacao")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Paginacao([FromQuery] VendaParameters vendaParameters)
    {
        var vendas = await _vendaService.GetVendasPaged(vendaParameters.PageNumber, vendaParameters.PageSize);

        var metadata = new
        {
            vendas.TotalCount,
            vendas.PageSize,
            vendas.CurrentPage,
            vendas.TotalPages,
            vendas.HasNextPage,
            vendas.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));
        return Ok(new { data = vendas, pagination = metadata });
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
        vendaDto.VendaId = id;

        try
        {
            await _vendaService.Update(vendaDto);
            return Ok(vendaDto);
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
    public async Task<ActionResult<VendaDto>> Delete(Guid id)
    {
        try
        {
            var vendaDto = await _vendaService.GetById(id);
            await _vendaService.Remove(id);
            return Ok(vendaDto);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
