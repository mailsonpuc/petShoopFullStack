using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Memory;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.CrossCutting.Pagination;
using System.Text.Json;

namespace PetShoop.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize(Roles = "admin")]
[EnableRateLimiting("fixedwindow")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;
    private readonly IMemoryCache _memoryCache;
    private const string ClientesKey = "CacheClientes";

    public ClientesController(IClienteService clienteService, IMemoryCache memoryCache)
    {
        _clienteService = clienteService;
        _memoryCache = memoryCache;
    }

    /// <summary>
    /// Obtém a lista de todos os Clientes cadastrados. Com cache de 30 segundos.
    /// </summary>
    /// <returns>Uma coleção de objetos ClienteDto.</returns>
    /// <response code="200">Retorna a lista de clientes.</response>
    /// <response code="401">Usuário não autenticado.</response>

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
    {
        if (_memoryCache.TryGetValue(ClientesKey, out IEnumerable<ClienteDto>? cachedClientes))
        {
            return Ok(cachedClientes);
        }

        var clientes = await _clienteService.GetClientes();

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(30))
            .SetSlidingExpiration(TimeSpan.FromSeconds(15))
            .SetPriority(CacheItemPriority.High);

        _memoryCache.Set(ClientesKey, clientes, cacheEntryOptions);
        return Ok(clientes);
    }

    [HttpGet("{id}", Name = "GetCliente")]
    public async Task<ActionResult<ClienteDto>> Get(Guid id)
    {
        var cliente = await _clienteService.GetById(id);
        return Ok(cliente);
    }


    //paginaçao clientes
    [HttpGet("paginacao")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Paginacao([FromQuery] ClienteParameters clienteParameters)
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
        return Ok(new { data = clientes, pagination = metadata });
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
        clienteDto.ClienteId = id;
        await _clienteService.Update(clienteDto);
        return Ok(clienteDto);
    }


    /// <summary>
    /// Somente Admin pode apagar.
    /// </summary>
    /// <returns>O cliente excluído.</returns>
    /// <response code="200">Cliente excluído com sucesso.</response>
    /// <response code="401">Usuário não autenticado.</response>
    /// <response code="403">Apenas administradores podem excluir.</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ClienteDto>> Delete(Guid id)
    {
        var clienteDto = await _clienteService.GetById(id);
        await _clienteService.Remove(id);
        return Ok(clienteDto);
    }
}
