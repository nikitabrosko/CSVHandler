using System;
using BL.Abstractions;
using DatabaseLayer.Models;

namespace BL.SalesDataSourceDTOs
{
    public class SalesDataSourceHandler : ISalesDataSourceHandler
    {
        public string CustomerFirstName { get; }

        public string CustomerLastName { get; }

        public string ManagerLastName { get; }

        public string ProductName { get; }

        public string ProductPrice { get; }

        public string OrderDate { get; }

        public string OrderSum { get; }

        public SalesDataSourceHandler(string customerFirstName,
            string customerLastName,
            string managerLastName,
            string productName,
            string productPrice,
            string orderDate,
            string orderSum)
        {
            Verify(customerFirstName, customerLastName, managerLastName,
                productName, productPrice, orderDate, orderSum);

            CustomerFirstName = customerFirstName;
            CustomerLastName = customerLastName;
            ManagerLastName = managerLastName;
            ProductName = productName;
            ProductPrice = productPrice;
            OrderDate = orderDate;
            OrderSum = orderSum;
        }

        public ISalesDataSourceDTO GetSalesDataSourceDTO()
        {
            var customer = new Customer
            {
                FirstName = CustomerFirstName,
                LastName = CustomerLastName
            };

            var manager = new Manager
            {
                LastName = ManagerLastName
            };

            var product = new Product
            {
                Name = ProductName,
                Price = decimal.Parse(ProductPrice)
            };

            var order = new Order
            {
                Date = DateTime.Parse(OrderDate),
                Sum = decimal.Parse(OrderSum),
                Customer = customer,
                Manager = manager,
                Product = product
            };

            return new SalesDataSourceDTO(customer, manager, order, product);
        }

        private static void Verify(string customerFirstName,
            string customerLastName,
            string managerLastName,
            string productName,
            string productPrice,
            string orderDate,
            string orderSum)
        {
            if (customerFirstName is null)
            {
                throw new ArgumentNullException(nameof(customerFirstName));
            }

            if (customerLastName is null)
            {
                throw new ArgumentNullException(nameof(customerLastName));
            }

            if (managerLastName is null)
            {
                throw new ArgumentNullException(nameof(managerLastName));
            }

            if (productName is null)
            {
                throw new ArgumentNullException(nameof(productName));
            }

            if (productPrice is null)
            {
                throw new ArgumentNullException(nameof(productPrice));
            }

            if (orderDate is null)
            {
                throw new ArgumentNullException(nameof(orderDate));
            }

            if (orderSum is null)
            {
                throw new ArgumentNullException(nameof(orderSum));
            }
        }
    }
}