using System;
using DAL.Abstractions.Factories;
using DAL.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DALTests.UnitOfWorksTests
{
    [TestClass]
    public class SalesDbUnitOfWorkTests
    {
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
