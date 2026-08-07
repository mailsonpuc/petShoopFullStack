using PetShoop.Domain.Entities;
using PetShoop.Domain.Interfaces;
using PetShoop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace PetShoop.Infrastructure.Repositories;

public class VacinaRepository : IVacinaRepository
{
    private readonly AppDbContext _context;

    public VacinaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Vacina> CreateAsync(Vacina vacina)
    {
        _context.Add(vacina);
        await _context.SaveChangesAsync();
        return vacina;
    }

    public async Task<Vacina> GetByIdAsync(Guid? id)
    {
        return (await _context.Vacinas.AsNoTracking().SingleOrDefaultAsync(v => v.VacinaId == id))!;
    }

    public async Task<IEnumerable<Vacina>> GetVacinasAsync()
    {
        return await _context.Vacinas.AsNoTracking().ToListAsync();
    }

    public async Task<Vacina> RemoveAsync(Vacina vacina)
    {
        _context.Remove(vacina);
        await _context.SaveChangesAsync();
        return vacina;
    }

    public async Task<Vacina> UpdateAsync(Vacina vacina)
    {
        _context.Update(vacina);
        await _context.SaveChangesAsync();
        return vacina;
    }
}
