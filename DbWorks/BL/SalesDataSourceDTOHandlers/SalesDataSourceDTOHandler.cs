using System;
using BL.Abstractions;
using DbWorks.Models;

namespace BL.SalesDataSourceDTOHandlers
{
    public class SalesDataSourceDTOHandler : ISalesDataSourceDTOHandler
    {
        public SalesDataSourceDTORaw SalesDataSourceDTORaw { get; }

        public SalesDataSourceDTOHandler(SalesDataSourceDTORaw salesDataSourceDTORaw)
        {
            SalesDataSourceDTORaw = salesDataSourceDTORaw;
        }

        public SalesDataSourceDTO TransformToSalesDataSourceDTO()
        {
            var customer = new Customer
            {
                FirstName = SalesDataSourceDTORaw.CustomerFirstName, 
                LastName = SalesDataSourceDTORaw.CustomerLastName
            };

            var manager = new Manager
            {
                LastName = SalesDataSourceDTORaw.ManagerLastName
            };

            var product = new Product
            {
                Name = SalesDataSourceDTORaw.ProductName,
                Price = decimal.Parse(SalesDataSourceDTORaw.ProductPrice)
            };

            var order = new Order
            {
                Date = DateTime.Parse(SalesDataSourceDTORaw.OrderDate),
                Sum = decimal.Parse(SalesDataSourceDTORaw.OrderSum),
                Customer = customer,
                Manager = manager,
                Product = product
            };

            return new SalesDataSourceDTO
            {
                Customer = customer,
                Manager = manager,
                Product = product,
                Order = order
            };
        }
    }
}