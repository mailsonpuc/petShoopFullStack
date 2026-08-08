

using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Mappings;
using PetShoop.Domain.Interfaces;

namespace PetShoop.Application.Services;

public class AgendamentoService : IAgendamentoService
{
    private readonly IAgendamentoRepository _agendamentoRepository;

    public AgendamentoService(IAgendamentoRepository agendamentoRepository)
    {
        _agendamentoRepository = agendamentoRepository;
    }

    public async Task<IEnumerable<AgendamentoDto>> GetAgendamentos()
    {
        var agendamentos = await _agendamentoRepository.GetAgendamentosAsync();
        return agendamentos.ToAgendamentoDtoList();
    }

    public async Task<AgendamentoDto> GetById(Guid? id)
    {
        var agendamento = await _agendamentoRepository.GetByIdAsync(id);
        var agendamentoDto = agendamento.ToAgendamentoDto();

        if (agendamentoDto is null)
        {
            throw new InvalidOperationException("Agendamento não encontrado.");
        }

        return agendamentoDto;
    }

    public async Task Add(AgendamentoDto agendamentoDto)
    {
        var agendamento = agendamentoDto.ToAgendamento();

        if (agendamento is null)
        {
            throw new ArgumentNullException(nameof(agendamentoDto));
        }

        await _agendamentoRepository.CreateAsync(agendamento);
    }

    public async Task Update(AgendamentoDto agendamentoDto)
    {
        if (agendamentoDto is null)
        {
            throw new ArgumentNullException(nameof(agendamentoDto));
        }

        var agendamento = await _agendamentoRepository.GetByIdAsync(agendamentoDto.AgendamentoId);

        if (agendamento is null)
        {
            throw new InvalidOperationException("Agendamento não encontrado.");
        }

        agendamento.Update(
            agendamentoDto.PetId,
            agendamentoDto.ServicoId,
            agendamentoDto.FuncionarioId,
            agendamentoDto.DataHora,
            agendamentoDto.Status,
            agendamentoDto.Observacoes);

        await _agendamentoRepository.UpdateAsync(agendamento);
    }

    public async Task Remove(Guid? id)
    {
        var agendamento = await _agendamentoRepository.GetByIdAsync(id);

        if (agendamento is null)
        {
            throw new InvalidOperationException("Agendamento não encontrado.");
        }

        await _agendamentoRepository.RemoveAsync(agendamento);
    }
}
