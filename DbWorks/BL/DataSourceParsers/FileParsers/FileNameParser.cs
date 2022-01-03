using System;
using System.Linq;
using BL.Abstractions;

namespace BL.DataSourceParsers.FileParsers
{
    public class FileNameParser : IFileNameParser
    {
        public string FileName { get; }

        public FileNameParser(string fileName)
        {
            Verify(fileName);

            FileName = fileName;
        }

        public string GetLastName()
        {
            return FileName.TakeWhile(char.IsLetter)
                .Aggregate(string.Empty, (current, character) => current + character);
        }

        public string GetDate()
        {
            return FileName.SkipWhile(c => char.IsLetter(c) || c.Equals('_'))
                .TakeWhile(char.IsDigit)
                .Aggregate(string.Empty, (current, character) => current + character);
        }

        protected virtual void Verify(string fileName)
        {
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (fileName.Equals(string.Empty))
            {
                throw new ArgumentException("File name can not be empty!", nameof(fileName));
            }

            if (!fileName.Contains('_'))
            {
                throw new ArgumentException("File name should contains a '_' symbol!", nameof(fileName));
            }

            var lastNameToValid = fileName.TakeWhile(char.IsLetter)
                .Aggregate(string.Empty, (current, character) => current + character);

            if (char.IsLetter(lastNameToValid.First()))
            {
                throw new ArgumentException("File name first letter can be letter!", nameof(fileName));
            }

            if (!char.IsUpper(lastNameToValid.First()))
            {
                throw new ArgumentException("File name first letter can be in upper case!", nameof(fileName));
            }

            if (!lastNameToValid.Skip(1).All(char.IsLower))
            {
                throw new ArgumentException("File name last name is invalid!", nameof(fileName));
            }

            var dateToValid = fileName.SkipWhile(c => char.IsLetter(c) || c.Equals('_'))
                .TakeWhile(char.IsDigit)
                .Aggregate(string.Empty, (current, character) => current + character);

            if (!dateToValid.Length.Equals(8))
            {
                throw new ArgumentException("File name date length should be equal 8!", nameof(fileName));
            }

            if (dateToValid.Take(1).All(c => (int.Parse(c.ToString()) > 3) || (int.Parse(c.ToString()) < 0)) 
                || dateToValid.Skip(1).Take(1).All(c => int.Parse(c.ToString()) < 0))
            {
                throw new ArgumentException("File name date day is invalid!", nameof(fileName));
            }

            if (dateToValid.Skip(2).Take(1).All(c => (int.Parse(c.ToString()) is not 1 or 0)) 
                || dateToValid.Skip(3).Take(1).All(c => int.Parse(c.ToString()) < 0))
            {
                throw new ArgumentException("File name date month is invalid!", nameof(fileName));
            }
        }
    }
}
