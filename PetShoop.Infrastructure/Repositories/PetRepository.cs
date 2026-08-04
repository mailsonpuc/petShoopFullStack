using PetShoop.Domain.Entities;
using PetShoop.Domain.Interfaces;
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

    public async Task<Pet> GetByIdAsync(Guid? id)
    {
        return (await _context.Pets.SingleOrDefaultAsync(p => p.PetId == id))!;
    }

    public async Task<IEnumerable<Pet>> GetPetsAsync()
    {
        return await _context.Pets.ToListAsync();
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
}
