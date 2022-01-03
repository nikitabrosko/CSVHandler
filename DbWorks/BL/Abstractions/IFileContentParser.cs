namespace BL.Abstractions
{
    public interface IFileContentParser
    {
        string ReadDataRecord();
        string[] ReadCustomerRecord();
        string[] ReadProductRecord();
        string ReadSumRecord();
    }
}