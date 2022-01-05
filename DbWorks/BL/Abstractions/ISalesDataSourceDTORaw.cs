namespace BL.Abstractions
{
    public interface ISalesDataSourceDTORaw
    {
        string CustomerFirstName { get; set; }

        string CustomerLastName { get; set; }

        string ManagerLastName { get; set; }

        string ProductName { get; set; }

        string ProductPrice { get; set; }

        string OrderDate { get; set; }

        string OrderSum { get; set; }
    }
}