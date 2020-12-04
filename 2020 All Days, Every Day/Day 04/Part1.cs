using System;
using System.IO;
using System.Collections.Generic;
using Serilog;
using Advent;
using System.Text.RegularExpressions;

namespace Day_04
{
    //Problem URL
    internal class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Something Something. Part One."; }

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

            foreach (var passport in passPorts)
            {
                totalCount++;
                if (passport.Count == 8)
                {
                    validPassports++;
                    continue;
                }

                if (passport.Count == 7 && !passport.ContainsKey("cid"))
                {
                    validPassports++;
                    continue;
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