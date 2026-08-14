

using PetShoop.Application.DTOs;
using PetShoop.Domain.Pagination;

namespace PetShoop.Application.Interfaces;

public interface IVacinaService
{
    Task<IEnumerable<VacinaDto>> GetVacinas();
    Task<VacinaDto> GetById(Guid? id);
    Task Add(VacinaDto vacinaDto);
    Task Update(VacinaDto vacinaDto);
    Task Remove(Guid? id);
    Task<PagedList<VacinaDto>> GetVacinasPaged(int pageNumber, int pageSize);
}
