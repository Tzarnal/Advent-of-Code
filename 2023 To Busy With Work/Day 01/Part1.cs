using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Text.RegularExpressions;

namespace Day_01
{
    //https://adventofcode.com/2022/day/1
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Trebuchet."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<string> input)
        {
            var calibrationSum = 0;

            var regex = new Regex("\\D+");
            foreach (var line in input)
            {
                var digitsOnly = regex.Replace(line, "");

                var newDigit = $"{digitsOnly[0]}{digitsOnly[digitsOnly.Count() - 1]}";

                var calibrationDigit = 0;
                if (int.TryParse(newDigit, out calibrationDigit))
                {
                    calibrationSum += calibrationDigit;
                }
                else
                {
                    Log.Error("Could not parse Digit: {0}", newDigit);
                }
            }

            Log.Information("Found {count} digits, total calibration value sum is, {calibrationSum}",
                input.Count,
                calibrationSum);
        }

        public static List<string> ParseInput(string filePath)
        {
            return File.ReadLines(filePath).ToList();
        }
    }
}