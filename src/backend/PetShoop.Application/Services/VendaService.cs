using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Mappings;
using PetShoop.Domain.Interfaces;

namespace PetShoop.Application.Services;

public class VendaService : IVendaService
{
    private readonly IVendaRepository _vendaRepository;
    private readonly IClienteService _clienteService;

    public VendaService(IVendaRepository vendaRepository, IClienteService clienteService)
    {
        _vendaRepository = vendaRepository;
        _clienteService = clienteService;
    }

    public async Task<IEnumerable<VendaDto>> GetVendas()
    {
        var vendas = await _vendaRepository.GetVendasAsync();
        var vendaDtos = vendas.ToVendaDtoList();

        var clientes = await _clienteService.GetClientes();
        var clienteMap = clientes.ToDictionary(c => c.ClienteId, c => c.Nome);

        foreach (var dto in vendaDtos)
        {
            dto.ClienteNome = clienteMap.GetValueOrDefault(dto.ClienteId);
        }

        return vendaDtos;
    }

    public async Task<VendaDto> GetById(Guid? id)
    {
        var venda = await _vendaRepository.GetByIdAsync(id);
        var vendaDto = venda.ToVendaDto();

        if (vendaDto is null)
        {
            throw new InvalidOperationException("Venda não encontrada.");
        }

        var cliente = await _clienteService.GetById(vendaDto.ClienteId);
        vendaDto.ClienteNome = cliente?.Nome;

        return vendaDto;
    }

    public async Task Add(VendaDto vendaDto)
    {
        if (vendaDto is null)
        {
            throw new ArgumentNullException(nameof(vendaDto));
        }

        var venda = vendaDto.ToVenda();

        if (venda is null)
        {
            throw new ArgumentNullException(nameof(vendaDto));
        }

        await _vendaRepository.CreateAsync(venda);
    }

    public async Task Update(VendaDto vendaDto)
    {
        if (vendaDto is null)
        {
            throw new ArgumentNullException(nameof(vendaDto));
        }

        var venda = await _vendaRepository.GetByIdAsync(vendaDto.VendaId);

        if (venda is null)
        {
            throw new InvalidOperationException("Venda não encontrada.");
        }

        venda.Update(
            vendaDto.ClienteId,
            vendaDto.DataVenda,
            vendaDto.ValorTotal,
            vendaDto.FormaPagamento);

        await _vendaRepository.UpdateAsync(venda);
    }

    public async Task Remove(Guid? id)
    {
        var venda = await _vendaRepository.GetByIdAsync(id);

        if (venda is null)
        {
            throw new InvalidOperationException("Venda não encontrada.");
        }

        await _vendaRepository.RemoveAsync(venda);
    }
}
