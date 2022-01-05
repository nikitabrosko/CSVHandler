using System.Data.Common;
using System.Data.Entity;
using DAL.Repositories;
using DAL.RepositoryFactories;
using DAL.SalesDbContextFactories;
using DbWorks.Contexts;
using DbWorks.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace DALTests.FactoriesTests
{
    [TestClass]
    public class SalesDbContextFactoryTests
    {
        [TestMethod]
        public void TestSalesDbContextFactoryClassReturningElement()
        {
            var dbConnection = new Mock<DbConnection>().Object;
            var salesDbContextFactory = new SalesDbContextFactory();

            var expectedSalesDbContext = new SalesDbContext(dbConnection, true);
            var actualSalesDbContext = salesDbContextFactory.CreateInstance(dbConnection, true);

            Assert.AreEqual(actualSalesDbContext.GetType(), expectedSalesDbContext.GetType());
        }
    }
}