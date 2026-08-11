

using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Mappings;
using PetShoop.Domain.Interfaces;

namespace PetShoop.Application.Services;

public class PetService : IPetService
{
    private readonly IPetRepository _petRepository;
    private readonly IClienteService _clienteService;

    public PetService(IPetRepository petRepository, IClienteService clienteService)
    {
        _petRepository = petRepository;
        _clienteService = clienteService;
    }

    public async Task<IEnumerable<PetDto>> GetPets()
    {
        var pets = await _petRepository.GetPetsAsync();
        var petDtos = pets.ToPetDtoList();

        var clientes = await _clienteService.GetClientes();
        var clienteMap = clientes.ToDictionary(c => c.ClienteId, c => c.Nome);

        foreach (var dto in petDtos)
        {
            dto.ClienteNome = clienteMap.GetValueOrDefault(dto.ClienteId);
        }

        return petDtos;
    }

    public async Task<PetDto> GetById(Guid? id)
    {
        var pet = await _petRepository.GetByIdAsync(id);
        var petDto = pet.ToPetDto();

        if (petDto is null)
        {
            throw new InvalidOperationException("Pet não encontrado.");
        }

        var cliente = await _clienteService.GetById(petDto.ClienteId);
        petDto.ClienteNome = cliente?.Nome;

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
