using PetShoop.Application.DTOs;
using PetShoop.Domain.Entities;

namespace PetShoop.Application.Mappings;

public static class ConsultaDTOMappingExtensions
{
    public static ConsultaDto? ToConsultaDto(this Consulta consulta)
    {
        if (consulta is null)
            return null;

        return new ConsultaDto
        {
            ConsultaId = consulta.ConsultaId,
            PetId = consulta.PetId,
            FuncionarioId = consulta.FuncionarioId,
            DataConsulta = consulta.DataConsulta,
            Peso = consulta.Peso,
            Temperatura = consulta.Temperatura,
            Diagnostico = consulta.Diagnostico,
            Prescricao = consulta.Prescricao
        };
    }

    public static Consulta? ToConsulta(this ConsultaDto consultaDto)
    {
        if (consultaDto is null)
            return null;

        return new Consulta(
            consultaDto.PetId,
            consultaDto.FuncionarioId,
            consultaDto.DataConsulta,
            consultaDto.Peso,
            consultaDto.Temperatura,
            consultaDto.Diagnostico,
            consultaDto.Prescricao);
    }

    public static IEnumerable<ConsultaDto> ToConsultaDtoList(this IEnumerable<Consulta> consultas)
    {
        if (consultas is null || !consultas.Any())
        {
            return new List<ConsultaDto>();
        }

        return consultas.Select(consulta => new ConsultaDto
        {
            ConsultaId = consulta.ConsultaId,
            PetId = consulta.PetId,
            FuncionarioId = consulta.FuncionarioId,
            DataConsulta = consulta.DataConsulta,
            Peso = consulta.Peso,
            Temperatura = consulta.Temperatura,
            Diagnostico = consulta.Diagnostico,
            Prescricao = consulta.Prescricao
        }).ToList();
    }
}
