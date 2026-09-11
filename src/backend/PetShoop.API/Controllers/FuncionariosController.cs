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
public class FuncionariosController : ControllerBase
{
    private readonly IFuncionarioService _funcionarioService;
    private readonly IMemoryCache _memoryCache;
    private const string FuncionariosKey = "CacheFuncionarios";

    public FuncionariosController(IFuncionarioService funcionarioService, IMemoryCache memoryCache)
    {
        _funcionarioService = funcionarioService;
        _memoryCache = memoryCache;
    }


    /// <summary>
    /// Obtém a lista de todos os funcionarios cadastrados. Com cache de 30 segundos.
    /// </summary>
    /// <returns>Uma coleção de objetos FuncionarioDto.</returns>
    /// <response code="200">Retorna a lista de funcionarios.</response>
    /// <response code="401">Usuário não autenticado.</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FuncionarioDto>>> Get()
    {
        if (_memoryCache.TryGetValue(FuncionariosKey, out IEnumerable<FuncionarioDto>? cachedFuncionarios))
        {
            return Ok(cachedFuncionarios);
        }

        var funcionarios = await _funcionarioService.GetFuncionarios();

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(30))
            .SetSlidingExpiration(TimeSpan.FromSeconds(15))
            .SetPriority(CacheItemPriority.High);

        _memoryCache.Set(FuncionariosKey, funcionarios, cacheEntryOptions);
        return Ok(funcionarios);
    }

    [HttpGet("{id}", Name = "GetFuncionario")]
    public async Task<ActionResult<FuncionarioDto>> Get(Guid id)
    {
        var funcionario = await _funcionarioService.GetById(id);
        return Ok(funcionario);
    }

    //paginaçao funcionarios
    [HttpGet("paginacao")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Paginacao([FromQuery] FuncionarioParameters funcionarioParameters)
    {
        var funcionarios = await _funcionarioService.GetFuncionariosPaged(funcionarioParameters.PageNumber, funcionarioParameters.PageSize);

        var metadata = new
        {
            funcionarios.TotalCount,
            funcionarios.PageSize,
            funcionarios.CurrentPage,
            funcionarios.TotalPages,
            funcionarios.HasNextPage,
            funcionarios.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));
        return Ok(new { data = funcionarios, pagination = metadata });
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] FuncionarioDto funcionarioDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _funcionarioService.Add(funcionarioDto);

        return new CreatedAtRouteResult("GetFuncionario", new { id = funcionarioDto.FuncionarioId }, funcionarioDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] FuncionarioDto funcionarioDto)
    {
        funcionarioDto.FuncionarioId = id;
        await _funcionarioService.Update(funcionarioDto);
        return Ok(funcionarioDto);
    }


    /// <summary>
    /// Somente Admin pode apagar.
    /// </summary>
    /// <returns>O funcionário excluído.</returns>
    /// <response code="200">Funcionário excluído com sucesso.</response>
    /// <response code="401">Usuário não autenticado.</response>
    /// <response code="403">Apenas administradores podem excluir.</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult<FuncionarioDto>> Delete(Guid id)
    {
        var funcionarioDto = await _funcionarioService.GetById(id);
        await _funcionarioService.Remove(id);
        return Ok(funcionarioDto);
    }
}
