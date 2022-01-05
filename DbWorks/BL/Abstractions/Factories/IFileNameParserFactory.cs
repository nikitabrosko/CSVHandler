namespace BL.Abstractions.Factories
{
    public interface IFileNameParserFactory
    {
        IFileNameParser CreateInstance(string fileName);
    }
}