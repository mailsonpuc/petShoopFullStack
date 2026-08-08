using PetShoop.Domain.Entities;
using PetShoop.Domain.Interfaces;
using PetShoop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace PetShoop.Infrastructure.Repositories;

public class ItemVendaRepository : IItemVendaRepository
{
    private readonly AppDbContext _context;

    public ItemVendaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ItemVenda> CreateAsync(ItemVenda itemVenda)
    {
        _context.Add(itemVenda);
        await _context.SaveChangesAsync();
        return itemVenda;
    }

    public async Task<ItemVenda> GetByIdAsync(Guid? id)
    {
        return (await _context.ItensVendas.AsNoTracking().SingleOrDefaultAsync(i => i.ItemVendaId == id))!;
    }

    public async Task<IEnumerable<ItemVenda>> GetItensVendasAsync()
    {
        return await _context.ItensVendas.AsNoTracking().ToListAsync();
    }

    public async Task<ItemVenda> RemoveAsync(ItemVenda itemVenda)
    {
        _context.Remove(itemVenda);
        await _context.SaveChangesAsync();
        return itemVenda;
    }

    public async Task<ItemVenda> UpdateAsync(ItemVenda itemVenda)
    {
        _context.Update(itemVenda);
        await _context.SaveChangesAsync();
        return itemVenda;
    }
}
