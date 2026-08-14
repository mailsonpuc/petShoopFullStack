using PetShoop.Application.DTOs;
using PetShoop.Domain.Pagination;

namespace PetShoop.Application.Interfaces;

public interface IVendaService
{
    Task<IEnumerable<VendaDto>> GetVendas();
    Task<VendaDto> GetById(Guid? id);
    Task Add(VendaDto vendaDto);
    Task Update(VendaDto vendaDto);
    Task Remove(Guid? id);
    Task<PagedList<VendaDto>> GetVendasPaged(int pageNumber, int pageSize);
}
