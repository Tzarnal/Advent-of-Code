using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Advent;
using RegExtract;

namespace Day_10
{
    //https://adventofcode.com/2015/day/10
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Elves Look, Elves Say. Part One."; }

        public void Run()
        {
            Solve("1", 5);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input, 40);
        }

        public static void Solve(string input, int iterations)
        {
            var workingInput = input;
            for (int i = 0; i < iterations; i++)
            {
                var runs = CountRuns(workingInput);

                var sb = new StringBuilder();

                foreach (var (character, count) in runs)
                {
                    sb.Append(count);
                    sb.Append(character);
                }

                workingInput = sb.ToString();
            }

            Log.Information("The length of the final result is {l}", workingInput.Length);
        }

        public static List<(char character, int count)> CountRuns(string input)
        {
            var output = new List<(char character, int count)>();

            var currentChar = input[0];
            var runLength = 1;

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == currentChar)
                {
                    runLength++;
                }
                else
                {
                    output.Add((currentChar, runLength));
                    currentChar = input[i];
                    runLength = 1;
                }
            }

            output.Add((currentChar, runLength));

            return output;
        }

        public static string ParseInput(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}