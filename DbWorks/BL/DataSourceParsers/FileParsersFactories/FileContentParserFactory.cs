using BL.Abstractions;
using BL.Abstractions.Factories;
using BL.DataSourceParsers.FileParsers;

namespace BL.DataSourceParsers.FileParsersFactories
{
    public class FileContentParserFactory : IFileContentParserFactory
    {
        public IFileContentParser CreateInstance(string content)
        {
            return new FileContentParser(content);
        }
    }
}