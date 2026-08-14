using PetShoop.Application.DTOs;
using PetShoop.Domain.Pagination;

namespace PetShoop.Application.Interfaces;

public interface IServicoService
{
    Task<IEnumerable<ServicoDto>> GetServicos();
    Task<ServicoDto> GetById(Guid? id);
    Task Add(ServicoDto servicoDto);
    Task Update(ServicoDto servicoDto);
    Task Remove(Guid? id);
    Task<PagedList<ServicoDto>> GetServicosPaged(int pageNumber, int pageSize);
}
