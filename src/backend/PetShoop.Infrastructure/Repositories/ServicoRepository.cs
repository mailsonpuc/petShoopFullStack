using PetShoop.Domain.Entities;
using PetShoop.Domain.Interfaces;
using PetShoop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace PetShoop.Infrastructure.Repositories;

public class ServicoRepository : IServicoRepository
{
    private readonly AppDbContext _context;

    public ServicoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Servico> CreateAsync(Servico servico)
    {
        _context.Add(servico);
        await _context.SaveChangesAsync();
        return servico;
    }

    public async Task<Servico> GetByIdAsync(Guid? id)
    {
        return (await _context.Servicos.AsNoTracking().SingleOrDefaultAsync(s => s.ServicoId == id))!;
    }

    public async Task<IEnumerable<Servico>> GetServicosAsync()
    {
        return await _context.Servicos.AsNoTracking().ToListAsync();
    }

    public async Task<Servico> RemoveAsync(Servico servico)
    {
        _context.Remove(servico);
        await _context.SaveChangesAsync();
        return servico;
    }

    public async Task<Servico> UpdateAsync(Servico servico)
    {
        _context.Update(servico);
        await _context.SaveChangesAsync();
        return servico;
    }
}
