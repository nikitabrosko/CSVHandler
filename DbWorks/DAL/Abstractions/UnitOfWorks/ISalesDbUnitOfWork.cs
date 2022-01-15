using DatabaseLayer.Models;

namespace DAL.Abstractions.UnitOfWorks
{
    public interface ISalesDbUnitOfWork : IUnitOfWork
    {
        IGenericRepository<Customer> CustomerRepository { get; }
        IGenericRepository<Manager> ManagerRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
    }
}