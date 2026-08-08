using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Mappings;
using PetShoop.Domain.Interfaces;

namespace PetShoop.Application.Services;

public class ConsultaService : IConsultaService
{
    private readonly IConsultaRepository _consultaRepository;

    public ConsultaService(IConsultaRepository consultaRepository)
    {
        _consultaRepository = consultaRepository;
    }

    public async Task<IEnumerable<ConsultaDto>> GetConsultas()
    {
        var consultas = await _consultaRepository.GetConsultasAsync();
        return consultas.ToConsultaDtoList();
    }

    public async Task<ConsultaDto> GetById(Guid? id)
    {
        var consulta = await _consultaRepository.GetByIdAsync(id);
        var consultaDto = consulta.ToConsultaDto();

        if (consultaDto is null)
        {
            throw new InvalidOperationException("Consulta não encontrada.");
        }

        return consultaDto;
    }

    public async Task Add(ConsultaDto consultaDto)
    {
        if (consultaDto is null)
        {
            throw new ArgumentNullException(nameof(consultaDto));
        }

        var consulta = consultaDto.ToConsulta();

        if (consulta is null)
        {
            throw new ArgumentNullException(nameof(consultaDto));
        }

        await _consultaRepository.CreateAsync(consulta);
    }

    public async Task Update(ConsultaDto consultaDto)
    {
        if (consultaDto is null)
        {
            throw new ArgumentNullException(nameof(consultaDto));
        }

        var consulta = await _consultaRepository.GetByIdAsync(consultaDto.ConsultaId);

        if (consulta is null)
        {
            throw new InvalidOperationException("Consulta não encontrada.");
        }

        consulta.Update(
            consultaDto.PetId,
            consultaDto.FuncionarioId,
            consultaDto.DataConsulta,
            consultaDto.Peso,
            consultaDto.Temperatura,
            consultaDto.Diagnostico,
            consultaDto.Prescricao);

        await _consultaRepository.UpdateAsync(consulta);
    }

    public async Task Remove(Guid? id)
    {
        var consulta = await _consultaRepository.GetByIdAsync(id);

        if (consulta is null)
        {
            throw new InvalidOperationException("Consulta não encontrada.");
        }

        await _consultaRepository.RemoveAsync(consulta);
    }
}
