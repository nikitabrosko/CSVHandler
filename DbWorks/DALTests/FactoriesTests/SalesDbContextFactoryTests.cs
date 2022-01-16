using System;
using DAL.SalesDbContextFactories;
using DatabaseLayer.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DALTests.FactoriesTests
{
    [TestClass]
    public class SalesDbContextFactoryTests
    {
        [TestMethod]
        public void TestSalesDbContextFactoryClassReturningElementDbConnectionIsNull()
        {
            DbContextOptions<SalesDbContext> contextOptions = null;
            var salesDbContextFactory = new SalesDbContextFactory();

            Assert.ThrowsException<ArgumentNullException>(() => salesDbContextFactory.CreateInstance(contextOptions));
        }
    }
}