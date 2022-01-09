using System;
using System.Linq;
using BL.DataSourceParsers.FileParsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLTests.DataSourceParsersTests.FileParsersTests
{
    [TestClass]
    public class FileContentParserTests
    {
        [TestMethod]
        public void FileContentParserCreatingWithInvalidParametersTest()
        {
            string fileText = null;

            Assert.ThrowsException<ArgumentNullException>(() => new FileContentParser(fileText));
        }

        [TestMethod]
        public void FileContentParserReadDataRecordMethodTest()
        {
            var fileContentParser = new FileContentParser(DataAccessForTests.FileText);

            var expectedContent = DataAccessForTests.FileText.Split(";")[0];
            var actualContent = fileContentParser.ReadDataRecord();

            Assert.AreEqual(expectedContent, actualContent);
        }

        [TestMethod]
        public void FileContentParserReadCustomerRecordMethodTest()
        {
            var fileContentParser = new FileContentParser(DataAccessForTests.FileText);

            var expectedContent = DataAccessForTests.FileText.Split(";")[1].Split(" ");
            var actualContent = fileContentParser.ReadCustomerRecord();

            Assert.IsTrue(expectedContent.SequenceEqual(actualContent));
        }

        [TestMethod]
        public void FileContentParserReadProductRecordMethodTest()
        {
            var fileContentParser = new FileContentParser(DataAccessForTests.FileText);

            var expectedContent = DataAccessForTests.FileText.Split(";")[2].Split(", ");
            var actualContent = fileContentParser.ReadProductRecord();

            Assert.IsTrue(expectedContent.SequenceEqual(actualContent));
        }

        [TestMethod]
        public void FileContentParserReadSumRecordMethodTest()
        {
            var fileContentParser = new FileContentParser(DataAccessForTests.FileText);

            var expectedContent = DataAccessForTests.FileText.Split(";")[3];
            var actualContent = fileContentParser.ReadSumRecord();

            Assert.AreEqual(expectedContent, actualContent);
        }
    }
}