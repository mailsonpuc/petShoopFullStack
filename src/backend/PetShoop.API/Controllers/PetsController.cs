using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Logging;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.CrossCutting.Pagination;
using System.Text.Json;

namespace PetShoop.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize(Roles = "admin")]
[EnableRateLimiting("fixedwindow")]
public class PetsController : ControllerBase
{
    private readonly IPetService _petService;
    private readonly ILogger<PetsController> _logger;

    public PetsController(IPetService petService, ILogger<PetsController> logger)
    {
        _petService = petService;
        _logger = logger;
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
        return Ok(pet);
    }

    //paginaçao pets
    [HttpGet("paginacao")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Paginacao([FromQuery] PetParameters petParameters)
    {
        var pets = await _petService.GetPetsPaged(petParameters.PageNumber, petParameters.PageSize);

        var metadata = new
        {
            pets.TotalCount,
            pets.PageSize,
            pets.CurrentPage,
            pets.TotalPages,
            pets.HasNextPage,
            pets.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));
        return Ok(new { data = pets, pagination = metadata });
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
                    _logger.LogWarning("ModelState error key={Key} msg={Message} ex={Exception}", kvp.Key, err.ErrorMessage, err.Exception);
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
        petDto.PetId = id;
        await _petService.Update(petDto);
        return Ok(petDto);
    }


    /// <summary>
    /// Somente Admin pode apagar.
    /// </summary>
    /// <returns>O pet excluído.</returns>
    /// <response code="200">Pet excluído com sucesso.</response>
    /// <response code="401">Usuário não autenticado.</response>
    /// <response code="403">Apenas administradores podem excluir.</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult<PetDto>> Delete(Guid id)
    {
        var petDto = await _petService.GetById(id);
        await _petService.Remove(id);
        return Ok(petDto);
    }
}
