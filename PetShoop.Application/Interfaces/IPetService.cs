

using PetShoop.Application.DTOs;

namespace PetShoop.Application.Interfaces;

public interface IPetService
{
    Task<IEnumerable<PetDto>> GetPets();
    Task<PetDto> GetById(Guid? id);
    Task Add(PetDto petDto);
    Task Update(PetDto petDto);
    Task Remove(Guid? id);
}
