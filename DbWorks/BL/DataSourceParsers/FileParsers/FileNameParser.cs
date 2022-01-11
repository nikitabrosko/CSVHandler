using System;
using System.Linq;
using System.Text;
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
            var name = FileName.Split("_").First();

            return name;
        }

        public string GetDate()
        {
            var date = FileName.SkipWhile(c => char.IsLetter(c) || c.Equals('_'))
                .TakeWhile(char.IsDigit)
                .Aggregate(string.Empty, (current, character) => current + character);
            
            var newDate = new StringBuilder();

            newDate.Append(date.Substring(0, 2))
                .Append('.')
                .Append(date.Substring(2, 2))
                .Append('.')
                .Append(date.Substring(4, 4));

            return newDate.ToString();
        }

        private static void Verify(string fileName)
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

            var lastNameToValid = fileName.Split("_").First();

            if (!char.IsLetter(lastNameToValid.First()))
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

            var firstDayExpression = dateToValid
                .Take(1)
                .All(c => (int.Parse(c.ToString()) > 3) || (int.Parse(c.ToString()) < 0));
            var secondDayExpression = dateToValid
                .Skip(1)
                .Take(1)
                .All(c => int.Parse(c.ToString()) < 0);
            var thirdDayExpression = dateToValid.Take(1).All(c => (int.Parse(c.ToString()) == 3))
                                     && dateToValid.Skip(1).Take(1).All(c => int.Parse(c.ToString()) > 1);

            if (firstDayExpression || secondDayExpression || thirdDayExpression)
            {
                throw new ArgumentException("File name date day is invalid!", nameof(fileName));
            }

            var firstDateExpression = dateToValid
                .Skip(2)
                .Take(1)
                .All(c => int.Parse(c.ToString()) > 1 || int.Parse(c.ToString()) < 0);
            var secondDateExpression = dateToValid
                .Skip(3)
                .Take(1)
                .All(c => int.Parse(c.ToString()) < 0);
            var thirdDateExpression = dateToValid.Skip(2).Take(1).All(c => int.Parse(c.ToString()) == 1)
                                      && dateToValid.Skip(3).Take(1).All(c => int.Parse(c.ToString()) > 1);

            if (firstDayExpression || secondDayExpression || thirdDateExpression)
            {
                throw new ArgumentException("File name date month is invalid!", nameof(fileName));
            }
        }
    }
}
