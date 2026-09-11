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
public class AgendamentosController : ControllerBase
{
    private readonly IAgendamentoService _agendamentoService;

    private readonly IMemoryCache _cache;
    private const string AgendamentosKey = "CacheAgendamentos";

    public AgendamentosController(IAgendamentoService agendamentoService, IMemoryCache cache)
    {
        _agendamentoService = agendamentoService;
        _cache = cache;
    }

    /// <summary>
    /// Obtém a lista de todos os agendamentos cadastrados. Com cache de 30 segundos.
    /// </summary>
    /// <returns>Uma coleção de objetos AgendamentoDto.</returns>
    /// <response code="200">Retorna a lista de agendamentos.</response>
    /// <response code="401">Usuário não autenticado.</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AgendamentoDto>>> Get()
    {
        var cacheKey = AgendamentosKey;
        if (_cache.TryGetValue(cacheKey, out IEnumerable<AgendamentoDto>? cachedAgendamentos))
        {
            return Ok(cachedAgendamentos);
        }

        var agendamentos = await _agendamentoService.GetAgendamentos();

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(30))
            .SetSlidingExpiration(TimeSpan.FromSeconds(15))
            .SetPriority(CacheItemPriority.High);

        _cache.Set(cacheKey, agendamentos, cacheEntryOptions);
        return Ok(agendamentos);
    }

    [HttpGet("{id}", Name = "GetAgendamento")]
    public async Task<ActionResult<AgendamentoDto>> Get(Guid id)
    {
        var agendamento = await _agendamentoService.GetById(id);
        return Ok(agendamento);
    }

    //paginaçao agendamentos
    [HttpGet("paginacao")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Paginacao([FromQuery] AgendamentoParameters agendamentoParameters)
    {
        var agendamentos = await _agendamentoService.GetAgendamentosPaged(agendamentoParameters.PageNumber, agendamentoParameters.PageSize);

        var metadata = new
        {
            agendamentos.TotalCount,
            agendamentos.PageSize,
            agendamentos.CurrentPage,
            agendamentos.TotalPages,
            agendamentos.HasNextPage,
            agendamentos.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));
        return Ok(new { data = agendamentos, pagination = metadata });
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] AgendamentoDto agendamentoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _agendamentoService.Add(agendamentoDto);

        return new CreatedAtRouteResult("GetAgendamento", new { id = agendamentoDto.AgendamentoId }, agendamentoDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] AgendamentoDto agendamentoDto)
    {
        agendamentoDto.AgendamentoId = id;
        await _agendamentoService.Update(agendamentoDto);
        return Ok(agendamentoDto);
    }


    /// <summary>
    /// Somente Admin pode apagar.
    /// </summary>
    /// <returns>O agendamento excluído.</returns>
    /// <response code="200">Agendamento excluído com sucesso.</response>
    /// <response code="401">Usuário não autenticado.</response>
    /// <response code="403">Apenas administradores podem excluir.</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult<AgendamentoDto>> Delete(Guid id)
    {
        var agendamentoDto = await _agendamentoService.GetById(id);
        await _agendamentoService.Remove(id);
        return Ok(agendamentoDto);
    }
}
