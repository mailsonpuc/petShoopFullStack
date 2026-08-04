using PetShoop.Domain.Entities;
using PetShoop.Domain.Interfaces;
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

    public async Task<Consulta> GetByIdAsync(Guid? id)
    {
        return (await _context.Consultas.SingleOrDefaultAsync(c => c.ConsultaId == id))!;
    }

    public async Task<IEnumerable<Consulta>> GetConsultasAsync()
    {
        return await _context.Consultas.ToListAsync();
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
