using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Mappings;
using PetShoop.Domain.Interfaces;
using PetShoop.Domain.Pagination;

namespace PetShoop.Application.Services;

public class ConsultaService : IConsultaService
{
    private readonly IConsultaRepository _consultaRepository;
    private readonly IPetService _petService;
    private readonly IFuncionarioService _funcionarioService;

    public ConsultaService(IConsultaRepository consultaRepository, IPetService petService, IFuncionarioService funcionarioService)
    {
        _consultaRepository = consultaRepository;
        _petService = petService;
        _funcionarioService = funcionarioService;
    }

    public async Task<IEnumerable<ConsultaDto>> GetConsultas()
    {
        var consultas = await _consultaRepository.GetConsultasAsync();
        var consultaDtos = consultas.ToConsultaDtoList();

        var pets = await _petService.GetPets();
        var petMap = pets.ToDictionary(p => p.PetId, p => p.Nome);

        var funcionarios = await _funcionarioService.GetFuncionarios();
        var funcionarioMap = funcionarios.ToDictionary(f => f.FuncionarioId, f => f.Nome);

        foreach (var dto in consultaDtos)
        {
            dto.PetNome = petMap.GetValueOrDefault(dto.PetId);
            dto.FuncionarioNome = funcionarioMap.GetValueOrDefault(dto.FuncionarioId);
        }

        return consultaDtos;
    }

    public async Task<PagedList<ConsultaDto>> GetConsultasPaged(int pageNumber, int pageSize)
    {
        var pagedConsultas = await _consultaRepository.GetConsultasPagedAsync(pageNumber, pageSize);
        var consultaDtos = pagedConsultas.ToConsultaDtoList().ToList();

        var pets = await _petService.GetPets();
        var petMap = pets.ToDictionary(p => p.PetId, p => p.Nome);

        var funcionarios = await _funcionarioService.GetFuncionarios();
        var funcionarioMap = funcionarios.ToDictionary(f => f.FuncionarioId, f => f.Nome);

        foreach (var dto in consultaDtos)
        {
            dto.PetNome = petMap.GetValueOrDefault(dto.PetId);
            dto.FuncionarioNome = funcionarioMap.GetValueOrDefault(dto.FuncionarioId);
        }

        return new PagedList<ConsultaDto>(consultaDtos, pagedConsultas.TotalCount, pageNumber, pageSize);
    }

    public async Task<ConsultaDto> GetById(Guid? id)
    {
        var consulta = await _consultaRepository.GetByIdAsync(id);
        var consultaDto = consulta.ToConsultaDto();

        if (consultaDto is null)
        {
            throw new InvalidOperationException("Consulta não encontrada.");
        }

        var pet = await _petService.GetById(consultaDto.PetId);
        consultaDto.PetNome = pet?.Nome;

        var funcionario = await _funcionarioService.GetById(consultaDto.FuncionarioId);
        consultaDto.FuncionarioNome = funcionario?.Nome;

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

        await _petService.GetById(consultaDto.PetId);
        await _funcionarioService.GetById(consultaDto.FuncionarioId);

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
