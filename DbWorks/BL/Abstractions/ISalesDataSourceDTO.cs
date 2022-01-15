using DatabaseLayer.Models;

namespace BL.Abstractions
{
    public interface ISalesDataSourceDTO
    {
        Customer Customer { get; }
        Manager Manager { get; }
        Order Order { get; }
        Product Product { get; }
    }
}