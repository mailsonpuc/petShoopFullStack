

using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Mappings;
using PetShoop.Domain.Interfaces;
using PetShoop.Domain.Pagination;

namespace PetShoop.Application.Services;

public class AgendamentoService : IAgendamentoService
{
    private readonly IAgendamentoRepository _agendamentoRepository;
    private readonly IPetService _petService;
    private readonly IServicoService _servicoService;
    private readonly IFuncionarioService _funcionarioService;

    public AgendamentoService(IAgendamentoRepository agendamentoRepository, IPetService petService, IServicoService servicoService, IFuncionarioService funcionarioService)
    {
        _agendamentoRepository = agendamentoRepository;
        _petService = petService;
        _servicoService = servicoService;
        _funcionarioService = funcionarioService;
    }

    public async Task<IEnumerable<AgendamentoDto>> GetAgendamentos()
    {
        var agendamentos = await _agendamentoRepository.GetAgendamentosAsync();
        var agendamentoDtos = agendamentos.ToAgendamentoDtoList();

        var pets = await _petService.GetPets();
        var petMap = pets.ToDictionary(p => p.PetId, p => p.Nome);

        var servicos = await _servicoService.GetServicos();
        var servicoMap = servicos.ToDictionary(s => s.ServicoId, s => s.Nome);

        var funcionarios = await _funcionarioService.GetFuncionarios();
        var funcionarioMap = funcionarios.ToDictionary(f => f.FuncionarioId, f => f.Nome);

        foreach (var dto in agendamentoDtos)
        {
            dto.PetNome = petMap.GetValueOrDefault(dto.PetId);
            dto.ServicoNome = servicoMap.GetValueOrDefault(dto.ServicoId);
            dto.FuncionarioNome = funcionarioMap.GetValueOrDefault(dto.FuncionarioId);
        }

        return agendamentoDtos;
    }

    public async Task<PagedList<AgendamentoDto>> GetAgendamentosPaged(int pageNumber, int pageSize)
    {
        var pagedAgendamentos = await _agendamentoRepository.GetAgendamentosPagedAsync(pageNumber, pageSize);
        var agendamentoDtos = pagedAgendamentos.ToAgendamentoDtoList().ToList();

        var pets = await _petService.GetPets();
        var petMap = pets.ToDictionary(p => p.PetId, p => p.Nome);

        var servicos = await _servicoService.GetServicos();
        var servicoMap = servicos.ToDictionary(s => s.ServicoId, s => s.Nome);

        var funcionarios = await _funcionarioService.GetFuncionarios();
        var funcionarioMap = funcionarios.ToDictionary(f => f.FuncionarioId, f => f.Nome);

        foreach (var dto in agendamentoDtos)
        {
            dto.PetNome = petMap.GetValueOrDefault(dto.PetId);
            dto.ServicoNome = servicoMap.GetValueOrDefault(dto.ServicoId);
            dto.FuncionarioNome = funcionarioMap.GetValueOrDefault(dto.FuncionarioId);
        }

        return new PagedList<AgendamentoDto>(agendamentoDtos, pagedAgendamentos.TotalCount, pageNumber, pageSize);
    }

    public async Task<AgendamentoDto> GetById(Guid? id)
    {
        var agendamento = await _agendamentoRepository.GetByIdAsync(id);
        var agendamentoDto = agendamento.ToAgendamentoDto();

        if (agendamentoDto is null)
        {
            throw new InvalidOperationException("Agendamento não encontrado.");
        }

        var pet = await _petService.GetById(agendamentoDto.PetId);
        agendamentoDto.PetNome = pet?.Nome;

        var servico = await _servicoService.GetById(agendamentoDto.ServicoId);
        agendamentoDto.ServicoNome = servico?.Nome;

        var funcionario = await _funcionarioService.GetById(agendamentoDto.FuncionarioId);
        agendamentoDto.FuncionarioNome = funcionario?.Nome;

        return agendamentoDto;
    }

    public async Task Add(AgendamentoDto agendamentoDto)
    {
        var agendamento = agendamentoDto.ToAgendamento();

        if (agendamento is null)
        {
            throw new ArgumentNullException(nameof(agendamentoDto));
        }

        await _petService.GetById(agendamentoDto.PetId);
        await _servicoService.GetById(agendamentoDto.ServicoId);
        await _funcionarioService.GetById(agendamentoDto.FuncionarioId);

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
