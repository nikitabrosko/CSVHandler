namespace BL.Abstractions
{
    public interface IFileContentParser
    {
        string ReadDateRecord();
        string[] ReadCustomerRecord();
        string[] ReadProductRecord();
        string ReadSumRecord();
    }
}