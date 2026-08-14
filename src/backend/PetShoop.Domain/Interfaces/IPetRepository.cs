using PetShoop.Domain.Entities;
using PetShoop.Domain.Pagination;

namespace PetShoop.Domain.Interfaces;

public interface IPetRepository
{
    Task<IEnumerable<Pet>> GetPetsAsync();
    Task<Pet?> GetByIdAsync(Guid? id);
    Task<Pet> CreateAsync(Pet pet);
    Task<Pet> UpdateAsync(Pet pet);
    Task<Pet> RemoveAsync(Pet pet);
    Task<bool> HasAgendamentosAsync(Guid petId);
    Task<bool> HasConsultasAsync(Guid petId);
    Task<bool> HasProntuariosAsync(Guid petId);
    Task<bool> HasVacinasAsync(Guid petId);
    Task<PagedList<Pet>> GetPetsPagedAsync(int pageNumber, int pageSize);
}
