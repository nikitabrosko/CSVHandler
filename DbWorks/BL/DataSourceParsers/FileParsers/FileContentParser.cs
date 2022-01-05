﻿using BL.Abstractions;

namespace BL.DataSourceParsers.FileParsers
{
    public class FileContentParser : IFileContentParser
    {
        private readonly string[] _records;

        public FileContentParser(string content)
        {
            _records = content.Split(";");
        }

        public string ReadDataRecord()
        {
            return _records[0];
        }

        public string[] ReadCustomerRecord()
        {
            return _records[1].Split(" ");
        }

        public string[] ReadProductRecord()
        {
            return _records[2].Split(", ");
        }

        public string ReadSumRecord()
        {
            return _records[3];
        }
    }
}