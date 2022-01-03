namespace BL.Abstractions
{
    public interface ISalesDataSourceDTOHandler
    {
        SalesDataSourceDTORaw SalesDataSourceDTORaw { get; }
        SalesDataSourceDTO TransformToSalesDataSourceDTO();
    }
}