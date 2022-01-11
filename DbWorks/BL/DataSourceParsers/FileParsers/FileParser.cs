using System;
using System.IO;
using System.Linq;
using BL.Abstractions;
using BL.DataSourceParsers.FileParsersFactories;
using BL.SalesDataSourceDTOs;

namespace BL.DataSourceParsers.FileParsers
{
    public class FileParser : IFileParser
    {
        private readonly string _filePath;
        private readonly TextReader _reader;

        public FileParser(string fullPath)
        {
            Verify(fullPath);

            _filePath = fullPath;
            _reader = new StreamReader(fullPath);
        }

        private static void Verify(string fullPath)
        {
            if (fullPath is null)
            {
                throw new ArgumentNullException(nameof(fullPath));
            }
        }

        public ISalesDataSourceDTO ReadFile()
        {
            var fileNameParser = new FileNameParserFactory()
                .CreateInstance(_filePath.Split("\\").Last());
            var fileContentParser = new FileContentParserFactory()
                .CreateInstance(_reader.ReadToEnd());
            var customerInfo = fileContentParser.ReadCustomerRecord();
            var productInfo = fileContentParser.ReadProductRecord();

            return new SalesDataSourceHandler(customerInfo[0], customerInfo[1],
                fileNameParser.GetLastName(), productInfo[0], productInfo[1],
                    fileContentParser.ReadDateRecord(), fileContentParser.ReadSumRecord())
                .GetSalesDataSourceDTO();
        }

        public void Dispose()
        {
            _reader.Close();
            _reader.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}