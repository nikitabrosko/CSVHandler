namespace BL.Abstractions
{
    public interface ISalesDataSourceDTOHandler
    {
        ISalesDataSourceDTORaw SalesDataSourceDTORaw { get; }
        ISalesDataSourceDTO TransformToSalesDataSourceDTO();
    }
}