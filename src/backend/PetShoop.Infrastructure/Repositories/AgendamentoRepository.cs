using PetShoop.Domain.Entities;
using PetShoop.Domain.Interfaces;
using PetShoop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace PetShoop.Infrastructure.Repositories;

public class AgendamentoRepository : IAgendamentoRepository
{
    private readonly AppDbContext _context;

    public AgendamentoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Agendamento> CreateAsync(Agendamento agendamento)
    {
        _context.Add(agendamento);
        await _context.SaveChangesAsync();
        return agendamento;
    }

    public async Task<Agendamento?> GetByIdAsync(Guid? id)
    {
        if (id == null)
            return null;

        return await _context.Agendamentos.AsNoTracking().SingleOrDefaultAsync(a => a.AgendamentoId == id);
    }

    public async Task<IEnumerable<Agendamento>> GetAgendamentosAsync()
    {
        return await _context.Agendamentos.AsNoTracking().ToListAsync();
    }

    public async Task<Agendamento> RemoveAsync(Agendamento agendamento)
    {
        _context.Remove(agendamento);
        await _context.SaveChangesAsync();
        return agendamento;
    }

    public async Task<Agendamento> UpdateAsync(Agendamento agendamento)
    {
        _context.Update(agendamento);
        await _context.SaveChangesAsync();
        return agendamento;
    }
}
