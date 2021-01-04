using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Text.RegularExpressions;

namespace Day_08
{
    //https://adventofcode.com/2015/day/8#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Matchsticks. Part Two."; }

        public void Run()
        {
            //var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<string> input)
        {
            var totalOriginalCharacters = input.Select(i => i.Length).Sum();

            var totalMemoryCharacters = 0;

            foreach (var line in input)
            {
                var memLine = line.Replace("\\", "\\\\"); //Escaped backslash(\\)
                memLine = memLine.Replace("\"", "\\\""); //Escaped string literal (\")
                memLine = $"\"{memLine}\"";//Starting and Ending "

                totalMemoryCharacters += memLine.Length;
            }

            Log.Information("Total original length: {orig}, memory length: {mem}. Awnser: {awnser}",
                totalOriginalCharacters, totalMemoryCharacters, totalMemoryCharacters - totalOriginalCharacters);
        }

        private List<string> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath);
        }
    }
}