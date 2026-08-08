

using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Mappings;
using PetShoop.Domain.Interfaces;

namespace PetShoop.Application.Services;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;

    public PetService(IPetRepository petRepository)
    {
        _petRepository = petRepository;
    }

    public async Task<IEnumerable<PetDto>> GetPets()
    {
        var pets = await _petRepository.GetPetsAsync();
        return pets.ToPetDtoList();
    }

    public async Task<PetDto> GetById(Guid? id)
    {
        var pet = await _petRepository.GetByIdAsync(id);
        var petDto = pet.ToPetDto();

        if (petDto is null)
        {
            throw new InvalidOperationException("Pet não encontrado.");
        }

        return petDto;
    }

    public async Task Add(PetDto petDto)
    {
        var pet = petDto.ToPet();

        if (pet is null)
        {
            throw new ArgumentNullException(nameof(petDto));
        }

        await _petRepository.CreateAsync(pet);
    }

    public async Task Update(PetDto petDto)
    {
        var pet = petDto.ToPet();

        if (pet is null)
        {
            throw new ArgumentNullException(nameof(petDto));
        }

        await _petRepository.UpdateAsync(pet);
    }

    public async Task Remove(Guid? id)
    {
        var pet = await _petRepository.GetByIdAsync(id);
        await _petRepository.RemoveAsync(pet);
    }
}
