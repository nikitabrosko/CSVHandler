using DbWorks.Models;

namespace BL.Abstractions
{
    public interface ISalesDataSourceDTO
    {
        Customer Customer { get; set; }

        Manager Manager { get; set; }

        Order Order { get; set; }

        Product Product { get; set; }
    }
}