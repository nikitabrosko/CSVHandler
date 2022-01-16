using System;

namespace BL.SalesDataSourceDTOs
{
    public class FileContentDTO
    {
        public DateTime OrderDate { get; set; }

        public string CustomerFullName { get; set; }

        public string ProductRecord { get; set; }

        public string OrderSum { get; set; }
    }
}