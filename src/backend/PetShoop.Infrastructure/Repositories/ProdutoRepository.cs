using PetShoop.Domain.Entities;
using PetShoop.Domain.Interfaces;
using PetShoop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace PetShoop.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Produto> CreateAsync(Produto produto)
    {
        _context.Add(produto);
        await _context.SaveChangesAsync();
        return produto;
    }

    public async Task<Produto> GetByIdAsync(Guid? id)
    {
        return (await _context.Produtos.AsNoTracking().SingleOrDefaultAsync(p => p.ProdutoId == id))!;
    }

    public async Task<IEnumerable<Produto>> GetProdutosAsync()
    {
        return await _context.Produtos.AsNoTracking().ToListAsync();
    }

    public async Task<Produto> RemoveAsync(Produto produto)
    {
        _context.Remove(produto);
        await _context.SaveChangesAsync();
        return produto;
    }

    public async Task<Produto> UpdateAsync(Produto produto)
    {
        _context.Update(produto);
        await _context.SaveChangesAsync();
        return produto;
    }
}
