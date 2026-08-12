using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Mappings;
using PetShoop.Domain.Interfaces;
using PetShoop.Domain.Validation;

namespace PetShoop.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<ClienteDto>> GetClientes()
    {
        var clientes = await _clienteRepository.GetClientesAsync();
        return clientes.ToClienteDtoList();
    }

    public async Task<ClienteDto> GetById(Guid? id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        var clienteDto = cliente.ToClienteDto();

        if (clienteDto is null)
        {
            throw new InvalidOperationException("Cliente não encontrado.");
        }

        return clienteDto;
    }

    public async Task Add(ClienteDto clienteDto)
    {
        var cliente = clienteDto.ToCliente();

        if (cliente is null)
        {
            throw new ArgumentNullException(nameof(clienteDto));
        }

        await _clienteRepository.CreateAsync(cliente);
    }

    public async Task Update(ClienteDto clienteDto)
    {
        var cliente = clienteDto.ToCliente();

        if (cliente is null)
        {
            throw new ArgumentNullException(nameof(clienteDto));
        }

        await _clienteRepository.UpdateAsync(cliente);
    }

    public async Task Remove(Guid? id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        if (cliente is null)
        {
            throw new InvalidOperationException("Cliente não encontrado.");
        }

        if (await _clienteRepository.HasPetsAsync(cliente.ClienteId) || await _clienteRepository.HasVendasAsync(cliente.ClienteId))
        {
            throw new DomainExceptionValidation("Não é possível excluir o cliente porque existem pets ou vendas vinculados a ele.");
        }

        await _clienteRepository.RemoveAsync(cliente);
    }
}
