using System;
using System.IO;
using BL.FileManagers;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLTests.FileManagersTests
{
    [TestClass]
    public class FileManagerTests
    {
        private readonly string _sourceDirectoryPath =
            new ConfigurationBuilder()
                .AddJsonFile(Path.GetFullPath(@"..\\..\\..\\FileManagersTests\\TestsSettings.json"))
                .Build()
                .GetSection("TestFilesFolders:SourceFolder").Value;

        private readonly string _targetDirectoryPath =
            new ConfigurationBuilder()
                .AddJsonFile(Path.GetFullPath(@"..\\..\\..\\FileManagersTests\\TestsSettings.json"))
                .Build()
                .GetSection("TestFilesFolders:TargetFolder").Value;

        private readonly string _fileExtension =
            new ConfigurationBuilder()
                .AddJsonFile(Path.GetFullPath(@"..\\..\\..\\FileManagersTests\\TestsSettings.json"))
                .Build().GetSection("FilesExtensions:FilesExtensionForTests").Value;

        [TestMethod]
        [DataRow("", "testPath")]
        [DataRow(" ", "testPath")]
        [DataRow(null, "testPath")]
        [DataRow("testPath", "")]
        [DataRow("testPath", " ")]
        [DataRow("testPath", null)]
        public void FileManagerCreatingWithInvalidParametersTest(string directoryPath, string filesToWatchExtension)
        {
            Assert.ThrowsException<ArgumentException>(() => 
                    new FileManager(directoryPath, filesToWatchExtension),
                "argument is null, empty or whitespace");
        }

        [TestMethod]
        public void FileManagerDisposeMethodTest()
        {
            var fileManager = new FileManager(_sourceDirectoryPath, _fileExtension);

            fileManager.Dispose();

            Assert.IsTrue(fileManager.IsDisposed);
        }

        [TestMethod]
        public void FileManagerMoveFileToAnotherDirectoryMethodTest()
        {
            var fileName = "TestFirst_01012022.csv";

            if (!File.Exists(_sourceDirectoryPath + fileName))
            {
                File.CreateText(_sourceDirectoryPath + fileName).Close();
            }

            var fileManager = new FileManager(_sourceDirectoryPath, _fileExtension);
            fileManager.MoveFileToAnotherDirectory(_targetDirectoryPath, fileName);

            Assert.IsTrue(File.Exists(_targetDirectoryPath + fileName));

            File.Delete(_targetDirectoryPath + fileName);
            fileManager.Dispose();
        }

        [TestMethod]
        public void FileManagerMoveFileToAnotherDirectoryMethodWithFileExistsTest()
        {
            var fileName = "TestSecond_01012022.csv";

            if (!File.Exists(_sourceDirectoryPath + fileName))
            {
                File.CreateText(_sourceDirectoryPath + fileName).Close();
            }

            var fileManager = new FileManager(_sourceDirectoryPath, _fileExtension);

            if (!File.Exists(_targetDirectoryPath + fileName))
            {
                File.CreateText(_targetDirectoryPath + fileName).Close();
            }

            fileManager.MoveFileToAnotherDirectory(_targetDirectoryPath, fileName);

            Assert.IsTrue(File.Exists(_targetDirectoryPath + fileName));

            File.Delete(_targetDirectoryPath + fileName);
            fileManager.Dispose();
        }
    }
}