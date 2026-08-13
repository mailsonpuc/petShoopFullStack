using PetShoop.Domain.Entities;

namespace PetShoop.Domain.Interfaces;

public interface IProntuarioRepository
{
    Task<IEnumerable<Prontuario>> GetProntuariosAsync();
    Task<Prontuario?> GetByIdAsync(Guid? id);
    Task<Prontuario> CreateAsync(Prontuario prontuario);
    Task<Prontuario> UpdateAsync(Prontuario prontuario);
    Task<Prontuario> RemoveAsync(Prontuario prontuario);
}
