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
            return BadRequest(ModelState);
        }

        await _petService.Add(petDto);

        return new CreatedAtRouteResult("GetPet", new { id = petDto.PetId }, petDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] PetDto petDto)
    {
        if (id != petDto.PetId)
        {
            return BadRequest();
        }

        await _petService.Update(petDto);

        return Ok(petDto);
    }

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
