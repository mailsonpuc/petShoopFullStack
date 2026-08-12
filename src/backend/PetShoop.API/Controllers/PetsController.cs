using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;

namespace PetShoop.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class PetsController : ControllerBase
{
    private readonly IPetService _petService;

    public PetsController(IPetService petService)
    {
        _petService = petService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PetDto>>> Get()
    {
        var pets = await _petService.GetPets();
        return Ok(pets);
    }

    [HttpGet("{id}", Name = "GetPet")]
    public async Task<ActionResult<PetDto>> Get(Guid id)
    {
        var pet = await _petService.GetById(id);

        if (pet == null)
        {
            return NotFound();
        }

        return Ok(pet);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PetDto petDto)
    {
        if (!ModelState.IsValid)
        {
            foreach (var kvp in ModelState)
            {
                foreach (var err in kvp.Value.Errors)
                {
                    Console.WriteLine($"ModelState error key={kvp.Key} msg={err.ErrorMessage} ex={err.Exception}");
                }
            }
            return BadRequest(ModelState);
        }

        await _petService.Add(petDto);

        return new CreatedAtRouteResult("GetPet", new { id = petDto.PetId }, petDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] PetDto petDto)
    {
        if (!ModelState.IsValid)
        {
            foreach (var kvp in ModelState)
            {
                foreach (var err in kvp.Value.Errors)
                {
                    Console.WriteLine($"ModelState error key={kvp.Key} msg={err.ErrorMessage} ex={err.Exception}");
                }
            }
            return BadRequest(ModelState);
        }

        if (id != petDto.PetId)
        {
            return BadRequest();
        }

        await _petService.Update(petDto);

        return Ok(petDto);
    }


    /// <summary>
    /// Somente Admin pode apagar.
    /// </summary>
    /// <returns>Uma coleção de objetos AgendamentoDto.</returns>
    /// <response code="200">Retorna a lista de agendamentos.</response>
    /// <response code="401">Usuário não autenticado.</response>
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<PetDto>> Delete(Guid id)
    {
        var petDto = await _petService.GetById(id);
        if (petDto == null)
        {
            return NotFound();
        }

        await _petService.Remove(id);
        return Ok(petDto);
    }
}
