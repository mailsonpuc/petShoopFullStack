using PetShoop.Domain.Entities;
using PetShoop.Domain.Interfaces;
using PetShoop.Domain.Pagination;
using PetShoop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace PetShoop.Infrastructure.Repositories;

public class PetRepository : IPetRepository
{
    private readonly AppDbContext _context;

    public PetRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Pet> CreateAsync(Pet pet)
    {
        _context.Add(pet);
        await _context.SaveChangesAsync();
        return pet;
    }

    public async Task<Pet?> GetByIdAsync(Guid? id)
    {
        if (id == null)
            return null;

        return await _context.Pets.AsNoTracking().SingleOrDefaultAsync(p => p.PetId == id);
    }

    public async Task<IEnumerable<Pet>> GetPetsAsync()
    {
        return await _context.Pets.AsNoTracking().ToListAsync();
    }

    public async Task<PagedList<Pet>> GetPetsPagedAsync(int pageNumber, int pageSize)
    {
        return await PagedList<Pet>.ToPagedListAsync(_context.Pets.AsNoTracking(), pageNumber, pageSize);
    }

    public async Task<Pet> RemoveAsync(Pet pet)
    {
        _context.Remove(pet);
        await _context.SaveChangesAsync();
        return pet;
    }

    public async Task<Pet> UpdateAsync(Pet pet)
    {
        _context.Update(pet);
        await _context.SaveChangesAsync();
        return pet;
    }

    public async Task<bool> HasAgendamentosAsync(Guid petId)
    {
        return await _context.Agendamentos.AnyAsync(a => a.PetId == petId);
    }

    public async Task<bool> HasConsultasAsync(Guid petId)
    {
        return await _context.Consultas.AnyAsync(c => c.PetId == petId);
    }

    public async Task<bool> HasProntuariosAsync(Guid petId)
    {
        return await _context.Prontuarios.AnyAsync(p => p.PetId == petId);
    }

    public async Task<bool> HasVacinasAsync(Guid petId)
    {
        return await _context.Vacinas.AnyAsync(v => v.PetId == petId);
    }
}
