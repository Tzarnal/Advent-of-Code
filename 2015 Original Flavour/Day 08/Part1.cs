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
    //https://adventofcode.com/2015/day/8
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Matchsticks. Part One."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<string> input)
        {
            var totalOriginalCharacters = input.Select(i => i.Length).Sum();

            List<int> memoryCharacterLengths = new();

            foreach (var line in input)
            {
                var memLine = line.Trim('\"');  //Starting and Ending "
                memLine = memLine.Replace("\\\"", "\""); //Escaped string literal (\")
                memLine = memLine.Replace("\\\\", "\\"); //Escaped backslash(\\)
                memLine = ReplaceHexEscaped(memLine); //Escaped hex ascii (\x27)

                memoryCharacterLengths.Add(memLine.Length);
            }

            var totalMemoryCharacters = memoryCharacterLengths.Sum();

            Log.Information("Total original length: {orig}, memory length: {mem}. Awnser: {awnser}",
                totalOriginalCharacters, totalMemoryCharacters, totalOriginalCharacters - totalMemoryCharacters);
        }

        public static string ReplaceHexEscaped(string Input)
        {
            if (!Input.Contains("\\x"))
            {
                return Input;
            }

            var hexCodeMatches = Regex.Matches(Input, @"(?:\\x(..))");

            foreach (Match hexCodeMatch in hexCodeMatches)
            {
                string asciiValue;

                try
                {
                    asciiValue = Convert.ToChar(Convert.ToInt32(hexCodeMatch.Groups[1].Value, 16)).ToString();
                }
                catch
                {
                    continue;
                }

                Input = Input.Replace($"\\x{hexCodeMatch.Groups[1].Value}", asciiValue);
            }

            return Input;
        }

        private List<string> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath);
        }
    }
}