
using PetShoop.Domain.Entities;
using PetShoop.Domain.Interfaces;
using PetShoop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace PetShoop.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Cliente> CreateAsync(Cliente cliente)
    {
        _context.Add(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> GetByIdAsync(Guid? id)
    {
        return (await _context.Clientes.AsNoTracking().SingleOrDefaultAsync(c => c.ClienteId == id))!;
    }

    public async Task<IEnumerable<Cliente>> GetClientesAsync()
    {
        return await _context.Clientes.AsNoTracking().ToListAsync();
    }

    public async Task<Cliente> RemoveAsync(Cliente cliente)
    {
        _context.Remove(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> UpdateAsync(Cliente cliente)
    {
        _context.Update(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }
}
