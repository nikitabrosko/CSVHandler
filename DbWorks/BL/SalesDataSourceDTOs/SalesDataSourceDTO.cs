using System;
using DatabaseLayer.Models;

namespace BL.SalesDataSourceDTOs
{
    public class SalesDataSourceDTO
    {
        public Customer Customer { get; }

        public Manager Manager { get; }

        public Order Order { get; }

        public Product Product { get; }

        public SalesDataSourceDTO(Customer customer, 
            Manager manager, Order order, Product product)
        {
            Verify(customer, manager, order, product);

            Customer = customer;
            Manager = manager;
            Order = order;
            Product = product;
        }

        private static void Verify(Customer customer, Manager manager, Order order, Product product)
        {
            if (customer is null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            if (manager is null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }
        }
    }
}