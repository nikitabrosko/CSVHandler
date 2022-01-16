using System;
using System.Linq;
using BL.Abstractions;
using BL.DataSourceParsers.FileParsers;
using BL.SalesDataSourceDTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLTests.DataSourceParsersTests.FileParsersTests
{
    [TestClass]
    public class FileParserTests
    {
        [TestMethod]
        public void CreatingFileParserWithInvalidParametersFullPathIsNull()
        {
            string fullPath = null;

            Assert.ThrowsException<ArgumentNullException>(() => new FileParser(fullPath));
        }

        [TestMethod]
        public void FileParserParseFileMethodTest()
        {
            var fileParser = new FileParser(DataAccessForTests.PathToFolder + DataAccessForTests.FileName);

            var fileContent = new FileContentDTO
            {
                CustomerFullName = "Ivan Sidorov",
                OrderDate = DateTime.Parse("01/01/2022"),
                OrderSum = "20",
                ProductRecord = "Telephone, 5"
            };

            var expectedDataSourceDto = 
                new SalesDataSourceHandler(fileContent, "Test")
                .GetSalesDataSourceDTO();

            var actualDataSourceDto = fileParser.ReadFile().First();

            Assert.IsTrue(CheckTwoSalesDataSourcesDto(expectedDataSourceDto, actualDataSourceDto));
        }

        private static bool CheckTwoSalesDataSourcesDto(SalesDataSourceDTO firstDataSource, SalesDataSourceDTO secondDataSource)
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