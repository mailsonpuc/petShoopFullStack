
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Mappings;
using PetShoop.Domain.Interfaces;
using PetShoop.Domain.Pagination;

namespace PetShoop.Application.Services;

public class VacinaService : IVacinaService
{
    private readonly IVacinaRepository _vacinaRepository;
    private readonly IPetService _petService;

    public VacinaService(IVacinaRepository vacinaRepository, IPetService petService)
    {
        _vacinaRepository = vacinaRepository;
        _petService = petService;
    }

    public async Task<IEnumerable<VacinaDto>> GetVacinas()
    {
        var vacinas = await _vacinaRepository.GetVacinasAsync();
        var vacinaDtos = vacinas.ToVacinaDtoList();

        var pets = await _petService.GetPets();
        var petMap = pets.ToDictionary(p => p.PetId, p => p.Nome);

        foreach (var dto in vacinaDtos)
        {
            dto.PetNome = petMap.GetValueOrDefault(dto.PetId);
        }

        return vacinaDtos;
    }

    public async Task<PagedList<VacinaDto>> GetVacinasPaged(int pageNumber, int pageSize)
    {
        var pagedVacinas = await _vacinaRepository.GetVacinasPagedAsync(pageNumber, pageSize);
        var vacinaDtos = pagedVacinas.ToVacinaDtoList().ToList();

        var pets = await _petService.GetPets();
        var petMap = pets.ToDictionary(p => p.PetId, p => p.Nome);

        foreach (var dto in vacinaDtos)
        {
            dto.PetNome = petMap.GetValueOrDefault(dto.PetId);
        }

        return new PagedList<VacinaDto>(vacinaDtos, pagedVacinas.TotalCount, pageNumber, pageSize);
    }

    public async Task<VacinaDto> GetById(Guid? id)
    {
        var vacina = await _vacinaRepository.GetByIdAsync(id);
        var vacinaDto = vacina.ToVacinaDto();

        if (vacinaDto is null)
        {
            throw new InvalidOperationException("Vacina não encontrada.");
        }

        var pet = await _petService.GetById(vacinaDto.PetId);
        vacinaDto.PetNome = pet?.Nome;

        return vacinaDto;
    }

    public async Task Add(VacinaDto vacinaDto)
    {
        var vacina = vacinaDto.ToVacina();

        if (vacina is null)
        {
            throw new ArgumentNullException(nameof(vacinaDto));
        }

        await _vacinaRepository.CreateAsync(vacina);
    }

    public async Task Update(VacinaDto vacinaDto)
    {
        if (vacinaDto is null)
        {
            throw new ArgumentNullException(nameof(vacinaDto));
        }

        var vacina = await _vacinaRepository.GetByIdAsync(vacinaDto.VacinaId);

        if (vacina is null)
        {
            throw new InvalidOperationException("Vacina não encontrada.");
        }

        vacina.Update(
            vacinaDto.PetId,
            vacinaDto.Nome,
            vacinaDto.Fabricante,
            vacinaDto.DataAplicacao,
            vacinaDto.ProximaDose);

        await _vacinaRepository.UpdateAsync(vacina);
    }

    public async Task Remove(Guid? id)
    {
        var vacina = await _vacinaRepository.GetByIdAsync(id);

        if (vacina is null)
        {
            throw new InvalidOperationException("Vacina não encontrada.");
        }

        await _vacinaRepository.RemoveAsync(vacina);
    }
}
