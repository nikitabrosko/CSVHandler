using System.IO;
using BL.DataSourceParsers.FileParsers;
using BL.DataSourceParsers.FileParsersFactories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLTests.DataSourceParsersTests.FileParsersFactoriesTests
{
    [TestClass]
    public class FileContentParserFactoryTests
    {
        [TestMethod]
        public void FileContentParserFactoryTestCreateInstanceMethod()
        {
            DataAccessForTests.CreateFileIfNotExists();

            var fileContent = File.ReadAllText(DataAccessForTests.PathToFolder + DataAccessForTests.FileName);

            var fileContentParserFactory = new FileContentParserFactory();

            var expectedFileContentParserObject = new FileContentParser(fileContent);
            var actualFileContentParserObject = fileContentParserFactory.CreateInstance(fileContent);

            Assert.AreEqual(expectedFileContentParserObject.ToString(), actualFileContentParserObject.ToString());
        }
    }
}
