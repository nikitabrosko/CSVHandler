using System;
using System.Linq;
using System.Text;
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
        public void FileContentParserReadDateRecordMethodTest()
        {
            var fileContentParser = new FileContentParser(DataAccessForTests.FileText);

            var date = DataAccessForTests.FileText.Split(";")[0];

            var expectedContent = new StringBuilder()
                .Append(date.Substring(0, 2))
                .Append('.')
                .Append(date.Substring(2, 2))
                .Append('.')
                .Append(date.Substring(4, 4))
                .ToString();

            var actualContent = fileContentParser.ReadDateRecord();

            Assert.AreEqual(expectedContent, actualContent);
        }

        [TestMethod]
        public void FileContentParserReadDateRecordMethodRecordIsEmptyTest()
        {
            var fileContentParser = new FileContentParser(";Ivan Sidorov;Telephone, 5;20");

            Assert.ThrowsException<ArgumentException>(() => fileContentParser.ReadDateRecord(),
                "Date record in file content is empty or whitespace");
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
        public void FileContentParserReadCustomerRecordMethodRecordIsEmptyTest()
        {
            var fileContentParser = new FileContentParser("01012022;;Telephone, 5;20");

            Assert.ThrowsException<ArgumentException>(() => fileContentParser.ReadCustomerRecord(),
                "Customer record in file content is empty or whitespace");
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
        public void FileContentParserReadProductRecordMethodRecordIsEmptyTest()
        {
            var fileContentParser = new FileContentParser("01012022;Ivan Sidorov;;20");
            Assert.ThrowsException<ArgumentException>(() => fileContentParser.ReadProductRecord(),
                "Product record in file content is empty or whitespace");
        }

        [TestMethod]
        public void FileContentParserReadSumRecordMethodTest()
        {
            var fileContentParser = new FileContentParser(DataAccessForTests.FileText);

            var expectedContent = DataAccessForTests.FileText.Split(";")[3];
            var actualContent = fileContentParser.ReadSumRecord();

            Assert.AreEqual(expectedContent, actualContent);
        }

        [TestMethod]
        public void FileContentParserReadSumRecordMethodRecordIsEmptyTest()
        {
            var fileContentParser = new FileContentParser("01012022;Ivan Sidorov;Telephone, 5;");

            Assert.ThrowsException<ArgumentException>(() => fileContentParser.ReadSumRecord(),
                "Sum record in file content is empty or whitespace");
        }
    }
}