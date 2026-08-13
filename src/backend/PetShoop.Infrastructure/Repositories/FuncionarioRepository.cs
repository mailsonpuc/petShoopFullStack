using PetShoop.Domain.Entities;
using PetShoop.Domain.Interfaces;
using PetShoop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace PetShoop.Infrastructure.Repositories;

public class FuncionarioRepository : IFuncionarioRepository
{
    private readonly AppDbContext _context;

    public FuncionarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Funcionario> CreateAsync(Funcionario funcionario)
    {
        _context.Add(funcionario);
        await _context.SaveChangesAsync();
        return funcionario;
    }

    public async Task<Funcionario?> GetByIdAsync(Guid? id)
    {
        if (id == null)
            return null;

        return await _context.Funcionarios.AsNoTracking().SingleOrDefaultAsync(f => f.FuncionarioId == id);
    }

    public async Task<IEnumerable<Funcionario>> GetFuncionariosAsync()
    {
        return await _context.Funcionarios.AsNoTracking().ToListAsync();
    }

    public async Task<Funcionario> RemoveAsync(Funcionario funcionario)
    {
        _context.Remove(funcionario);
        await _context.SaveChangesAsync();
        return funcionario;
    }

    public async Task<Funcionario> UpdateAsync(Funcionario funcionario)
    {
        _context.Update(funcionario);
        await _context.SaveChangesAsync();
        return funcionario;
    }

    public async Task<bool> HasAgendamentosAsync(Guid funcionarioId)
    {
        return await _context.Agendamentos.AnyAsync(a => a.FuncionarioId == funcionarioId);
    }

    public async Task<bool> HasConsultasAsync(Guid funcionarioId)
    {
        return await _context.Consultas.AnyAsync(c => c.FuncionarioId == funcionarioId);
    }

    public async Task<bool> HasProntuariosAsync(Guid funcionarioId)
    {
        return await _context.Prontuarios.AnyAsync(p => p.FuncionarioId == funcionarioId);
    }
}
