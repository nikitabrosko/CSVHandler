using System.IO;
using BL.Abstractions;

namespace BL.DataSourceParsers.FileParsers
{
    public class FileParser : IFileParser
    {
        private readonly string _filePath;
        private readonly TextReader _reader;

        public FileParser(string path)
        {
            _filePath = path;
            _reader = new StreamReader(path);
        }

        public SalesDataSourceDTORaw ReadFile()
        {
            var fileNameParser = new FileNameParser(_filePath);
            var fileContentParser = new FileContentParser(_reader.ReadToEnd());
            var customerInfo = fileContentParser.ReadCustomerRecord();
            var productInfo = fileContentParser.ReadProductRecord();

            return new SalesDataSourceDTORaw
            {
                CustomerFirstName = customerInfo[0],
                CustomerLastName = customerInfo[1],
                ProductName = productInfo[0],
                ProductPrice = productInfo[1],
                ManagerLastName = fileNameParser.GetLastName(),
                OrderDate = fileNameParser.GetDate(),
                OrderSum = fileContentParser.ReadSumRecord()
            };
        }
    }
}