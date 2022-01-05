using BL.Abstractions;
using BL.Abstractions.Factories;
using BL.DataSourceParsers.FileParsers;

namespace BL.DataSourceParsers.FileParsersFactories
{
    public class FileParserFactory : IFileParserFactory
    {
        public IFileParser CreateInstance(string path)
        {
            return new FileParser(path);
        }
    }
}