
using PetShoop.Application.DTOs;
using PetShoop.Domain.Entities;

namespace PetShoop.Application.Mappings;

public static class ServicoDTOMappingExtensions
{
    public static ServicoDto? ToServicoDto(this Servico servico)
    {
        if (servico is null)
            return null;

        return new ServicoDto
        {
            ServicoId = servico.ServicoId,
            Nome = servico.Nome,
            Descricao = servico.Descricao,
            Preco = servico.Preco,
            DuracaoEmMinutos = servico.DuracaoEmMinutos
        };
    }

    public static Servico? ToServico(this ServicoDto servicoDto)
    {
        if (servicoDto is null)
            return null;

        var servico = new Servico(
            servicoDto.Nome,
            servicoDto.Descricao,
            servicoDto.Preco,
            servicoDto.DuracaoEmMinutos);

        servico.SetServicoId(servicoDto.ServicoId);

        return servico;
    }

    public static IEnumerable<ServicoDto> ToServicoDtoList(this IEnumerable<Servico> servicos)
    {
        if (servicos is null || !servicos.Any())
        {
            return new List<ServicoDto>();
        }

        return servicos.Select(servico => new ServicoDto
        {
            ServicoId = servico.ServicoId,
            Nome = servico.Nome,
            Descricao = servico.Descricao,
            Preco = servico.Preco,
            DuracaoEmMinutos = servico.DuracaoEmMinutos
        }).ToList();
    }
}
