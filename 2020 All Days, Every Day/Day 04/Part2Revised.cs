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
        public string ProblemName { get => $"Day {Dayname}: Passport Processing. Part Two (Revised)."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinputList);

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

            var entries = data.Trim().Split("\r\n\r\n");

            foreach (var entry in entries)
            {
                var passport = new Dictionary<string, string>();

                var pairs = entry.Replace("\r\n", " ").Split(' ');

                foreach (var pair in pairs)
                {
                    var splitPair = pair.Split(":");
                    passport.Add(splitPair[0], splitPair[1]);
                }
                passPorts.Add(passport);
            }

            return passPorts;
        }
    }
}