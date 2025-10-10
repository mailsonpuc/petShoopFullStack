
using petBack.Context;
using petBack.Models;
using petBack.Repositories.Interfaces;

namespace petBack.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {       
        }
    }
}