using System;
using DAL.Abstractions;
using DbWorks.Contexts;
using DbWorks.Models;

namespace DAL.UnitOfWorks
{
    public class SalesDbUnitOfWork : IUnitOfWork
    {
        private readonly SalesDbContext _context;
        private IGenericRepository<Customer> _customerRepository;
        private IGenericRepository<Manager> _managerRepository;
        private IGenericRepository<Order> _orderRepository;
        private IGenericRepository<Product> _productRepository;
        private readonly IRepositoryFactory _repositoryFactory;
        private bool _isDisposed = false;

        public IGenericRepository<Customer> CustomerRepository =>
            _customerRepository ??= _repositoryFactory.CreateInstance<Customer>(_context);

        public IGenericRepository<Manager> ManagerRepository => 
            _managerRepository ??= _repositoryFactory.CreateInstance<Manager>(_context);

        public IGenericRepository<Order> OrderRepository =>
            _orderRepository ??= _repositoryFactory.CreateInstance<Order>(_context);

        public IGenericRepository<Product> ProductRepository =>
            _productRepository ??= _repositoryFactory.CreateInstance<Product>(_context);

        public SalesDbUnitOfWork(SalesDbContext context, IRepositoryFactory repositoryFactory)
        {
            _context = context;
            _repositoryFactory = repositoryFactory;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            CustomerRepository.Dispose();
            ManagerRepository.Dispose();
            OrderRepository.Dispose();
            ProductRepository.Dispose();

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}