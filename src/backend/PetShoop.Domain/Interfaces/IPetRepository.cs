using PetShoop.Domain.Entities;

namespace PetShoop.Domain.Interfaces;

public interface IPetRepository
{
    Task<IEnumerable<Pet>> GetPetsAsync();
    Task<Pet> GetByIdAsync(Guid? id);
    Task<Pet> CreateAsync(Pet pet);
    Task<Pet> UpdateAsync(Pet pet);
    Task<Pet> RemoveAsync(Pet pet);
}
