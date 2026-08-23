using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.CrossCutting.Pagination;
using PetShoop.Domain.Validation;
using System.Text.Json;

namespace PetShoop.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize(Roles = "admin")]
[EnableRateLimiting("fixedwindow")]
public class ServicosController : ControllerBase
{
    private readonly IServicoService _servicoService;

    public ServicosController(IServicoService servicoService)
    {
        _servicoService = servicoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServicoDto>>> Get()
    {
        var servicos = await _servicoService.GetServicos();
        return Ok(servicos);
    }

    [HttpGet("{id}", Name = "GetServico")]
    public async Task<ActionResult<ServicoDto>> Get(Guid id)
    {
        var servico = await _servicoService.GetById(id);
        return Ok(servico);
    }

    //paginaçao servicos
    [HttpGet("paginacao")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Paginacao([FromQuery] ServicoParameters servicoParameters)
    {
        var servicos = await _servicoService.GetServicosPaged(servicoParameters.PageNumber, servicoParameters.PageSize);

        var metadata = new
        {
            servicos.TotalCount,
            servicos.PageSize,
            servicos.CurrentPage,
            servicos.TotalPages,
            servicos.HasNextPage,
            servicos.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));
        return Ok(new { data = servicos, pagination = metadata });
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ServicoDto servicoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _servicoService.Add(servicoDto);

        return new CreatedAtRouteResult("GetServico", new { id = servicoDto.ServicoId }, servicoDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] ServicoDto servicoDto)
    {
        servicoDto.ServicoId = id;
        await _servicoService.Update(servicoDto);
        return Ok(servicoDto);
    }


    /// <summary>
    /// Somente Admin pode apagar.
    /// </summary>
    /// <returns>O serviço excluído.</returns>
    /// <response code="200">Serviço excluído com sucesso.</response>
    /// <response code="401">Usuário não autenticado.</response>
    /// <response code="403">Apenas administradores podem excluir.</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServicoDto>> Delete(Guid id)
    {
        var servicoDto = await _servicoService.GetById(id);
        await _servicoService.Remove(id);
        return Ok(servicoDto);
    }
}
