using PetShoop.Domain.Entities;
using PetShoop.Domain.Interfaces;
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

    public async Task<Venda> GetByIdAsync(Guid? id)
    {
        return (await _context.Vendas.AsNoTracking().SingleOrDefaultAsync(v => v.VendaId == id))!;
    }

    public async Task<IEnumerable<Venda>> GetVendasAsync()
    {
        return await _context.Vendas.AsNoTracking().ToListAsync();
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
}
