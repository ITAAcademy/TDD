using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TDD.SimpleCalculator
{
    public class SimpleCalculator
    {
        private string _delimitersPrefix = "//";

        private bool CanAddNumbers(string numbers)
        {
            return !string.IsNullOrEmpty(numbers);
        }

        public int Add(string numbers)
        {
            if (!CanAddNumbers(numbers))
            {
                return 0;
            }
            
            var providedStringType = CalculatorIsOfType(numbers);
            List<string> delimiters = ParseDelimiters(providedStringType, numbers);
            var parsedNumbers = ParseNumbers(numbers, delimiters, providedStringType);
            
            return Add(parsedNumbers);
        }

        private int Add(string[] parsedNumbers)
        {
            var sum = 0;
            var negativeNumbers = new List<int>();

            foreach (var parsedNumber in parsedNumbers)
            {
                var numberToAdd = int.Parse(parsedNumber.Trim());
                if (numberToAdd < 0)
                {
                    negativeNumbers.Add(numberToAdd);
                }
                else if (numberToAdd <= 1000)
                {
                    sum += numberToAdd;
                }
            }

            if (negativeNumbers.Count > 0)
            {
                throw new Exception($"Negatives are not allowed: {string.Join(", ", negativeNumbers)}");
            }

            return sum;
        }

        private string[] ParseNumbers(string numbersAndDelimiters, List<string> delimiters, StringType providedStringType)
        {
            var numbersString = providedStringType != StringType.NoDelimitersString ? numbersAndDelimiters.Substring(numbersAndDelimiters.IndexOf("\n") + 1) : numbersAndDelimiters;
            return numbersString.Split(delimiters.ToArray(), StringSplitOptions.None);
        }

        private List<string> ParseDelimiters(StringType providedStringType, string numbersAndDelimiters)
        {
            switch (providedStringType)
            {
                case StringType.ComplexDelimiter:
                    return ParseComplexDelimiters(numbersAndDelimiters);
                case StringType.OneCharDelimiter:
                    return ParseOneCharDelimiter(numbersAndDelimiters);
                case StringType.NoDelimitersString:
                    return DefaultDelimiters();
                default:
                    return new List<string>();
            }
        }

        private List<string> ParseComplexDelimiters(string numbersAndDelimiters)
        {
            var delimitersList = new List<string>();
            var r = new Regex(@"\[([^]]*)\]");
            Match m = r.Match(numbersAndDelimiters);
            while (m.Success)
            {
                delimitersList.Add(m.Groups[1].Value);
                m = m.NextMatch();
            }

            return delimitersList;
        }

        private List<string> ParseOneCharDelimiter(string numbersAndDelimiters)
        {
            int delimiterIndex = numbersAndDelimiters.IndexOf(_delimitersPrefix) + 2;
            return new List<string>
            {
                numbersAndDelimiters.Substring(delimiterIndex, numbersAndDelimiters.IndexOf("\n") - delimiterIndex)
            };
        }

        private List<string> DefaultDelimiters()
        {
            return new List<string>() { ",", "\n" };
        }

        private StringType CalculatorIsOfType(string numbers)
        {
            var providedStringType = StringType.NoDelimitersString;
            if (numbers.StartsWith(_delimitersPrefix))
            {
                providedStringType = IsDelimetersListInBrackets(numbers) ? StringType.ComplexDelimiter : StringType.OneCharDelimiter;
            }
            return providedStringType;
        }

        private bool IsDelimetersListInBrackets(string delimetersAndNumbers)
        {
            var r = new Regex(@"^(\/\/)(\[([^]]*)\])*\n.+");
            return r.IsMatch(delimetersAndNumbers);
        }

        private enum StringType
        {
            NoDelimitersString,
            OneCharDelimiter,
            ComplexDelimiter
        }
    }
}
