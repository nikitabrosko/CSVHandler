using System;
using DatabaseLayer.Models;

namespace BL.SalesDataSourceDTOs
{
    public class SalesDataSourceHandler
    {
        public DateTime OrderDate { get; set; }

        public string CustomerFullName { get; set; }

        public string ProductRecord { get; set; }

        public string OrderSum { get; set; }

        public string ManagerLastName { get; set; }

        public SalesDataSourceDTO GetSalesDataSourceDTO()
        {
            
            var customer = new Customer
            {
                FirstName = CustomerFullName.Split(' ')[0],
                LastName = CustomerFullName.Split(' ')[1]
            };

            var manager = new Manager
            {
                LastName = ManagerLastName
            };

            var product = new Product
            {
                Name = ProductRecord.Split(", ")[0],
                Price = decimal.Parse(ProductRecord.Split(", ")[1])
            };

            var order = new Order
            {
                Date = OrderDate,
                Sum = decimal.Parse(OrderSum),
                Customer = customer,
                Manager = manager,
                Product = product
            };

            return new SalesDataSourceDTO(customer, manager, order, product);
        }
    }
}