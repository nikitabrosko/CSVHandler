using BL.Abstractions;
using DbWorks.Models;

namespace BL.SalesDataSourceDTOs
{
    public class SalesDataSourceDTO : ISalesDataSourceDTO
    {
        public Customer Customer { get; set; }

        public Manager Manager { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}