using petBack.Context;
using petBack.Repositories.Interfaces;

namespace petBack.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProductRepository? _productRepo;

        public AppDbContext _context;


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }



        //product
        public IProductRepository ProductRepository
        {
            get
            {
                return _productRepo = _productRepo ?? new ProductRepository(_context);
            }
        }


        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }



        public void Dispose()
        {
            _context.Dispose();
        }


    }


}