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
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> Get()
    {
        var produtos = await _produtoService.GetProdutos();
        return Ok(produtos);
    }

    [HttpGet("{id}", Name = "GetProduto")]
    public async Task<ActionResult<ProdutoDto>> Get(Guid id)
    {
        try
        {
            var produto = await _produtoService.GetById(id);
            return Ok(produto);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    //paginaçao produtos
    [HttpGet("paginacao")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Paginacao([FromQuery] ProdutoParameters produtoParameters)
    {
        var produtos = await _produtoService.GetProdutosPaged(produtoParameters.PageNumber, produtoParameters.PageSize);

        var metadata = new
        {
            produtos.TotalCount,
            produtos.PageSize,
            produtos.CurrentPage,
            produtos.TotalPages,
            produtos.HasNextPage,
            produtos.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));
        return Ok(new { data = produtos, pagination = metadata });
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProdutoDto produtoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _produtoService.Add(produtoDto);

        return new CreatedAtRouteResult("GetProduto", new { id = produtoDto.ProdutoId }, produtoDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] ProdutoDto produtoDto)
    {
        produtoDto.ProdutoId = id;

        try
        {
            await _produtoService.Update(produtoDto);
            return Ok(produtoDto);
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
    public async Task<ActionResult<ProdutoDto>> Delete(Guid id)
    {
        try
        {
            var produtoDto = await _produtoService.GetById(id);
            await _produtoService.Remove(id);
            return Ok(produtoDto);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
        catch (DomainExceptionValidation ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}
