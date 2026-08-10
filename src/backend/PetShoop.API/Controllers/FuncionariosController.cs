using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;

namespace PetShoop.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class FuncionariosController : ControllerBase
{
    private readonly IFuncionarioService _funcionarioService;

    public FuncionariosController(IFuncionarioService funcionarioService)
    {
        _funcionarioService = funcionarioService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FuncionarioDto>>> Get()
    {
        var funcionarios = await _funcionarioService.GetFuncionarios();
        return Ok(funcionarios);
    }

    [HttpGet("{id}", Name = "GetFuncionario")]
    public async Task<ActionResult<FuncionarioDto>> Get(Guid id)
    {
        var funcionario = await _funcionarioService.GetById(id);

        if (funcionario == null)
        {
            return NotFound();
        }

        return Ok(funcionario);
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
        if (id != funcionarioDto.FuncionarioId)
        {
            return BadRequest();
        }

        await _funcionarioService.Update(funcionarioDto);

        return Ok(funcionarioDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<FuncionarioDto>> Delete(Guid id)
    {
        var funcionarioDto = await _funcionarioService.GetById(id);
        if (funcionarioDto == null)
        {
            return NotFound();
        }

        await _funcionarioService.Remove(id);
        return Ok(funcionarioDto);
    }
}
