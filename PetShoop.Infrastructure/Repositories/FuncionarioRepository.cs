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

    public async Task<Funcionario> GetByIdAsync(Guid? id)
    {
        return (await _context.Funcionarios.SingleOrDefaultAsync(f => f.FuncionarioId == id))!;
    }

    public async Task<IEnumerable<Funcionario>> GetFuncionariosAsync()
    {
        return await _context.Funcionarios.ToListAsync();
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
}
