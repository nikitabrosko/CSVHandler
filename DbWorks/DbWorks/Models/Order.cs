using System;

namespace DbWorks.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public Customer Customer { get; set; }
        public Manager Manager { get; set; }
        public Product Product { get; set; }
    }
}