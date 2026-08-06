

using PetShoop.Application.DTOs;

namespace PetShoop.Application.Interfaces;

public interface IConsultaService
{
    Task<IEnumerable<ConsultaDto>> GetConsultas();
    Task<ConsultaDto> GetById(Guid? id);
    Task Add(ConsultaDto consultaDto);
    Task Update(ConsultaDto consultaDto);
    Task Remove(Guid? id);
}
