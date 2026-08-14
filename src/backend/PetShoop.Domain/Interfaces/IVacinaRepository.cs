using PetShoop.Domain.Entities;
using PetShoop.Domain.Pagination;

namespace PetShoop.Domain.Interfaces;

public interface IVacinaRepository
{
    Task<IEnumerable<Vacina>> GetVacinasAsync();
    Task<Vacina?> GetByIdAsync(Guid? id);
    Task<Vacina> CreateAsync(Vacina vacina);
    Task<Vacina> UpdateAsync(Vacina vacina);
    Task<Vacina> RemoveAsync(Vacina vacina);
    Task<PagedList<Vacina>> GetVacinasPagedAsync(int pageNumber, int pageSize);
}
