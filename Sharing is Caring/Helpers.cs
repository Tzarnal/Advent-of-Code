using System.IO;
using System.Collections.Generic;
using Serilog;
using Advent.Framework;
using System;
using System.Text.RegularExpressions;
using System.Linq;

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

        public static string GetDayFromNamespace(object o)
        {
            var nameSpace = o.GetType().Namespace;

            var day = nameSpace.Split('_')[1].Trim();

            return day;
        }

        public static void ProblemRunner(IAdventProblem problem)
        {
            Console.WriteLine();
            Log.Information("Running {ProblemName}", problem.ProblemName);
            Log.Logger = CLI.IndentLogger;
            problem.Run();

            Log.Logger = CLI.DefaultLogger;
        }
    }
}