using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BL.Abstractions;
using BL.Abstractions.CsvMapping;
using BL.DataSourceParsers.FileParsersFactories;
using BL.SalesDataSourceDTOs;
using TinyCsvParser;

namespace BL.DataSourceParsers.FileParsers
{
    public class FileParser : IFileParser
    {
        private readonly string _filePath;

        public FileParser(string fullPath)
        {
            Verify(fullPath);

            _filePath = fullPath;
        }

        private static void Verify(string fullPath)
        {
            if (fullPath is null)
            {
                throw new ArgumentNullException(nameof(fullPath));
            }
        }

        public IEnumerable<SalesDataSourceDTO> ReadFile()
        {
            var csvParserOptions = new CsvParserOptions(true, ';');
            var csvReader = new CsvParser<FileContentDTO>(csvParserOptions, new CsvFileContentMapping());
            var records = csvReader.ReadFromFile(_filePath, Encoding.UTF8);

            var managerLastName = new Regex("[A-Za-z]+")
                .Match(Path.GetFileName(_filePath)).Value;

            foreach (var fileContentDto in records)
            {
                yield return new SalesDataSourceHandler(fileContentDto.Result, managerLastName)
                    .GetSalesDataSourceDTO();
            }
        }
    }
}