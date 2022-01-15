using BL.Abstractions.ConfigurationOptions;

namespace BL.ConfigurationOptions
{
    public class FileConfigurationOptions : IFileConfigurationOptions
    {
        public string SourceDirectoryPath { get; set; }
        
        public string TargetDirectoryPath { get; set; }

        public string FilesExtension { get; set; }
    }
}