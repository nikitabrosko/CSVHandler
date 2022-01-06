using System;
using System.Data.Common;
using DAL.SalesDbContextFactories;
using DbWorks.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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

        [TestMethod]
        public void TestSalesDbContextFactoryClassReturningElementDbConnectionIsNull()
        {
            DbConnection dbConnection = null;
            var salesDbContextFactory = new SalesDbContextFactory();

            Assert.ThrowsException<ArgumentNullException>(() => salesDbContextFactory.CreateInstance(dbConnection));
        }
    }
}