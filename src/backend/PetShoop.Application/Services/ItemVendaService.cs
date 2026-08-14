using PetShoop.Application.DTOs;
using PetShoop.Application.Interfaces;
using PetShoop.Application.Mappings;
using PetShoop.Domain.Interfaces;
using PetShoop.Domain.Pagination;

namespace PetShoop.Application.Services;

public class ItemVendaService : IItemVendaService
{
    private readonly IItemVendaRepository _itemVendaRepository;
    private readonly IProdutoService _produtoService;
    private readonly IVendaService _vendaService;

    public ItemVendaService(IItemVendaRepository itemVendaRepository, IProdutoService produtoService, IVendaService vendaService)
    {
        _itemVendaRepository = itemVendaRepository;
        _produtoService = produtoService;
        _vendaService = vendaService;
    }

    public async Task<IEnumerable<ItemVendaDto>> GetItensVendas()
    {
        var itensVenda = await _itemVendaRepository.GetItensVendasAsync();
        var itemVendaDtos = itensVenda.ToItemVendaDtoList();

        var produtos = await _produtoService.GetProdutos();
        var produtoMap = produtos.ToDictionary(p => p.ProdutoId, p => p.Nome);

        var vendas = await _vendaService.GetVendas();
        var vendaMap = vendas.ToDictionary(v => v.VendaId, v => v);
        var clienteMap = vendas.Where(v => v.ClienteId != Guid.Empty).ToDictionary(v => v.VendaId, v => v.ClienteNome);

        foreach (var dto in itemVendaDtos)
        {
            dto.ProdutoNome = produtoMap.GetValueOrDefault(dto.ProdutoId);
            dto.ClienteNome = clienteMap.GetValueOrDefault(dto.VendaId);
            if (vendaMap.TryGetValue(dto.VendaId, out var vendaDto))
            {
                dto.VendaInfo = $"Venda em {vendaDto.DataVenda:dd/MM/yyyy HH:mm}";
            }
        }

        return itemVendaDtos;
    }

    public async Task<PagedList<ItemVendaDto>> GetItensVendasPaged(int pageNumber, int pageSize)
    {
        var pagedItensVenda = await _itemVendaRepository.GetItensVendasPagedAsync(pageNumber, pageSize);
        var itemVendaDtos = pagedItensVenda.ToItemVendaDtoList().ToList();

        var produtos = await _produtoService.GetProdutos();
        var produtoMap = produtos.ToDictionary(p => p.ProdutoId, p => p.Nome);

        var vendas = await _vendaService.GetVendas();
        var vendaMap = vendas.ToDictionary(v => v.VendaId, v => v);
        var clienteMap = vendas.Where(v => v.ClienteId != Guid.Empty).ToDictionary(v => v.VendaId, v => v.ClienteNome);

        foreach (var dto in itemVendaDtos)
        {
            dto.ProdutoNome = produtoMap.GetValueOrDefault(dto.ProdutoId);
            dto.ClienteNome = clienteMap.GetValueOrDefault(dto.VendaId);
            if (vendaMap.TryGetValue(dto.VendaId, out var vendaDto))
            {
                dto.VendaInfo = $"Venda em {vendaDto.DataVenda:dd/MM/yyyy HH:mm}";
            }
        }

        return new PagedList<ItemVendaDto>(itemVendaDtos, pagedItensVenda.TotalCount, pageNumber, pageSize);
    }

    public async Task<ItemVendaDto> GetById(Guid? id)
    {
        var itemVenda = await _itemVendaRepository.GetByIdAsync(id);
        var itemVendaDto = itemVenda.ToItemVendaDto();

        if (itemVendaDto is null)
        {
            throw new InvalidOperationException("Item de venda não encontrado.");
        }

        var produto = await _produtoService.GetById(itemVendaDto.ProdutoId);
        itemVendaDto.ProdutoNome = produto?.Nome;

        var venda = await _vendaService.GetById(itemVendaDto.VendaId);
        if (venda != null)
        {
            itemVendaDto.VendaInfo = $"Venda em {venda.DataVenda:dd/MM/yyyy HH:mm}";
            itemVendaDto.ClienteNome = venda.ClienteNome;
        }

        return itemVendaDto;
    }

    public async Task Add(ItemVendaDto itemVendaDto)
    {
        if (itemVendaDto is null)
        {
            throw new ArgumentNullException(nameof(itemVendaDto));
        }

        var itemVenda = itemVendaDto.ToItemVenda();

        if (itemVenda is null)
        {
            throw new ArgumentNullException(nameof(itemVendaDto));
        }

        await _itemVendaRepository.CreateAsync(itemVenda);
    }

    public async Task Update(ItemVendaDto itemVendaDto)
    {
        if (itemVendaDto is null)
        {
            throw new ArgumentNullException(nameof(itemVendaDto));
        }

        var itemVenda = await _itemVendaRepository.GetByIdAsync(itemVendaDto.ItemVendaId);

        if (itemVenda is null)
        {
            throw new InvalidOperationException("Item de venda não encontrado.");
        }

        itemVenda.Update(
            itemVendaDto.VendaId,
            itemVendaDto.ProdutoId,
            itemVendaDto.Quantidade,
            itemVendaDto.ValorUnitario);

        await _itemVendaRepository.UpdateAsync(itemVenda);
    }

    public async Task Remove(Guid? id)
    {
        var itemVenda = await _itemVendaRepository.GetByIdAsync(id);

        if (itemVenda is null)
        {
            throw new InvalidOperationException("Item de venda não encontrado.");
        }

        await _itemVendaRepository.RemoveAsync(itemVenda);
    }
}
