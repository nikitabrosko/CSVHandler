using System;
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
            using var fileParser = new FileParser(DataAccessForTests.PathToFolder + DataAccessForTests.FileName);

            var expectedDataSourceDto = new SalesDataSourceHandler("Ivan", "Sidorov",
                    "Test", "Telephone", "5", "01.01.2022", "20")
                .GetSalesDataSourceDTO();

            var actualDataSourceDto = fileParser.ReadFile();

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