using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Mappings;
using PetShoop.Domain.Interfaces;

namespace PetShoop.Application.Services;

public class ProntuarioService : IProntuarioService
{
    private readonly IProntuarioRepository _prontuarioRepository;

    public ProntuarioService(IProntuarioRepository prontuarioRepository)
    {
        _prontuarioRepository = prontuarioRepository;
    }

    public async Task<IEnumerable<ProntuarioDto>> GetProntuarios()
    {
        var prontuarios = await _prontuarioRepository.GetProntuariosAsync();
        return prontuarios.ToProntuarioDtoList();
    }

    public async Task<ProntuarioDto> GetById(Guid? id)
    {
        var prontuario = await _prontuarioRepository.GetByIdAsync(id);
        var prontuarioDto = prontuario.ToProntuarioDto();

        if (prontuarioDto is null)
        {
            throw new InvalidOperationException("Prontuário não encontrado.");
        }

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
