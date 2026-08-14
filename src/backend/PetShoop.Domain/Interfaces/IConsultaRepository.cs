using PetShoop.Domain.Entities;
using PetShoop.Domain.Pagination;

namespace PetShoop.Domain.Interfaces;

public interface IConsultaRepository
{
    Task<IEnumerable<Consulta>> GetConsultasAsync();
    Task<Consulta?> GetByIdAsync(Guid? id);
    Task<Consulta> CreateAsync(Consulta consulta);
    Task<Consulta> UpdateAsync(Consulta consulta);
    Task<Consulta> RemoveAsync(Consulta consulta);
    Task<PagedList<Consulta>> GetConsultasPagedAsync(int pageNumber, int pageSize);
}
