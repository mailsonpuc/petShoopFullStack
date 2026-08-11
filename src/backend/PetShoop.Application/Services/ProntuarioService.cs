using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Mappings;
using PetShoop.Domain.Interfaces;

namespace PetShoop.Application.Services;

public class ProntuarioService : IProntuarioService
{
    private readonly IProntuarioRepository _prontuarioRepository;
    private readonly IPetService _petService;
    private readonly IFuncionarioService _funcionarioService;

    public ProntuarioService(IProntuarioRepository prontuarioRepository, IPetService petService, IFuncionarioService funcionarioService)
    {
        _prontuarioRepository = prontuarioRepository;
        _petService = petService;
        _funcionarioService = funcionarioService;
    }

    public async Task<IEnumerable<ProntuarioDto>> GetProntuarios()
    {
        var prontuarios = await _prontuarioRepository.GetProntuariosAsync();
        var prontuarioDtos = prontuarios.ToProntuarioDtoList();

        var pets = await _petService.GetPets();
        var petMap = pets.ToDictionary(p => p.PetId, p => p.Nome);

        var funcionarios = await _funcionarioService.GetFuncionarios();
        var funcionarioMap = funcionarios.ToDictionary(f => f.FuncionarioId, f => f.Nome);

        foreach (var dto in prontuarioDtos)
        {
            dto.PetNome = petMap.GetValueOrDefault(dto.PetId);
            dto.FuncionarioNome = funcionarioMap.GetValueOrDefault(dto.FuncionarioId);
        }

        return prontuarioDtos;
    }

    public async Task<ProntuarioDto> GetById(Guid? id)
    {
        var prontuario = await _prontuarioRepository.GetByIdAsync(id);
        var prontuarioDto = prontuario.ToProntuarioDto();

        if (prontuarioDto is null)
        {
            throw new InvalidOperationException("Prontuário não encontrado.");
        }

        var pet = await _petService.GetById(prontuarioDto.PetId);
        prontuarioDto.PetNome = pet?.Nome;

        var funcionario = await _funcionarioService.GetById(prontuarioDto.FuncionarioId);
        prontuarioDto.FuncionarioNome = funcionario?.Nome;

        return prontuarioDto;
    }

    public async Task Add(ProntuarioDto prontuarioDto)
    {
        if (prontuarioDto is null)
        {
            throw new ArgumentNullException(nameof(prontuarioDto));
        }

        var prontuario = prontuarioDto.ToProntuario();

        if (prontuario is null)
        {
            throw new ArgumentNullException(nameof(prontuarioDto));
        }

        await _prontuarioRepository.CreateAsync(prontuario);
    }

    public async Task Update(ProntuarioDto prontuarioDto)
    {
        if (prontuarioDto is null)
        {
            throw new ArgumentNullException(nameof(prontuarioDto));
        }

        var prontuario = await _prontuarioRepository.GetByIdAsync(prontuarioDto.ProntuarioId);

        if (prontuario is null)
        {
            throw new InvalidOperationException("Prontuário não encontrado.");
        }

        prontuario.Update(
            prontuarioDto.PetId,
            prontuarioDto.FuncionarioId,
            prontuarioDto.DataRegistro,
            prontuarioDto.Descricao);

        await _prontuarioRepository.UpdateAsync(prontuario);
    }

    public async Task Remove(Guid? id)
    {
        var prontuario = await _prontuarioRepository.GetByIdAsync(id);

        if (prontuario is null)
        {
            throw new InvalidOperationException("Prontuário não encontrado.");
        }

        await _prontuarioRepository.RemoveAsync(prontuario);
    }
}
