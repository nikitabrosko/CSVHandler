using System;
using BL.SalesDataSourceDTOs;
using DatabaseLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BLTests.SalesDataSourceDTOsTests
{
    [TestClass]
    public class SalesDataSourceDTOTests
    {
        [TestMethod]
        public void CreatingSalesDataSourceDtoWithInvalidParametersCustomerIsNull()
        {
            Customer customer = null;
            var manager = new Mock<Manager>().Object;
            var order = new Mock<Order>().Object;
            var product = new Mock<Product>().Object;

            Assert.ThrowsException<ArgumentNullException>(() =>
                new SalesDataSourceDTO(customer, manager, order, product));
        }

        [TestMethod]
        public void CreatingSalesDataSourceDtoWithInvalidParametersManagerIsNull()
        {
            var customer = new Mock<Customer>().Object;
            Manager manager = null;
            var order = new Mock<Order>().Object;
            var product = new Mock<Product>().Object;

            Assert.ThrowsException<ArgumentNullException>(() =>
                new SalesDataSourceDTO(customer, manager, order, product));
        }

        [TestMethod]
        public void CreatingSalesDataSourceDtoWithInvalidParametersOrderIsNull()
        {
            var customer = new Mock<Customer>().Object;
            var manager = new Mock<Manager>().Object;
            Order order = null;
            var product = new Mock<Product>().Object;

            Assert.ThrowsException<ArgumentNullException>(() =>
                new SalesDataSourceDTO(customer, manager, order, product));
        }

        [TestMethod]
        public void CreatingSalesDataSourceDtoWithInvalidParametersProductIsNull()
        {
            var customer = new Mock<Customer>().Object;
            var manager = new Mock<Manager>().Object;
            var order = new Mock<Order>().Object;
            Product product = null;

            Assert.ThrowsException<ArgumentNullException>(() =>
                new SalesDataSourceDTO(customer, manager, order, product));
        }

        [TestMethod]
        public void CreatingSalesDataSourceDtoWithValidParameters()
        {
            var customer = new Customer();
            var manager = new Manager();
            var order = new Order();
            var product = new Product();

            var salesDataSourceDtoObject = new SalesDataSourceDTO(customer, manager, order, product);

            Assert.IsTrue(salesDataSourceDtoObject.Customer.Equals(customer)
                          && salesDataSourceDtoObject.Manager.Equals(manager)
                          && salesDataSourceDtoObject.Order.Equals(order)
                          && salesDataSourceDtoObject.Product.Equals(product));
        }
    }
}