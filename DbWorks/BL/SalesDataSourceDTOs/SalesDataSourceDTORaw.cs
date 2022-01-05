using BL.Abstractions;

namespace BL.SalesDataSourceDTOs
{
    public class SalesDataSourceDTORaw : ISalesDataSourceDTORaw
    {
        public string CustomerFirstName { get; set; }

        public string CustomerLastName { get; set; }

        public string ManagerLastName { get; set; }

        public string ProductName { get; set; }

        public string ProductPrice { get; set; }

        public string OrderDate { get; set; }

        public string OrderSum { get; set; }
    }
}