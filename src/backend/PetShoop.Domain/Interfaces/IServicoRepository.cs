using PetShoop.Domain.Entities;

namespace PetShoop.Domain.Interfaces;

public interface IServicoRepository
{
    Task<IEnumerable<Servico>> GetServicosAsync();
    Task<Servico> GetByIdAsync(Guid? id);
    Task<Servico> CreateAsync(Servico servico);
    Task<Servico> UpdateAsync(Servico servico);
    Task<Servico> RemoveAsync(Servico servico);
}
