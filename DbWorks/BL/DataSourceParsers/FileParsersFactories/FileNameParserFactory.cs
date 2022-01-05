using BL.Abstractions;
using BL.Abstractions.Factories;
using BL.DataSourceParsers.FileParsers;

namespace BL.DataSourceParsers.FileParsersFactories
{
    public class FileNameParserFactory : IFileNameParserFactory
    {
        public IFileNameParser CreateInstance(string fileName)
        {
            return new FileNameParser(fileName);
        }
    }
}