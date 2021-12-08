using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_08
{
    //https://adventofcode.com/2021/day/08
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Seven Segment Search. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(List<string> Digits, List<string> Output)> input)
        {
            var counts = 0;

            foreach (var display in input)
            {
                foreach (var digit in display.Output)
                {
                    if (digit.Count() is 2 or 3 or 4 or 7)
                    {
                        counts++;
                    }
                }
            }

            Log.Information("The digits 1, 4, 7, or 8 appear {counts} times in the output.",
                counts);
        }

        public static List<(List<string> Digits, List<string> Output)> ParseInput(string filePath)
        {
            var displayValues = new List<(List<string> Digits, List<string> Output)>();

            foreach (var line in Helpers.ReadStringsFile(filePath))
            {
                var values = line.Split(" ");

                //Alphebatize the signal patterns
                var digits = values[..10].ToList().Select(v => string.Concat(v.OrderBy(c => c))).ToList();
                var output = values[^4..].ToList().Select(v => string.Concat(v.OrderBy(c => c))).ToList();

                displayValues.Add((digits, output));
            }

            return displayValues;
        }
    }
}