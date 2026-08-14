using PetShoop.Application.DTOs;
using PetShoop.Domain.Pagination;

namespace PetShoop.Application.Interfaces;

public interface IFuncionarioService
{
    Task<IEnumerable<FuncionarioDto>> GetFuncionarios();
    Task<FuncionarioDto> GetById(Guid? id);
    Task Add(FuncionarioDto funcionarioDto);
    Task Update(FuncionarioDto funcionarioDto);
    Task Remove(Guid? id);
    Task<PagedList<FuncionarioDto>> GetFuncionariosPaged(int pageNumber, int pageSize);
}
