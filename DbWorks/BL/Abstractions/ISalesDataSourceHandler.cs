using BL.SalesDataSourceDTOs;

namespace BL.Abstractions
{
    public interface ISalesDataSourceHandler
    {
        string CustomerFirstName { get; }
        string CustomerLastName { get; }
        string ManagerLastName { get; }
        string ProductName { get; }
        string ProductPrice { get; }
        string OrderDate { get; }
        string OrderSum { get; }
        SalesDataSourceDTO GetSalesDataSourceDTO();
    }
}