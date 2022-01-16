using System;
using DatabaseLayer.Models;

namespace BL.SalesDataSourceDTOs
{
    public class SalesDataSourceHandler
    {
        public FileContentDTO FileContent { get; }

        public string ManagerLastName { get; }

        public SalesDataSourceHandler(FileContentDTO fileContent, string managerLastName)
        {
            Verify(fileContent, managerLastName);

            FileContent = fileContent;
            ManagerLastName = managerLastName;
        }

        public SalesDataSourceDTO GetSalesDataSourceDTO()
        {
            
            var customer = new Customer
            {
                FirstName = FileContent.CustomerFullName.Split(' ')[0],
                LastName = FileContent.CustomerFullName.Split(' ')[1]
            };

            var manager = new Manager
            {
                LastName = ManagerLastName
            };

            var product = new Product
            {
                Name = FileContent.ProductRecord.Split(", ")[0],
                Price = decimal.Parse(FileContent.ProductRecord.Split(", ")[1])
            };

            var order = new Order
            {
                Date = FileContent.OrderDate,
                Sum = decimal.Parse(FileContent.OrderSum),
                Customer = customer,
                Manager = manager,
                Product = product
            };

            return new SalesDataSourceDTO(customer, manager, order, product);
        }

        private static void Verify(FileContentDTO fileContent,
            string managerLastName)
        {
            var customerFullNameSplit = fileContent.CustomerFullName.Split(' ');
            var productRecordSplit = fileContent.ProductRecord.Split(", ");

            if (string.IsNullOrWhiteSpace(customerFullNameSplit[0]))
            {
                throw new ArgumentException("argument is null, or empty, or whitespace", nameof(fileContent));
            }

            if (string.IsNullOrWhiteSpace(customerFullNameSplit[1]))
            {
                throw new ArgumentException("argument is null, or empty, or whitespace", nameof(fileContent));
            }

            if (string.IsNullOrWhiteSpace(managerLastName))
            {
                throw new ArgumentException("argument is null, or empty, or whitespace", nameof(fileContent));
            }

            if (string.IsNullOrWhiteSpace(productRecordSplit[0]))
            {
                throw new ArgumentException("argument is null, or empty, or whitespace", nameof(fileContent));
            }

            if (string.IsNullOrWhiteSpace(productRecordSplit[1]))
            {
                throw new ArgumentException("argument is null, or empty, or whitespace", nameof(fileContent));
            }

            if (string.IsNullOrWhiteSpace(fileContent.OrderSum))
            {
                throw new ArgumentException("argument is null, or empty, or whitespace", nameof(fileContent));
            }
        }
    }
}