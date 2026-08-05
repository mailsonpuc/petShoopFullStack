
using Microsoft.AspNetCore.Mvc;
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;

namespace PetShoop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VacinasController : ControllerBase
{
	private readonly IVacinaService _vacinaService;

	public VacinasController(IVacinaService vacinaService)
	{
		_vacinaService = vacinaService;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<VacinaDto>>> Get()
	{
		var vacinas = await _vacinaService.GetPets();
		return Ok(vacinas);
	}

	[HttpGet("{id}", Name = "GetVacina")]
	public async Task<ActionResult<VacinaDto>> Get(Guid id)
	{
		var vacina = await _vacinaService.GetById(id);

		if (vacina == null)
		{
			return NotFound();
		}

		return Ok(vacina);
	}

	[HttpPost]
	public async Task<ActionResult> Post([FromBody] VacinaDto vacinaDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		await _vacinaService.Add(vacinaDto);

		return new CreatedAtRouteResult("GetVacina", new { id = vacinaDto.VacinaId }, vacinaDto);
	}

	[HttpPut("{id}")]
	public async Task<ActionResult> Put(Guid id, [FromBody] VacinaDto vacinaDto)
	{
		if (id != vacinaDto.VacinaId)
		{
			return BadRequest();
		}

		await _vacinaService.Update(vacinaDto);

		return Ok(vacinaDto);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<VacinaDto>> Delete(Guid id)
	{
		var vacinaDto = await _vacinaService.GetById(id);
		if (vacinaDto == null)
		{
			return NotFound();
		}

		await _vacinaService.Remove(id);
		return Ok(vacinaDto);
	}

}
