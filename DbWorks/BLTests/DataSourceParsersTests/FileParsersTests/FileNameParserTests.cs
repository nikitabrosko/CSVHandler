using System;
using System.Linq;
using System.Text;
using BL.DataSourceParsers.FileParsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLTests.DataSourceParsersTests.FileParsersTests
{
    [TestClass]
    public class FileNameParserTests
    {
        [TestMethod]
        public void FileNameParserCreatingWithInvalidParametersFileNameIsNullTest()
        {
            string fileName = null;

            Assert.ThrowsException<ArgumentNullException>(() => new FileNameParser(fileName));
        }

        [TestMethod]
        public void FileNameParserGetLastNameMethodTest()
        {
            var fileNameParser = new FileNameParser(DataAccessForTests.FileName);

            var expectedLastName = DataAccessForTests.FileName.Split("_").First();
            var actualLastName = fileNameParser.GetLastName();

            Assert.AreEqual(expectedLastName, actualLastName);
        }

        [TestMethod]
        public void FileNameParserGetDateMethodTest()
        {
            var fileNameParser = new FileNameParser(DataAccessForTests.FileName);

            var date = DataAccessForTests.FileName
                .Split("_").Last()
                .Split(".").First();

            var expectedDate = new StringBuilder()
                .Append(date.Substring(0, 2))
                .Append('.')
                .Append(date.Substring(2, 2))
                .Append('.')
                .Append(date.Substring(4, 4))
                .ToString();

            var actualDate = fileNameParser.GetDate();
            
            Assert.AreEqual(expectedDate, actualDate);
        }

        [TestMethod]
        public void FileNameParserCreatingWithInvalidParametersFileNameIsEmptyTest()
        {
            var fileName = string.Empty;

            Assert.ThrowsException<ArgumentException>(() => new FileNameParser(fileName),
                "File name can not be empty!");
        }

        [TestMethod]
        public void FileNameParserCreatingWithInvalidParametersFileNameIsInvalidTest()
        {
            var fileName = "Test01012022";

            Assert.ThrowsException<ArgumentException>(() => new FileNameParser(fileName),
                "File name should contains a '_' symbol!");
        }

        [TestMethod]
        public void FileNameParserCreatingWithInvalidParametersFileNameLastNameFirstCharacterIsNotLetterTest()
        {
            var fileName = "1est_01012022";

            Assert.ThrowsException<ArgumentException>(() => new FileNameParser(fileName),
                "File name first letter can be letter!");
        }

        [TestMethod]
        public void FileNameParserCreatingWithInvalidParametersFileNameLastNameFirstCharacterIsInLowerCaseTest()
        {
            var fileName = "test_01012022";

            Assert.ThrowsException<ArgumentException>(() => new FileNameParser(fileName),
                "File name first letter can be in upper case!");
        }

        [TestMethod]
        public void FileNameParserCreatingWithInvalidParametersFileNameLastCharactersIsInUpperCaseNameTest()
        {
            var fileName = "TEST_01012022";

            Assert.ThrowsException<ArgumentException>(() => new FileNameParser(fileName),
                "File name last name is invalid!");
        }

        [TestMethod]
        public void FileNameParserCreatingWithInvalidParametersFileNameDateLengthIsInvalidTest()
        {
            var fileName = "Test_010120221";

            Assert.ThrowsException<ArgumentException>(() => new FileNameParser(fileName),
                "File name date length should be equal 8!");
        }

        [TestMethod]
        public void FileNameParserCreatingWithInvalidParametersFileNameDateDayIsInvalidTest()
        {
            var fileName = "Test_32012022";

            Assert.ThrowsException<ArgumentException>(() => new FileNameParser(fileName),
                "File name date day is invalid!");
        }

        [TestMethod]
        public void FileNameParserCreatingWithInvalidParametersFileNameDateMonthIsInvalidTest()
        {
            var fileName = "Test_01132022";

            Assert.ThrowsException<ArgumentException>(() => new FileNameParser(fileName),
                "File name date month is invalid!");
        }
    }
}