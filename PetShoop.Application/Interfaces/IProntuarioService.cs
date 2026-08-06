using PetShoop.Application.DTOs;

namespace PetShoop.Application.Interfaces;

public interface IProntuarioService
{
    Task<IEnumerable<ProntuarioDto>> GetProntuarios();
    Task<ProntuarioDto> GetById(Guid? id);
    Task Add(ProntuarioDto prontuarioDto);
    Task Update(ProntuarioDto prontuarioDto);
    Task Remove(Guid? id);
}
