using System;
using BL.Abstractions;
using BL.SalesDataSourceDTOs;
using DbWorks.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLTests.SalesDataSourceDTOsTests
{
    [TestClass]
    public class SalesDataSourceHandlerTests
    {
        [TestMethod]
        [DataRow(null, "Test", "Test", "Test", "5", "01012022", "20")]
        [DataRow("Test", null, "Test", "Test", "5", "01012022", "20")]
        [DataRow("Test", "Test", null, "Test", "5", "01012022", "20")]
        [DataRow("Test", "Test", "Test", null, "5", "01012022", "20")]
        [DataRow("Test", "Test", "Test", "Test", null, "01012022", "20")]
        [DataRow("Test", "Test", "Test", "Test", "5", null, "20")]
        [DataRow("Test", "Test", "Test", "Test", "5", "01012022", null)]
        public void SalesDataSourceHandlerCreatingWithInvalidParametersCustomerFirstNameIsNullTest(string customerFirstName,
            string customerLastName, string managerLastName, string productName, string productPrice, string orderDate, string orderSum)
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new SalesDataSourceHandler(customerFirstName, customerLastName, managerLastName, 
                    productName, productPrice, orderDate, orderSum));
        }

        [TestMethod]
        [DataRow("Test", "Test", "Test", "Test", "5", "01.01.2022", "20")]
        public void SalesDataSourceHandlerGetSalesDataSourceDtoMethodTest(string customerFirstName, string customerLastName, 
            string managerLastName, string productName, string productPrice, string orderDate, string orderSum)
        {
            var salesDataSourceHandler = new SalesDataSourceHandler(customerFirstName, customerLastName, 
                managerLastName, productName, productPrice, orderDate, orderSum);

            var customer = new Customer
            {
                FirstName = customerFirstName, 
                LastName = customerLastName
            };

            var manager = new Manager
            {
                LastName = managerLastName

            };

            var product = new Product
            {
                Name = productName, 
                Price = decimal.Parse(productPrice)
            };

            var order = new Order
            {
                Customer = customer,
                Manager = manager,
                Product = product,
                Date = DateTime.Parse(orderDate),
                Sum = decimal.Parse(orderSum)
            };

            var expectedDataSourceDto = new SalesDataSourceDTO(customer, manager, order, product);

            var actualDataSourceDto = salesDataSourceHandler.GetSalesDataSourceDTO();

            Assert.IsTrue(CheckTwoSalesDataSourcesDto(expectedDataSourceDto, actualDataSourceDto));
        }

        private static bool CheckTwoSalesDataSourcesDto(ISalesDataSourceDTO firstDataSource, ISalesDataSourceDTO secondDataSource)
        {
            var checkForCustomerEquals =
                Equals(firstDataSource.Customer.FirstName, secondDataSource.Customer.FirstName)
                && Equals(firstDataSource.Customer.LastName, secondDataSource.Customer.LastName)
                && Equals(firstDataSource.Customer.FullName, secondDataSource.Customer.FullName)
                && Equals(firstDataSource.Customer.Id, secondDataSource.Customer.Id)
                && Equals(firstDataSource.Customer.Orders.ToString(), secondDataSource.Customer.Orders.ToString());

            var checkForManagerEquals =
                Equals(firstDataSource.Manager.Id, secondDataSource.Manager.Id)
                && Equals(firstDataSource.Manager.LastName, secondDataSource.Manager.LastName)
                && Equals(firstDataSource.Manager.Orders.ToString(), secondDataSource.Manager.Orders.ToString());

            var checkForProductEquals =
                Equals(firstDataSource.Product.Id, secondDataSource.Product.Id)
                && Equals(firstDataSource.Product.Name, secondDataSource.Product.Name)
                && Equals(firstDataSource.Product.Orders.ToString(), secondDataSource.Product.Orders.ToString())
                && Equals(firstDataSource.Product.Price, secondDataSource.Product.Price);

            var checkForOrderEquals =
                Equals(firstDataSource.Order.Customer.ToString(), secondDataSource.Order.Customer.ToString())
                && Equals(firstDataSource.Order.Manager.ToString(), secondDataSource.Order.Manager.ToString())
                && Equals(firstDataSource.Order.Product.ToString(), secondDataSource.Order.Product.ToString())
                && Equals(firstDataSource.Order.Date, secondDataSource.Order.Date)
                && Equals(firstDataSource.Order.Id, secondDataSource.Order.Id)
                && Equals(firstDataSource.Order.Sum, secondDataSource.Order.Sum);

            return checkForCustomerEquals
                   && checkForManagerEquals
                   && checkForProductEquals
                   && checkForOrderEquals;
        }
    }
}