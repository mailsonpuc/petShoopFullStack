using PetShoop.Application.DTOs;
using PetShoop.Domain.Pagination;

namespace PetShoop.Application.Interfaces;

public interface IClienteService
{
    Task<IEnumerable<ClienteDto>> GetClientes();
    Task<ClienteDto> GetById(Guid? id);
    Task Add(ClienteDto clienteDto);
    Task Update(ClienteDto clienteDto);
    Task Remove(Guid? id);

    //paginaçao
    Task<PagedList<ClienteDto>> GetClientesPaged(int pageNumber, int pageSize);
}
