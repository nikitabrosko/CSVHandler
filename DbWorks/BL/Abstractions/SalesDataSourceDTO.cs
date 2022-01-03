using DbWorks.Models;

namespace BL.Abstractions
{
    public class SalesDataSourceDTO
    {
        public Customer Customer { get; set; }

        public Manager Manager { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}