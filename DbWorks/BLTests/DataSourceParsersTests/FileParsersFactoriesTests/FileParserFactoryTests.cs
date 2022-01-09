using BL.DataSourceParsers.FileParsers;
using BL.DataSourceParsers.FileParsersFactories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLTests.DataSourceParsersTests.FileParsersFactoriesTests
{
    [TestClass]
    public class FileParserFactoryTests
    {
        [TestMethod]
        public void FileParserFactoryTestCreateInstanceMethod()
        {
            DataAccessForTests.CreateFileIfNotExists();

            var fileParserFactory = new FileParserFactory();

            var expectedFileParserObject =
                new FileParser(DataAccessForTests.PathToFolder + DataAccessForTests.FileName);
            var actualFileParserObject =
                fileParserFactory.CreateInstance(DataAccessForTests.PathToFolder + DataAccessForTests.FileName);

            Assert.AreEqual(expectedFileParserObject.ToString(), actualFileParserObject.ToString());
        }
    }
}