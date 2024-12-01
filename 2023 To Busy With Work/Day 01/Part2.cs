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
    //https://adventofcode.com/2022/day/1#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Trebuchet. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<string> input)
        {
            var calibrationSum = 0;

            var regex = new Regex("\\D+");
            foreach (var line in input)
            {
                var improvedLine = SubstituteDigits(line);
                var digitsOnly = regex.Replace(improvedLine, "");

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

            Log.Information("Found {count} digits, after substitutions total calibration value sum is, {calibrationSum}",
                input.Count,
                calibrationSum);
        }

        private Dictionary<string, string> DigitsMap = new Dictionary<string, string>
        {
            ["nine"] = "9",
            ["eight"] = "8",
            ["seven"] = "7",
            ["six"] = "6",
            ["five"] = "5",
            ["four"] = "4",
            ["three"] = "3",
            ["two"] = "2",
            ["one"] = "1",
        };

        public string SubstituteDigits(string input)
        {
            foreach (var sub in DigitsMap)
            {
                input = input.Replace(sub.Key, $"{sub.Key}{sub.Value}{sub.Key}");
            }

            return input;
        }
    }
}