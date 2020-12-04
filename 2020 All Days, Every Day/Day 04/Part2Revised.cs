using Advent;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day_04
{
    //https://adventofcode.com/2020/day/4#part2
    public class Part2Revised : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Passport Processing. Part Two."; }

        public void Run()
        {
            //var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<Dictionary<string, string>> passPorts)
        {
            var validPassports = 0;
            var totalCount = 0;

            foreach (var passportData in passPorts)
            {
                var passPort = new Passport(passportData);

                totalCount++;
                if (passPort.Valid() || passPort.ValidWithoutCid())
                {
                    validPassports++;
                }
            }

            Log.Information("Counted {totalCount} passports. Found {validPassports} valid passports.", totalCount, validPassports);
        }

        private List<Dictionary<string, string>> ParseInput(string filePath)
        {
            var data = File.ReadAllText(filePath);
            var passPorts = new List<Dictionary<string, string>>();

            var splitData = data.Split(Environment.NewLine);

            //var splitRegex = @".+\n\n";
            //var splitResult = Regex.Match(data, splitRegex, RegexOptions.Singleline);

            var pairRegex = @"((\w+):([#\w]+))";

            var passPort = new Dictionary<string, string>();
            foreach (var line in splitData)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    passPorts.Add(passPort);
                    passPort = new Dictionary<string, string>();
                    continue;
                }

                var pairMatches = Regex.Matches(line, pairRegex);
                foreach (Match match in pairMatches)
                {
                    passPort.Add(match.Groups[2].Value, match.Groups[3].Value);
                }
            }

            //Add final one
            passPorts.Add(passPort);
            return passPorts;
        }
    }
}