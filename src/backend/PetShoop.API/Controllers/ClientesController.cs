using Microsoft.AspNetCore.Mvc;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;

namespace PetShoop.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
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

    [HttpDelete("{id}")]
    public async Task<ActionResult<ClienteDto>> Delete(Guid id)
    {
        var clienteDto = await _clienteService.GetById(id);
        if (clienteDto == null)
        {
            return NotFound();
        }

        await _clienteService.Remove(id);
        return Ok(clienteDto);
    }
}
