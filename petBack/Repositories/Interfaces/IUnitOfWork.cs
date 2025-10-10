namespace petBack.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }

        Task Commit();
    }
}