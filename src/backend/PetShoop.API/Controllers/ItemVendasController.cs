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
public class ItemVendasController : ControllerBase
{
    private readonly IItemVendaService _itemVendaService;
    private readonly IMemoryCache _memoryCache;
    private const string ItensVendaKey = "CacheItensVenda";

    public ItemVendasController(IItemVendaService itemVendaService, IMemoryCache memoryCache)
    {
        _itemVendaService = itemVendaService;
        _memoryCache = memoryCache;
    }

    
    /// <summary>
    /// Obtém a lista de todos os itens de venda cadastrados. Com cache de 30 segundos.
    /// </summary>
    /// <returns>Uma coleção de objetos ItemVendaDto.</returns>
    /// <response code="200">Retorna a lista de itens de venda.</response>
    /// <response code="401">Usuário não autenticado.</response>

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemVendaDto>>> Get()
    {
        if (_memoryCache.TryGetValue(ItensVendaKey, out IEnumerable<ItemVendaDto>? cachedItensVenda))
        {
            return Ok(cachedItensVenda);
        }

        var itensVenda = await _itemVendaService.GetItensVendas();

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(30))
            .SetSlidingExpiration(TimeSpan.FromSeconds(15))
            .SetPriority(CacheItemPriority.High);

        _memoryCache.Set(ItensVendaKey, itensVenda, cacheEntryOptions);
        return Ok(itensVenda);
    }

    [HttpGet("{id}", Name = "GetItemVenda")]
    public async Task<ActionResult<ItemVendaDto>> Get(Guid id)
    {
        try
        {
            var itemVenda = await _itemVendaService.GetById(id);
            return Ok(itemVenda);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    //paginaçao itens venda
    [HttpGet("paginacao")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Paginacao([FromQuery] ItemVendaParameters itemVendaParameters)
    {
        var itensVenda = await _itemVendaService.GetItensVendasPaged(itemVendaParameters.PageNumber, itemVendaParameters.PageSize);

        var metadata = new
        {
            itensVenda.TotalCount,
            itensVenda.PageSize,
            itensVenda.CurrentPage,
            itensVenda.TotalPages,
            itensVenda.HasNextPage,
            itensVenda.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));
        return Ok(new { data = itensVenda, pagination = metadata });
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
        itemVendaDto.ItemVendaId = id;

        try
        {
            await _itemVendaService.Update(itemVendaDto);
            return Ok(itemVendaDto);
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
    [HttpDelete("{id}")]
    public async Task<ActionResult<ItemVendaDto>> Delete(Guid id)
    {
        try
        {
            var itemVendaDto = await _itemVendaService.GetById(id);
            await _itemVendaService.Remove(id);
            return Ok(itemVendaDto);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
