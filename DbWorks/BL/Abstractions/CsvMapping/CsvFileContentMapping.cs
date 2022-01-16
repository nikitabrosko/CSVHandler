using BL.SalesDataSourceDTOs;
using TinyCsvParser.Mapping;

namespace BL.Abstractions.CsvMapping
{
    public class CsvFileContentMapping : CsvMapping<SalesDataSourceHandler>
    {
        public CsvFileContentMapping() 
            : base()
        {
            MapProperty(0, x => x.OrderDate);
            MapProperty(1, x => x.CustomerFullName);
            MapProperty(2, x => x.ProductRecord);
            MapProperty(3, x => x.OrderSum);
        }
    }
}