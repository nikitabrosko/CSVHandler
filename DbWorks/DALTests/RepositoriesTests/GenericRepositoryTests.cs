using System;
using System.Data.Entity;
using DAL.Repositories;
using DbWorks.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DALTests.RepositoriesTests
{
    [TestClass]
    public class GenericRepositoryTests
    {
        [TestMethod]
        public void TestGenericRepositoryClassCreatingWithValidParameters()
        {
            var dbContext = new Mock<DbContext>().Object;

            var genericRepository = new GenericRepository<Customer>(dbContext);

            Assert.IsTrue(Equals(genericRepository.Context, dbContext)
                          && genericRepository.IsDisposed is false);

            genericRepository.Dispose();
        }

        [TestMethod]
        public void TestGenericRepositoryClassCreatingWithInvalidParametersDbContextIsNull()
        {
            DbContext dbContext = null;

            Assert.ThrowsException<ArgumentNullException>(() => new GenericRepository<Customer>(dbContext));
        }

        [TestMethod]
        public void TestGenericRepositoryClassDisposeMethod()
        {
            var dbContext = new Mock<DbContext>().Object;

            var genericRepository = new GenericRepository<Customer>(dbContext);

            genericRepository.Dispose();

            Assert.IsTrue(genericRepository.IsDisposed);
        }
    }
}