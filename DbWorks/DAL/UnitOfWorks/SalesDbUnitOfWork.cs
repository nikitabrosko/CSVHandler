using System;
using DAL.Abstractions;
using DAL.Abstractions.Factories;
using DAL.Abstractions.UnitOfWorks;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.UnitOfWorks
{
    public class SalesDbUnitOfWork : ISalesDbUnitOfWork
    {
        private readonly DbContext _context;
        private IGenericRepository<Customer> _customerRepository;
        private IGenericRepository<Manager> _managerRepository;
        private IGenericRepository<Order> _orderRepository;
        private IGenericRepository<Product> _productRepository;
        private readonly IGenericRepositoryFactory _repositoryFactory;

        public bool IsDisposed { get; protected set; }

        public IGenericRepository<Customer> CustomerRepository =>
            _customerRepository ??= _repositoryFactory.CreateInstance<Customer>(_context);

        public IGenericRepository<Manager> ManagerRepository => 
            _managerRepository ??= _repositoryFactory.CreateInstance<Manager>(_context);

        public IGenericRepository<Order> OrderRepository =>
            _orderRepository ??= _repositoryFactory.CreateInstance<Order>(_context);

        public IGenericRepository<Product> ProductRepository =>
            _productRepository ??= _repositoryFactory.CreateInstance<Product>(_context);

        public SalesDbUnitOfWork(DbContext context, IGenericRepositoryFactory repositoryFactory)
        {
            Verify(context, repositoryFactory);

            _context = context;
            _repositoryFactory = repositoryFactory;
        }

        private static void Verify(DbContext context, IGenericRepositoryFactory repositoryFactory)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (repositoryFactory is null)
            {
                throw new ArgumentNullException(nameof(repositoryFactory));
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                IsDisposed = true;
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