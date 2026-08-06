
using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Mappings;
using PetShoop.Domain.Interfaces;

namespace PetShoop.Application.Services;

public class FuncionarioService : IFuncionarioService
{
    private readonly IFuncionarioRepository _funcionarioRepository;

    public FuncionarioService(IFuncionarioRepository funcionarioRepository)
    {
        _funcionarioRepository = funcionarioRepository;
    }

    public async Task<IEnumerable<FuncionarioDto>> GetFuncionarios()
    {
        var funcionarios = await _funcionarioRepository.GetFuncionariosAsync();
        return funcionarios.ToFuncionarioDtoList();
    }

    public async Task<FuncionarioDto> GetById(Guid? id)
    {
        var funcionario = await _funcionarioRepository.GetByIdAsync(id);
        var funcionarioDto = funcionario.ToFuncionarioDto();

        if (funcionarioDto is null)
        {
            throw new InvalidOperationException("Funcionário não encontrado.");
        }

        return funcionarioDto;
    }

    public async Task Add(FuncionarioDto funcionarioDto)
    {
        if (funcionarioDto is null)
        {
            throw new ArgumentNullException(nameof(funcionarioDto));
        }

        var funcionario = funcionarioDto.ToFuncionario();

        if (funcionario is null)
        {
            throw new ArgumentNullException(nameof(funcionarioDto));
        }

        await _funcionarioRepository.CreateAsync(funcionario);
    }

    public async Task Update(FuncionarioDto funcionarioDto)
    {
        if (funcionarioDto is null)
        {
            throw new ArgumentNullException(nameof(funcionarioDto));
        }

        var funcionario = await _funcionarioRepository.GetByIdAsync(funcionarioDto.FuncionarioId);

        if (funcionario is null)
        {
            throw new InvalidOperationException("Funcionário não encontrado.");
        }

        funcionario.Update(
            funcionarioDto.Nome,
            funcionarioDto.Cpf,
            funcionarioDto.Email,
            funcionarioDto.Telefone,
            funcionarioDto.Cargo,
            funcionarioDto.Salario,
            funcionarioDto.DataAdmissao);

        await _funcionarioRepository.UpdateAsync(funcionario);
    }

    public async Task Remove(Guid? id)
    {
        var funcionario = await _funcionarioRepository.GetByIdAsync(id);

        if (funcionario is null)
        {
            throw new InvalidOperationException("Funcionário não encontrado.");
        }

        await _funcionarioRepository.RemoveAsync(funcionario);
    }
}
