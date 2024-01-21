using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Text;

using static Trade_Test.Models.Constants.Constants;


namespace Trade_Test.Utilities.Extensions
{
    public static class StringExtensions
    {
        private static readonly Random random = new();


        public static int UniqueRandomInt(int min, int max)
        {
            var rand = new Random();
            var randomList = new List<int>();
            int myNumber;

            do
            {
                myNumber = rand.Next(min, max);
            } while (randomList.Contains(myNumber));

            return myNumber;
        }


        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var result = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return result;
        }


        public static string Trimmed(this string text) => text.ToLower().Trim();

        public static string GetQueryType(this string commandText)
        {
            var onelineString = commandText.Replace("\r\n", " ");
            var result = string.Empty;
            var sqlTexts = onelineString.Split(' ');
            var sqlTypes = new List<string> {
                SqlType.SELECT.AsString(),
                SqlType.INSERT.AsString(),
                SqlType.UPDATE.AsString(),
                SqlType.DELETE.AsString(),
                SqlType.EXEC.AsString()
            };

            foreach (var text in sqlTexts)
            {
                if (sqlTypes.Contains(text.ToLower()))
                {
                    result += $"{text.ToUpper()},";
                }
            }

            var uniq = string.Join(" ", result.Split(',').Distinct());
            return uniq;
        }


        public static string GetTableName(this string commandText)
        {
            var result = string.Empty;
            var sqlTexts = commandText.Split(' ');
            var tablePrefix = "tbl";

            foreach (var text in sqlTexts)
            {
                if (text.ToLower().Contains(tablePrefix))
                {
                    result += $"{text.ToUpper()},";
                }
            }

            var uniq = string.Join(" ", result.Split(',').Distinct());
            return uniq;
        }


        public static string GetQueryParametersAsCommaSeparatedString(this DbParameterCollection parameters)
        {
            var result = string.Empty;

            if (parameters != null && parameters.Count > 0)
            {
                var parameterArr = new DbParameter[parameters.Count];
                parameters.CopyTo(parameterArr, 0);
                result = string.Join<DbParameter>(',', parameterArr);
            }

            return result;
        }


        public static string AsTruthyWord(this bool booleanToConvert)
        {
            return booleanToConvert ? "Yes" : "No";
        }


        public static DateTime AsDateTime(this DateTime? dateTimeObject)
        {
            return dateTimeObject != null ? dateTimeObject.Value : DateTime.UtcNow;
        }


        public static string AsDateString(this DateTime dateTimeObject, string format)
        {
            return dateTimeObject.ToString(format);
        }


        public static string AsString(this SqlType sqlType)
        {
            return sqlType.ToString().ToLower();
        }


        public static string AsBase64(this object value)
        {
            var result = string.Empty;

            if (value != null)
            {
                var valueStr = value.ToString();

                if (!string.IsNullOrEmpty(valueStr))
                    result = Convert.ToBase64String(Encoding.ASCII.GetBytes(valueStr));
            }

            return result;
        }


        public static string FromBase64(this object value)
        {
            var result = string.Empty;

            if (value != null)
            {
                var valueStr = value.ToString();

                if (!string.IsNullOrEmpty(valueStr))
                    result = Encoding.ASCII.GetString(Convert.FromBase64String(valueStr));
            }

            return result;
        }


        public static string ConvertToWord(this string value) => !string.IsNullOrEmpty(value) ?
                value = new string(value.ToLower().Where(x => char.IsLetter(x)).ToArray())
                : string.Empty;


        public static Guid NewGuid(this string value) => string.IsNullOrEmpty(value) ? Guid.NewGuid() : value.ParseGuid();


        public static Guid ParseGuid(this string id)
        {
            Guid.TryParse(id, out Guid guidId);

            return guidId;
        }


        public static Guid ParseNullableGuid(this Guid? id) => id.ParseString().ParseGuid();


        public static string GenerateGuid() => Guid.NewGuid().ToString();


        public static string ParseString(this Guid id) => Convert.ToString(id);


        public static string ParseDateString(this DateTimeOffset value) => value.ToString("dd MMM yyyy");


        public static string ParseString(this Guid? id) => id != null ? Convert.ToString(id) : string.Empty;


        public static string ParseString(this object value) => value != null ? value.ParseString() : string.Empty;


        public static string ParseString(this string value) => !string.IsNullOrEmpty(value) ? value : string.Empty;


        public static int ParseInt(this int? value) => value != null ? (int)value : 0;


        public static string GetName(this string valueToCompare, bool returnLastValue = false)
        {
            if (valueToCompare.Contains(' '))
            {
                var splittedName = valueToCompare.Split(new[] { ' ' }, 2);

                if (splittedName.Length > 0)
                {
                    if (!string.IsNullOrEmpty(splittedName[0]))
                    {
                        if (returnLastValue)
                        {
                            return new string(splittedName[1]);
                        }
                        else
                        {
                            return new string(splittedName[0]);
                        }
                    }
                }
            }
            else
            {
                if (returnLastValue) return string.Empty;
            }

            return valueToCompare;
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            var sb = new StringBuilder();

            foreach (var c in str.Where(c => (c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c == ' ')))
            {
                sb.Append(c);
            }

            return sb.ToString();
        }


        /// <summary>
        /// Returns a new string in which all occurrences of a specified string in the current instance are replaced with another 
        /// specified string according the type of search to use for the specified string.
        /// </summary>
        /// <param name="str">The string performing the replace method.</param>
        /// <param name="oldValue">The string to be replaced.</param>
        /// <param name="newValue">The string replace all occurrences of <paramref name="oldValue"/>. 
        /// If value is equal to <c>null</c>, than all occurrences of <paramref name="oldValue"/> will be removed from the <paramref name="str"/>.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns>A string that is equivalent to the current string except that all instances of <paramref name="oldValue"/> are replaced with <paramref name="newValue"/>. 
        /// If <paramref name="oldValue"/> is not found in the current instance, the method returns the current instance unchanged.</returns>
        [DebuggerStepThrough]
        public static string ReplaceCaseInsensitive(this string str,
            string oldValue, string @newValue,
            StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {

            // Check inputs.
            if (str == null)
            {
                // Same as original .NET C# string.Replace behavior.
                throw new ArgumentNullException(nameof(str));
            }

            if (str.Length == 0)
            {
                // Same as original .NET C# string.Replace behavior.
                return str;
            }

            if (oldValue == null)
            {
                // Same as original .NET C# string.Replace behavior.
                throw new ArgumentNullException(nameof(oldValue));
            }

            if (oldValue.Length == 0)
            {
                // Same as original .NET C# string.Replace behavior.
                throw new ArgumentException("String cannot be of zero length.");
            }

            // Prepare string builder for storing the processed string.
            // Note: StringBuilder has a better performance than String by 30-40%.
            var resultStringBuilder = new StringBuilder(str.Length);

            // Analyze the replacement: replace or remove.
            bool isReplacementNullOrEmpty = string.IsNullOrEmpty(@newValue);

            // Replace all values.
            const int valueNotFound = -1;
            int foundAt;
            int startSearchFromIndex = 0;

            while ((foundAt = str.IndexOf(oldValue, startSearchFromIndex, comparisonType)) != valueNotFound)
            {
                // Append all characters until the found replacement.
                int @charsUntilReplacment = foundAt - startSearchFromIndex;
                bool isNothingToAppend = @charsUntilReplacment == 0;

                if (!isNothingToAppend)
                {
                    resultStringBuilder.Append(str, startSearchFromIndex, @charsUntilReplacment);
                }

                // Process the replacement.
                if (!isReplacementNullOrEmpty)
                {
                    resultStringBuilder.Append(@newValue);
                }

                // Prepare start index for the next search.
                // This needed to prevent infinite loop, otherwise method always start search 
                // from the start of the string. For example: if an oldValue == "EXAMPLE", newValue == "example"
                // and comparisonType == "any ignore case" will conquer to replacing:
                // "EXAMPLE" to "example" to "example" to "example" … infinite loop.
                startSearchFromIndex = foundAt + oldValue.Length;

                if (startSearchFromIndex == str.Length)
                {
                    // It is end of the input string: no more space for the next search.
                    // The input string ends with a value that has already been replaced. 
                    // Therefore, the string builder with the result is complete and no further action is required.
                    return resultStringBuilder.ToString();
                }
            }

            // Append the last part to the result.
            int @charsUntilStringEnd = str.Length - startSearchFromIndex;
            resultStringBuilder.Append(str, startSearchFromIndex, @charsUntilStringEnd);

            return resultStringBuilder.ToString();
        }


        public static string NumberToWords(this int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += $"{NumberToWords(number / 1000000)} million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += $"{NumberToWords(number / 1000)} thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += $"{NumberToWords(number / 100)} hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                {
                    words += unitsMap[number];
                }
                else
                {
                    words += tensMap[number / 10];

                    if ((number % 10) > 0)
                    {
                        words += $"-{unitsMap[number % 10]}";
                    }
                }
            }

            return words;
        }

        public static string GetAbbreviatedName(this int month)
        {
            DateTime date = new DateTime(2020, month, 1);

            return date.ToString("MMM");
        }

        // function to get the full month name
        public static string GetFullName(this int month)
        {
            DateTime date = new DateTime(2020, month, 1);

            return date.ToString("MMMM");
        }
    }
}
