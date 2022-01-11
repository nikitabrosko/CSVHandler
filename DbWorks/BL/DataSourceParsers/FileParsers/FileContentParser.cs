using System;
using System.Linq;
using System.Text;
using BL.Abstractions;

namespace BL.DataSourceParsers.FileParsers
{
    public class FileContentParser : IFileContentParser
    {
        private readonly string[] _records;

        public FileContentParser(string content)
        {
            Verify(content);

            _records = content.Split(";");
        }

        private static void Verify(string contentToVerify)
        {
            if (contentToVerify is null)
            {
                throw new ArgumentNullException(nameof(contentToVerify));
            }
        }

        public string ReadDateRecord()
        {
            var dateRecord = _records[0];

            if (string.IsNullOrWhiteSpace(dateRecord))
            {
                throw new ArgumentException("Date record in file content is empty or whitespace");
            }

            var newDate = new StringBuilder();

            newDate.Append(dateRecord.Substring(0, 2))
                .Append('.')
                .Append(dateRecord.Substring(2, 2))
                .Append('.')
                .Append(dateRecord.Substring(4, 4));

            return newDate.ToString();
        }

        public string[] ReadCustomerRecord()
        {
            var customerRecord = _records[1].Split(" ");

            if (customerRecord.Any(string.IsNullOrWhiteSpace))
            {
                throw new ArgumentException("Customer record in file content is empty or whitespace");
            }

            return customerRecord;
        }

        public string[] ReadProductRecord()
        {
            var productRecord = _records[2].Split(", ");

            if (productRecord.Any(string.IsNullOrWhiteSpace))
            {
                throw new ArgumentException("Product record in file content is empty or whitespace");
            }

            return productRecord;
        }

        public string ReadSumRecord()
        {
            var sumRecord = _records[3];

            if (string.IsNullOrWhiteSpace(sumRecord))
            {
                throw new ArgumentException("Sum record in file content is empty or whitespace");
            }

            return sumRecord;
        }
    }
}