
using PetShoop.Domain.Pagination;
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

    public async Task<Cliente?> GetByIdAsync(Guid? id)
    {
        if (id == null)
            return null;

        return await _context.Clientes.AsNoTracking().SingleOrDefaultAsync(c => c.ClienteId == id);
    }

    public async Task<IEnumerable<Cliente>> GetClientesAsync()
    {
        return await _context.Clientes.AsNoTracking().ToListAsync();
    }

    //paginaçao
    public async Task<PagedList<Cliente>> GetClientesPagedAsync(int pageNumber, int pageSize)
    {
        var count = await _context.Clientes.CountAsync();
        var items = await _context.Clientes.AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<Cliente>(items, count, pageNumber, pageSize);
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

    public async Task<bool> HasPetsAsync(Guid clienteId)
    {
        return await _context.Pets.AnyAsync(p => p.ClienteId == clienteId);
    }

    public async Task<bool> HasVendasAsync(Guid clienteId)
    {
        return await _context.Vendas.AnyAsync(v => v.ClienteId == clienteId);
    }
}
