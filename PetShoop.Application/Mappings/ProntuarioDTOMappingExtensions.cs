using PetShoop.Application.DTOs;
using PetShoop.Domain.Entities;

namespace PetShoop.Application.Mappings;

public static class ProntuarioDTOMappingExtensions
{
    public static ProntuarioDto? ToProntuarioDto(this Prontuario prontuario)
    {
        if (prontuario is null)
            return null;

        return new ProntuarioDto
        {
            ProntuarioId = prontuario.ProntuarioId,
            PetId = prontuario.PetId,
            FuncionarioId = prontuario.FuncionarioId,
            DataRegistro = prontuario.DataRegistro,
            Descricao = prontuario.Descricao
        };
    }

    public static Prontuario? ToProntuario(this ProntuarioDto prontuarioDto)
    {
        if (prontuarioDto is null)
            return null;

        return new Prontuario(
            prontuarioDto.PetId,
            prontuarioDto.FuncionarioId,
            prontuarioDto.DataRegistro,
            prontuarioDto.Descricao);
    }

    public static IEnumerable<ProntuarioDto> ToProntuarioDtoList(this IEnumerable<Prontuario> prontuarios)
    {
        if (prontuarios is null || !prontuarios.Any())
        {
            return new List<ProntuarioDto>();
        }

        return prontuarios.Select(prontuario => new ProntuarioDto
        {
            ProntuarioId = prontuario.ProntuarioId,
            PetId = prontuario.PetId,
            FuncionarioId = prontuario.FuncionarioId,
            DataRegistro = prontuario.DataRegistro,
            Descricao = prontuario.Descricao
        }).ToList();
    }
}
