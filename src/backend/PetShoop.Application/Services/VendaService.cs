using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Mappings;
using PetShoop.Domain.Interfaces;

namespace PetShoop.Application.Services;

public class VendaService : IVendaService
{
    private readonly IVendaRepository _vendaRepository;

    public VendaService(IVendaRepository vendaRepository)
    {
        _vendaRepository = vendaRepository;
    }

    public async Task<IEnumerable<VendaDto>> GetVendas()
    {
        var vendas = await _vendaRepository.GetVendasAsync();
        return vendas.ToVendaDtoList();
    }

    public async Task<VendaDto> GetById(Guid? id)
    {
        var venda = await _vendaRepository.GetByIdAsync(id);
        var vendaDto = venda.ToVendaDto();

        if (vendaDto is null)
        {
            throw new InvalidOperationException("Venda não encontrada.");
        }

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
