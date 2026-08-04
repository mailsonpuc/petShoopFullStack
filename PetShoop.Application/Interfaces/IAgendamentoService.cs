using PetShoop.Application.DTOs;

namespace PetShoop.Application.Interfaces;

public interface IAgendamentoService
{
    Task<IEnumerable<AgendamentoDto>> GetAgendamentos();
    Task<AgendamentoDto> GetById(Guid? id);
    Task Add(AgendamentoDto agendamentoDto);
    Task Update(AgendamentoDto agendamentoDto);
    Task Remove(Guid? id);
}
