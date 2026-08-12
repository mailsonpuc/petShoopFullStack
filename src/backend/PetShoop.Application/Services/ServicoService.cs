using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Mappings;
using PetShoop.Domain.Interfaces;
using PetShoop.Domain.Validation;

namespace PetShoop.Application.Services;

public class ServicoService : IServicoService
{
    private readonly IServicoRepository _servicoRepository;

    public ServicoService(IServicoRepository servicoRepository)
    {
        _servicoRepository = servicoRepository;
    }

    public async Task<IEnumerable<ServicoDto>> GetServicos()
    {
        var servicos = await _servicoRepository.GetServicosAsync();
        return servicos.ToServicoDtoList();
    }

    public async Task<ServicoDto> GetById(Guid? id)
    {
        var servico = await _servicoRepository.GetByIdAsync(id);
        var servicoDto = servico.ToServicoDto();

        if (servicoDto is null)
        {
            throw new InvalidOperationException("Serviço não encontrado.");
        }

        return servicoDto;
    }

    public async Task Add(ServicoDto servicoDto)
    {
        if (servicoDto is null)
        {
            throw new ArgumentNullException(nameof(servicoDto));
        }

        var servico = servicoDto.ToServico();

        if (servico is null)
        {
            throw new ArgumentNullException(nameof(servicoDto));
        }

        await _servicoRepository.CreateAsync(servico);
    }

    public async Task Update(ServicoDto servicoDto)
    {
        if (servicoDto is null)
        {
            throw new ArgumentNullException(nameof(servicoDto));
        }

        var servico = await _servicoRepository.GetByIdAsync(servicoDto.ServicoId);

        if (servico is null)
        {
            throw new InvalidOperationException("Serviço não encontrado.");
        }

        servico.Update(
            servicoDto.Nome,
            servicoDto.Descricao,
            servicoDto.Preco,
            servicoDto.DuracaoEmMinutos);

        await _servicoRepository.UpdateAsync(servico);
    }

    public async Task Remove(Guid? id)
    {
        var servico = await _servicoRepository.GetByIdAsync(id);

        if (servico is null)
        {
            throw new InvalidOperationException("Serviço não encontrado.");
        }

        if (await _servicoRepository.HasAgendamentosAsync(servico.ServicoId))
        {
            throw new DomainExceptionValidation("Não é possível excluir o serviço porque existem agendamentos vinculados a ele.");
        }

        await _servicoRepository.RemoveAsync(servico);
    }
}
