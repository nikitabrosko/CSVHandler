namespace BL.Abstractions.ConfigurationOptions
{
    public interface IFileConfigurationOptions
    {
        string SourceDirectoryPath { get; }

        string TargetDirectoryPath { get; }

        string FilesExtension { get; }
    }
}