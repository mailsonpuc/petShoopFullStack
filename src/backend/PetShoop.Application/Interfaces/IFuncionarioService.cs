using PetShoop.Application.DTOs;

namespace PetShoop.Application.Interfaces;

public interface IFuncionarioService
{
    Task<IEnumerable<FuncionarioDto>> GetFuncionarios();
    Task<FuncionarioDto> GetById(Guid? id);
    Task Add(FuncionarioDto funcionarioDto);
    Task Update(FuncionarioDto funcionarioDto);
    Task Remove(Guid? id);
}
