using System;
using System.Data.Common;
using DAL.Abstractions.Factories;
using DAL.Repositories;
using DAL.RepositoryFactories;
using DAL.UnitOfWorks;
using DatabaseLayer.Contexts;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DALTests.UnitOfWorksTests
{
    [TestClass]
    public class SalesDbUnitOfWorkTests
    {
        [TestMethod]
        public void TestSalesDbUnitOfWorkClassGetCustomerRepository()
        {
            var contextOptions = new Mock<DbContextOptions<SalesDbContext>>().Object;
            var dbContext = new SalesDbContext(contextOptions);
            var repositoryFactory = new GenericRepositoryFactory();

            var salesDbUnitOfWorkObject = new SalesDbUnitOfWork(dbContext, repositoryFactory);

            var expectedRepository = new GenericRepository<Customer>(dbContext);
            var actualRepository = salesDbUnitOfWorkObject.CustomerRepository;

            Assert.AreEqual(expectedRepository.ToString(), actualRepository.ToString());

            salesDbUnitOfWorkObject.Dispose();
        }

        [TestMethod]
        public void TestSalesDbUnitOfWorkClassGetManagerRepository()
        {
            var contextOptions = new Mock<DbContextOptions<SalesDbContext>>().Object;
            var dbContext = new SalesDbContext(contextOptions);
            var repositoryFactory = new GenericRepositoryFactory();

            var salesDbUnitOfWorkObject = new SalesDbUnitOfWork(dbContext, repositoryFactory);

            var expectedRepository = new GenericRepository<Manager>(dbContext);
            var actualRepository = salesDbUnitOfWorkObject.ManagerRepository;

            Assert.AreEqual(expectedRepository.ToString(), actualRepository.ToString());

            salesDbUnitOfWorkObject.Dispose();
        }

        [TestMethod]
        public void TestSalesDbUnitOfWorkClassGetOrderRepository()
        {
            var contextOptions = new Mock<DbContextOptions<SalesDbContext>>().Object;
            var dbContext = new SalesDbContext(contextOptions);
            var repositoryFactory = new GenericRepositoryFactory();

            var salesDbUnitOfWorkObject = new SalesDbUnitOfWork(dbContext, repositoryFactory);

            var expectedRepository = new GenericRepository<Order>(dbContext);
            var actualRepository = salesDbUnitOfWorkObject.OrderRepository;

            Assert.AreEqual(expectedRepository.ToString(), actualRepository.ToString());

            salesDbUnitOfWorkObject.Dispose();
        }

        [TestMethod]
        public void TestSalesDbUnitOfWorkClassGetProductRepository()
        {
            var contextOptions = new Mock<DbContextOptions<SalesDbContext>>().Object;
            var dbContext = new SalesDbContext(contextOptions);
            var repositoryFactory = new GenericRepositoryFactory();

            var salesDbUnitOfWorkObject = new SalesDbUnitOfWork(dbContext, repositoryFactory);

            var expectedRepository = new GenericRepository<Product>(dbContext);
            var actualRepository = salesDbUnitOfWorkObject.ProductRepository;

            Assert.AreEqual(expectedRepository.ToString(), actualRepository.ToString());

            salesDbUnitOfWorkObject.Dispose();
        }

        [TestMethod]
        public void TestSalesDbUnitOfWorkClassDisposeMethod()
        {
            var contextOptions = new Mock<DbContextOptions<SalesDbContext>>().Object;
            var dbContext = new SalesDbContext(contextOptions);
            var repositoryFactory = new GenericRepositoryFactory();

            var salesDbUnitOfWorkObject = new SalesDbUnitOfWork(dbContext, repositoryFactory);

            salesDbUnitOfWorkObject.Dispose();

            Assert.IsTrue(salesDbUnitOfWorkObject.IsDisposed
                          && salesDbUnitOfWorkObject.CustomerRepository.IsDisposed
                          && salesDbUnitOfWorkObject.ManagerRepository.IsDisposed
                          && salesDbUnitOfWorkObject.OrderRepository.IsDisposed
                          && salesDbUnitOfWorkObject.ProductRepository.IsDisposed);
        }

        [TestMethod]
        public void TestSalesDbUnitOfWorkClassCreatingWithInvalidParametersDbConnectionIsNull()
        {
            DbContext dbContext = null;
            var repositoryFactoryMock = new Mock<IGenericRepositoryFactory>();

            Assert.ThrowsException<ArgumentNullException>(() =>
                new SalesDbUnitOfWork(dbContext, repositoryFactoryMock.Object), nameof(DbContext));
        }

        [TestMethod]
        public void TestSalesDbUnitOfWorkClassCreatingWithInvalidParametersRepositoryFactoryIsNull()
        {
            var dbContext = new Mock<DbContext>().Object;
            IGenericRepositoryFactory repositoryFactory = null;

            Assert.ThrowsException<ArgumentNullException>(() =>
                new SalesDbUnitOfWork(dbContext, repositoryFactory), nameof(IGenericRepositoryFactory));
        }
    }
}
