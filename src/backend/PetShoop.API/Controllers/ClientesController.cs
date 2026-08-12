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
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
    {
        var clientes = await _clienteService.GetClientes();
        return Ok(clientes);
    }

    [HttpGet("{id}", Name = "GetCliente")]
    public async Task<ActionResult<ClienteDto>> Get(Guid id)
    {
        var cliente = await _clienteService.GetById(id);

        if (cliente == null)
        {
            return NotFound();
        }

        return Ok(cliente);
    }


    //paginaçao clientes
    [HttpGet("paginacao")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> Paginacao([FromQuery] ClienteParameters clienteParameters)
    {
        var clientes = await _clienteService.GetClientesPaged(clienteParameters.PageNumber, clienteParameters.PageSize);

        var metadata = new
        {
            clientes.TotalCount,
            clientes.PageSize,
            clientes.CurrentPage,
            clientes.TotalPages,
            clientes.HasNextPage,
            clientes.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));
        return Ok(clientes);
    }



    /// <summary>
    /// Clientes deve se o primeiro a se criado.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ClienteDto clienteDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _clienteService.Add(clienteDto);

        return new CreatedAtRouteResult("GetCliente", new { id = clienteDto.ClienteId }, clienteDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] ClienteDto clienteDto)
    {
        if (id != clienteDto.ClienteId)
        {
            return BadRequest();
        }

        await _clienteService.Update(clienteDto);

        return Ok(clienteDto);
    }


    /// <summary>
    /// Somente Admin pode apagar.
    /// </summary>
    /// <returns>Uma coleção de objetos AgendamentoDto.</returns>
    /// <response code="200">Retorna a lista de agendamentos.</response>
    /// <response code="401">Usuário não autenticado.</response>
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ClienteDto>> Delete(Guid id)
    {
        try
        {
            var clienteDto = await _clienteService.GetById(id);
            if (clienteDto == null)
            {
                return NotFound();
            }

            await _clienteService.Remove(id);
            return Ok(clienteDto);
        }
        catch (DomainExceptionValidation ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}
