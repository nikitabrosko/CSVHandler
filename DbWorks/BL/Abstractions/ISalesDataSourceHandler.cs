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
        ISalesDataSourceDTO GetSalesDataSourceDTO();
    }
}