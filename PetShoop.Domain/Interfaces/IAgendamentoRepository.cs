using PetShoop.Domain.Entities;

namespace PetShoop.Domain.Interfaces;

public interface IAgendamentoRepository
{
    Task<IEnumerable<Agendamento>> GetAgendamentosAsync();
    Task<Agendamento> GetByIdAsync(Guid? id);
    Task<Agendamento> CreateAsync(Agendamento agendamento);
    Task<Agendamento> UpdateAsync(Agendamento agendamento);
    Task<Agendamento> RemoveAsync(Agendamento agendamento);
}
