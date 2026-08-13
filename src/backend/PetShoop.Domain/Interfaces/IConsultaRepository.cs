using PetShoop.Domain.Entities;

namespace PetShoop.Domain.Interfaces;

public interface IConsultaRepository
{
    Task<IEnumerable<Consulta>> GetConsultasAsync();
    Task<Consulta?> GetByIdAsync(Guid? id);
    Task<Consulta> CreateAsync(Consulta consulta);
    Task<Consulta> UpdateAsync(Consulta consulta);
    Task<Consulta> RemoveAsync(Consulta consulta);
}
