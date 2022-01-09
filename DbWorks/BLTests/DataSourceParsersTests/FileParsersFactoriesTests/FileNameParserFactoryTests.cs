using BL.DataSourceParsers.FileParsers;
using BL.DataSourceParsers.FileParsersFactories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLTests.DataSourceParsersTests.FileParsersFactoriesTests
{
    [TestClass]
    public class FileNameParserFactoryTests
    {
        [TestMethod]
        public void FileNameParserFactoryTestCreateInstanceMethod()
        {
            DataAccessForTests.CreateFileIfNotExists();

            var fileNameParserFactory = new FileNameParserFactory();

            var expectedFileNameParserObject = new FileNameParser(DataAccessForTests.FileName);
            var actualFileNameParserObject = fileNameParserFactory.CreateInstance(DataAccessForTests.FileName);

            Assert.AreEqual(expectedFileNameParserObject.ToString(), actualFileNameParserObject.ToString());
        }
    }
}