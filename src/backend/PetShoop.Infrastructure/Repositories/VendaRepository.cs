using PetShoop.Domain.Entities;
using PetShoop.Domain.Interfaces;
using PetShoop.Domain.Pagination;
using PetShoop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace PetShoop.Infrastructure.Repositories;

public class VendaRepository : IVendaRepository
{
    private readonly AppDbContext _context;

    public VendaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Venda> CreateAsync(Venda venda)
    {
        _context.Add(venda);
        await _context.SaveChangesAsync();
        return venda;
    }

    public async Task<Venda?> GetByIdAsync(Guid? id)
    {
        if (id == null)
            return null;

        return await _context.Vendas.AsNoTracking().SingleOrDefaultAsync(v => v.VendaId == id);
    }

    public async Task<IEnumerable<Venda>> GetVendasAsync()
    {
        return await _context.Vendas.AsNoTracking().ToListAsync();
    }

    public async Task<PagedList<Venda>> GetVendasPagedAsync(int pageNumber, int pageSize)
    {
        return await PagedList<Venda>.ToPagedListAsync(_context.Vendas.AsNoTracking(), pageNumber, pageSize);
    }

    public async Task<Venda> RemoveAsync(Venda venda)
    {
        _context.Remove(venda);
        await _context.SaveChangesAsync();
        return venda;
    }

    public async Task<Venda> UpdateAsync(Venda venda)
    {
        _context.Update(venda);
        await _context.SaveChangesAsync();
        return venda;
    }

    public async Task<bool> HasItensVendaAsync(Guid vendaId)
    {
        return await _context.ItensVendas.AnyAsync(iv => iv.VendaId == vendaId);
    }

    public async Task RecalcularTotalAsync(Guid vendaId, decimal total)
    {
        var venda = await _context.Vendas.FirstOrDefaultAsync(v => v.VendaId == vendaId);
        if (venda != null)
        {
            venda.Update(venda.ClienteId, venda.DataVenda, total, venda.FormaPagamento);
            await _context.SaveChangesAsync();
        }
    }
}
