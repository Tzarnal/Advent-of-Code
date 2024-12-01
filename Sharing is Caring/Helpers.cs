using System.IO;
using System.Collections.Generic;
using Serilog;
using Advent.Framework;
using System;
using System.Text.RegularExpressions;
using System.Linq;
using Serilog.Formatting.Display;

namespace Advent
{
    public static class Helpers
    {
        public static List<int> ReadNumbersFile(string filePath)
        {
            var inputFile = File.ReadAllLines(filePath);
            var inputList = new List<int>();

            foreach (var line in inputFile)
            {
                int c;
                if (int.TryParse(line, out c))
                {
                    inputList.Add(c);
                }
                else
                {
                    Log.Warning("Conversion Error: {c}", c);
                }
            }

            return inputList;
        }

        public static List<int> ReadAllIntsStrings(string input)
        {
            var intsResult = Regex.Matches(input, @"(-?\d+)");

            var ints = new List<int>();

            foreach (Match result in intsResult)
            {
                ints.Add(int.Parse(result.Value));
            }

            return ints;
        }

        public static List<List<int>> ReadAllIntsPerLine(string filePath)
        {
            var intsPerLine = new List<List<int>>();

            var inputFile = File.ReadAllLines(filePath);
            foreach (var line in inputFile)
            {
                var ints = new List<int>();
                var intsResult = Regex.Matches(line, @"(-?\d+)");
                foreach (Match result in intsResult)
                {
                    int i = 0;
                    if (int.TryParse((string)result.Value, out i))
                    {
                        ints.Add(i);
                    }
                }

                intsPerLine.Add(ints);
            }

            return intsPerLine;
        }

        public static List<int> ReadAllIntsFile(string filePath)
        {
            var file = File.ReadAllText(filePath);
            return ReadAllIntsStrings(file);
        }

        public static List<string> ReadStringsFile(string filePath)
        {
            var inputFile = File.ReadAllLines(filePath);

            return new List<string>(inputFile);
        }

        public static List<List<string>> ReadAllRecords(string filePath)
        {
            var inputFile = File.ReadAllText(filePath);

            var chunks = inputFile.Split($"{Environment.NewLine}{Environment.NewLine}");

            var records = new List<List<string>>();
            foreach (var chunk in chunks)
            {
                var record = new List<string>();
                foreach (var line in chunk.Split(Environment.NewLine))
                {
                    record.Add(line);
                }
                records.Add(record);
            }
            return records;
        }

        public static List<List<int>> ReadAllRecordsInt(string filePath)
        {
            var inputFile = File.ReadAllText(filePath);

            var chunks = inputFile.Split($"{Environment.NewLine}{Environment.NewLine}");

            var records = new List<List<int>>();
            foreach (var chunk in chunks)
            {
                var record = new List<int>();
                foreach (var line in chunk.Split(Environment.NewLine))
                {
                    record.Add(int.Parse(line));
                }
                records.Add(record);
            }
            return records;
        }

        public static IEnumerable<int[]> PartitionIntoPossibleParts(int Total, int Parts)
        {
            if (Parts == 1)
            {
                yield return new int[] { Total };
                yield break;
            }

            for (var i = 0; i <= Total; i++)
            {
                foreach (var Remainder in PartitionIntoPossibleParts(Total - i, Parts - 1))
                {
                    yield return Remainder.Select(x => x).Append(i).ToArray();
                }
            }
        }

        public static string ClearGridString(string gridString)
        {
            gridString = gridString.Replace(".", "░");
            gridString = gridString.Replace("#", "▓");

            return gridString;
        }

        public static string GetDayFromNamespace(object o)
        {
            var nameSpace = o.GetType().Namespace;

            var day = nameSpace.Split('_')[1].Trim();

            return day;
        }

        public static string StringEnumerableToString(List<string> input)
        {
            string output = "";
            foreach (var item in input)
            {
                output += item;
            }
            return output;
        }

        public static bool IsLetters(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool HasLetters(string s)
        {
            foreach (char c in s)
            {
                if (char.IsLetter(c))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsDigits(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool HasDigits(string s)
        {
            foreach (char c in s)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsSymbols(string s)
        {
            foreach (char c in s)
            {
                if (char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool HasSymbols(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return true;
                }
            }

            return false;
        }
    }
}