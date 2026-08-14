using PetShoop.Domain.Entities;
using PetShoop.Domain.Interfaces;
using PetShoop.Domain.Pagination;
using PetShoop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace PetShoop.Infrastructure.Repositories;

public class ConsultaRepository : IConsultaRepository
{
    private readonly AppDbContext _context;

    public ConsultaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Consulta> CreateAsync(Consulta consulta)
    {
        _context.Add(consulta);
        await _context.SaveChangesAsync();
        return consulta;
    }

    public async Task<Consulta?> GetByIdAsync(Guid? id)
    {
        if (id == null)
            return null;

        return await _context.Consultas.AsNoTracking().SingleOrDefaultAsync(c => c.ConsultaId == id);
    }

    public async Task<IEnumerable<Consulta>> GetConsultasAsync()
    {
        return await _context.Consultas.AsNoTracking().ToListAsync();
    }

    public async Task<PagedList<Consulta>> GetConsultasPagedAsync(int pageNumber, int pageSize)
    {
        return await PagedList<Consulta>.ToPagedListAsync(_context.Consultas.AsNoTracking(), pageNumber, pageSize);
    }

    public async Task<Consulta> RemoveAsync(Consulta consulta)
    {
        _context.Remove(consulta);
        await _context.SaveChangesAsync();
        return consulta;
    }

    public async Task<Consulta> UpdateAsync(Consulta consulta)
    {
        _context.Update(consulta);
        await _context.SaveChangesAsync();
        return consulta;
    }
}
