

using PetShoop.Application.DTOs;
using PetShoop.Domain.Entities;

namespace PetShoop.Application.Mappings;

public static class ClienteDTOMappingExtensions
{
    public static ClienteDto? ToClienteDto(this Cliente cliente)
    {
        if (cliente is null)
            return null;

        return new ClienteDto
        {
            ClienteId = cliente.ClienteId,
            Nome = cliente.Nome,
            Cpf = cliente.Cpf,
            Email = cliente.Email,
            Telefone = cliente.Telefone,
            DataDeNascimento = cliente.DataDeNascimento,
            Endereco = cliente.Endereco
        };
    }



    public static Cliente? ToCliente(this ClienteDto clienteDto)
    {
        if (clienteDto is null) return null;

        return new Cliente(
            clienteDto.Nome,
            clienteDto.Cpf,
            clienteDto.Email,
            clienteDto.Telefone,
            clienteDto.DataDeNascimento,
            clienteDto.Endereco);
    }


    public static IEnumerable<ClienteDto> ToClienteDtoList(this IEnumerable<Cliente> clientes)
    {
        if (clientes is null || !clientes.Any())
        {
            return new List<ClienteDto>();
        }

        return clientes.Select(cliente => new ClienteDto
        {
            ClienteId = cliente.ClienteId,
            Nome = cliente.Nome,
            Cpf = cliente.Cpf,
            Email = cliente.Email,
            Telefone = cliente.Telefone,
            DataDeNascimento = cliente.DataDeNascimento,
            Endereco = cliente.Endereco
        }).ToList();
    }


}
