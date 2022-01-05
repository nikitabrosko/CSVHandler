namespace BL.Abstractions.Factories
{
    public interface IFileParserFactory
    {
        IFileParser CreateInstance(string path);
    }
}