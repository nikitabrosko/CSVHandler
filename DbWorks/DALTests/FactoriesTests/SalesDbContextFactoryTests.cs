using System;
using DAL.SalesDbContextFactories;
using DatabaseLayer.Contexts;
using Microsoft.EntityFrameworkCore;
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
            var contextOptions = new Mock<DbContextOptions<SalesDbContext>>().Object;
            var salesDbContextFactory = new SalesDbContextFactory();

            var expectedSalesDbContext = new SalesDbContext(contextOptions);
            var actualSalesDbContext = salesDbContextFactory.CreateInstance(contextOptions);

            Assert.AreEqual(actualSalesDbContext.GetType(), expectedSalesDbContext.GetType());
        }

        [TestMethod]
        public void TestSalesDbContextFactoryClassReturningElementDbConnectionIsNull()
        {
            DbContextOptions<SalesDbContext> contextOptions = null;
            var salesDbContextFactory = new SalesDbContextFactory();

            Assert.ThrowsException<ArgumentNullException>(() => salesDbContextFactory.CreateInstance(contextOptions));
        }
    }
}