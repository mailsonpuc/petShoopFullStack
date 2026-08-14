

using PetShoop.Application.DTOs;
using PetShoop.Domain.Pagination;

namespace PetShoop.Application.Interfaces;

public interface IPetService
{
    Task<IEnumerable<PetDto>> GetPets();
    Task<PetDto> GetById(Guid? id);
    Task Add(PetDto petDto);
    Task Update(PetDto petDto);
    Task Remove(Guid? id);
    Task<PagedList<PetDto>> GetPetsPaged(int pageNumber, int pageSize);
}
