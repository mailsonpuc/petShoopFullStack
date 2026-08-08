using PetShoop.Domain.Entities;
using PetShoop.Domain.Interfaces;
using PetShoop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace PetShoop.Infrastructure.Repositories;

public class ProntuarioRepository : IProntuarioRepository
{
    private readonly AppDbContext _context;

    public ProntuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Prontuario> CreateAsync(Prontuario prontuario)
    {
        _context.Add(prontuario);
        await _context.SaveChangesAsync();
        return prontuario;
    }

    public async Task<Prontuario> GetByIdAsync(Guid? id)
    {
        return (await _context.Prontuarios.AsNoTracking().SingleOrDefaultAsync(p => p.ProntuarioId == id))!;
    }

    public async Task<IEnumerable<Prontuario>> GetProntuariosAsync()
    {
        return await _context.Prontuarios.AsNoTracking().ToListAsync();
    }

    public async Task<Prontuario> RemoveAsync(Prontuario prontuario)
    {
        _context.Remove(prontuario);
        await _context.SaveChangesAsync();
        return prontuario;
    }

    public async Task<Prontuario> UpdateAsync(Prontuario prontuario)
    {
        _context.Update(prontuario);
        await _context.SaveChangesAsync();
        return prontuario;
    }
}
