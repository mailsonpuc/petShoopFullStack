using PetShoop.Application.DTOs;

namespace PetShoop.Application.Interfaces;

public interface IVendaService
{
    Task<IEnumerable<VendaDto>> GetVendas();
    Task<VendaDto> GetById(Guid? id);
    Task Add(VendaDto vendaDto);
    Task Update(VendaDto vendaDto);
    Task Remove(Guid? id);
}
