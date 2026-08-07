
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Mappings;
using PetShoop.Domain.Interfaces;

namespace PetShoop.Application.Services;

public class VacinaService : IVacinaService
{
    private readonly IVacinaRepository _vacinaRepository;

    public VacinaService(IVacinaRepository vacinaRepository)
    {
        _vacinaRepository = vacinaRepository;
    }

    public async Task<IEnumerable<VacinaDto>> GetPets()
    {
        var vacinas = await _vacinaRepository.GetVacinasAsync();
        return vacinas.ToVacinaDtoList();
    }

    public async Task<VacinaDto> GetById(Guid? id)
    {
        var vacina = await _vacinaRepository.GetByIdAsync(id);
        var vacinaDto = vacina.ToVacinaDto();

        if (vacinaDto is null)
        {
            throw new InvalidOperationException("Vacina não encontrada.");
        }

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
