using PetShoop.Application.DTOs;
using PetShoop.Domain.Entities;

namespace PetShoop.Application.Mappings;

public static class AgendamentoDTOMappingExtensions
{
    public static AgendamentoDto? ToAgendamentoDto(this Agendamento agendamento)
    {
        if (agendamento is null)
            return null;

        return new AgendamentoDto
        {
            AgendamentoId = agendamento.AgendamentoId,
            PetId = agendamento.PetId,
            ServicoId = agendamento.ServicoId,
            FuncionarioId = agendamento.FuncionarioId,
            DataHora = agendamento.DataHora,
            Status = agendamento.Status,
            Observacoes = agendamento.Observacoes
        };
    }

    public static Agendamento? ToAgendamento(this AgendamentoDto agendamentoDto)
    {
        if (agendamentoDto is null) return null;

        return new Agendamento(
            agendamentoDto.PetId,
            agendamentoDto.ServicoId,
            agendamentoDto.FuncionarioId,
            agendamentoDto.DataHora,
            agendamentoDto.Status,
            agendamentoDto.Observacoes);
    }

    public static IEnumerable<AgendamentoDto> ToAgendamentoDtoList(this IEnumerable<Agendamento> agendamentos)
    {
        if (agendamentos is null || !agendamentos.Any())
        {
            return new List<AgendamentoDto>();
        }

        return agendamentos.Select(agendamento => new AgendamentoDto
        {
            AgendamentoId = agendamento.AgendamentoId,
            PetId = agendamento.PetId,
            ServicoId = agendamento.ServicoId,
            FuncionarioId = agendamento.FuncionarioId,
            DataHora = agendamento.DataHora,
            Status = agendamento.Status,
            Observacoes = agendamento.Observacoes
        }).ToList();
    }
}
