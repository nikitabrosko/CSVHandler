using System.IO;
using Microsoft.Extensions.Configuration;

namespace BLTests.DataSourceParsersTests
{
    public static class DataAccessForTests
    {
        public static string PathToFolder =>
            new ConfigurationBuilder()
                .AddJsonFile(Path.GetFullPath(@"..\\..\\..\\DataSourceParsersTests\\TestsSettings.json"))
                .Build()
                .GetSection("TestFilesFolders:FilesForTests").Value;

        public static string FileName => "Test_01012022.csv";

        public static string FileText => "01012022;Ivan Sidorov;Telephone, 5;20";

        public static void CreateFileIfNotExists()
        {
            var fullPath = PathToFolder + FileName;

            if (!File.Exists(fullPath))
            {
                File.Create(fullPath);
                File.WriteAllText(fullPath, FileText);
            }
        }
    }
}