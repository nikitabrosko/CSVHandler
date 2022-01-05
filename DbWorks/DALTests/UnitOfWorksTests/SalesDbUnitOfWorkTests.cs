using System;
using System.Data.Common;
using System.Data.Entity;
using DAL.Abstractions.Factories;
using DAL.Repositories;
using DAL.RepositoryFactories;
using DAL.UnitOfWorks;
using DbWorks.Models;
using DbWorks.Contexts;
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
            var dbConnectionMock = new Mock<DbConnection>();
            var dbContext = new SalesDbContext(dbConnectionMock.Object, true);
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
            var dbConnectionMock = new Mock<DbConnection>();
            var dbContext = new SalesDbContext(dbConnectionMock.Object, true);
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
            var dbConnectionMock = new Mock<DbConnection>();
            var dbContext = new SalesDbContext(dbConnectionMock.Object, true);
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
            var dbConnectionMock = new Mock<DbConnection>();
            var dbContext = new SalesDbContext(dbConnectionMock.Object, true);
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
            var dbConnectionMock = new Mock<DbConnection>();
            var dbContext = new SalesDbContext(dbConnectionMock.Object, true);
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
            var dbContextMock = new Mock<DbContext>();
            IGenericRepositoryFactory repositoryFactory = null;

            Assert.ThrowsException<ArgumentNullException>(() =>
                new SalesDbUnitOfWork(dbContextMock.Object, repositoryFactory), nameof(IGenericRepositoryFactory));
        }
    }
}
