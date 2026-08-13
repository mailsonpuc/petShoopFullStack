
using PetShoop.Application.DTOs;
using PetShoop.Domain.Entities;

namespace PetShoop.Application.Mappings;

public static class VacinaDTOMappingExtensions
{
    public static VacinaDto? ToVacinaDto(this Vacina vacina)
    {
        if (vacina is null)
            return null;

        return new VacinaDto
        {
            VacinaId = vacina.VacinaId,
            PetId = vacina.PetId,
            Nome = vacina.Nome,
            Fabricante = vacina.Fabricante,
            DataAplicacao = vacina.DataAplicacao,
            ProximaDose = vacina.ProximaDose
        };
    }


    public static Vacina? ToVacina(this VacinaDto vacinaDto)
    {
        if (vacinaDto is null) return null;

        var vacina = new Vacina(
            vacinaDto.PetId,
            vacinaDto.Nome,
            vacinaDto.Fabricante,
            vacinaDto.DataAplicacao,
            vacinaDto.ProximaDose);

        if (vacinaDto.VacinaId != Guid.Empty)
        {
            vacina.SetVacinaId(vacinaDto.VacinaId);
        }

        return vacina;
    }


    public static IEnumerable<VacinaDto> ToVacinaDtoList(this IEnumerable<Vacina> vacinas)
    {
        if (vacinas is null || !vacinas.Any())
        {
            return new List<VacinaDto>();
        }

        return vacinas.Select(vacina => new VacinaDto
        {
            VacinaId = vacina.VacinaId,
            PetId = vacina.PetId,
            Nome = vacina.Nome,
            Fabricante = vacina.Fabricante,
            DataAplicacao = vacina.DataAplicacao,
            ProximaDose = vacina.ProximaDose
        }).ToList();
    }

}
