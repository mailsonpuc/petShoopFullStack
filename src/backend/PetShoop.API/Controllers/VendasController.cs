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
public class VendasController : ControllerBase
{
    private readonly IVendaService _vendaService;
    private readonly IMemoryCache _memoryCache;
    private const string VendasKey = "CacheVendas";

    public VendasController(IVendaService vendaService, IMemoryCache memoryCache)
    {
        _vendaService = vendaService;
        _memoryCache = memoryCache;
    }


    /// <summary>
    /// Obtém a lista de todos os vendas cadastrados. Com cache de 30 segundos.
    /// </summary>
    /// <returns>Uma coleção de objetos VendaDto.</returns>
    /// <response code="200">Retorna a lista de vendas.</response>
    /// <response code="401">Usuário não autenticado.</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VendaDto>>> Get()
    {
        if (_memoryCache.TryGetValue(VendasKey, out IEnumerable<VendaDto>? cachedVendas))
        {
            return Ok(cachedVendas);
        }

        var vendas = await _vendaService.GetVendas();

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(30))
            .SetSlidingExpiration(TimeSpan.FromSeconds(15))
            .SetPriority(CacheItemPriority.High);

        _memoryCache.Set(VendasKey, vendas, cacheEntryOptions);
        return Ok(vendas);
    }

    [HttpGet("{id}", Name = "GetVenda")]
    public async Task<ActionResult<VendaDto>> Get(Guid id)
    {
        var venda = await _vendaService.GetById(id);
        return Ok(venda);
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
        await _vendaService.Update(vendaDto);
        return Ok(vendaDto);
    }

    /// <summary>
    /// Somente Admin pode apagar.
    /// </summary>
    /// <returns>A venda excluída.</returns>
    /// <response code="200">Venda excluída com sucesso.</response>
    /// <response code="401">Usuário não autenticado.</response>
    /// <response code="403">Apenas administradores podem excluir.</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult<VendaDto>> Delete(Guid id)
    {
        var vendaDto = await _vendaService.GetById(id);
        await _vendaService.Remove(id);
        return Ok(vendaDto);
    }
}
