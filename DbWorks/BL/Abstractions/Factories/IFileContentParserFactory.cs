namespace BL.Abstractions.Factories
{
    public interface IFileContentParserFactory
    {
        IFileContentParser CreateInstance(string content);
    }
}