using DAL.Repositories;
using DAL.RepositoryFactories;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DALTests.FactoriesTests
{
    [TestClass]
    public class GenericRepositoryFactoryTests
    {
        [TestMethod]
        public void TestRepositoryFactoryClassReturningElement()
        {
            var dbContext = new Mock<DbContext>().Object;
            var genericRepositoryFactory = new GenericRepositoryFactory();

            var expectedGenericRepository = new GenericRepository<Customer>(dbContext);
            var actualGenericRepository = genericRepositoryFactory.CreateInstance<Customer>(dbContext);

            Assert.AreEqual(actualGenericRepository.ToString(), expectedGenericRepository.ToString());
        }
    }
}