using PetShoop.Application.DTOs;
using PetShoop.Domain.Entities;

namespace PetShoop.Application.Mappings;

public static class FuncionarioDTOMappingExtensions
{
    public static FuncionarioDto? ToFuncionarioDto(this Funcionario funcionario)
    {
        if (funcionario is null)
            return null;

        return new FuncionarioDto
        {
            FuncionarioId = funcionario.FuncionarioId,
            Nome = funcionario.Nome,
            Cpf = funcionario.Cpf,
            Email = funcionario.Email,
            Telefone = funcionario.Telefone,
            Cargo = funcionario.Cargo,
            Salario = funcionario.Salario,
            DataAdmissao = funcionario.DataAdmissao
        };
    }

    public static Funcionario? ToFuncionario(this FuncionarioDto funcionarioDto)
    {
        if (funcionarioDto is null)
            return null;

        var funcionario = new Funcionario(
            funcionarioDto.Nome,
            funcionarioDto.Cpf,
            funcionarioDto.Email,
            funcionarioDto.Telefone,
            funcionarioDto.Cargo,
            funcionarioDto.Salario,
            funcionarioDto.DataAdmissao);

        funcionario.SetFuncionarioId(funcionarioDto.FuncionarioId);

        return funcionario;
    }

    public static IEnumerable<FuncionarioDto> ToFuncionarioDtoList(this IEnumerable<Funcionario> funcionarios)
    {
        if (funcionarios is null || !funcionarios.Any())
        {
            return new List<FuncionarioDto>();
        }

        return funcionarios.Select(funcionario => new FuncionarioDto
        {
            FuncionarioId = funcionario.FuncionarioId,
            Nome = funcionario.Nome,
            Cpf = funcionario.Cpf,
            Email = funcionario.Email,
            Telefone = funcionario.Telefone,
            Cargo = funcionario.Cargo,
            Salario = funcionario.Salario,
            DataAdmissao = funcionario.DataAdmissao
        }).ToList();
    }
}
