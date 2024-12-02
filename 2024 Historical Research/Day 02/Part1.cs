using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_02
{
    //https://adventofcode.com/2024/day/02
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Red-Nosed Reports. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<List<int>> input)
        {
            var safeReports = new List<List<int>>();

            foreach (var line in input)
            {
                if (!safeDiff(line))
                {
                    continue;
                }

                if (allHigher(line) || allLower(line))
                {
                    safeReports.Add(line);
                }
            }

            Log.Information("Found {count} safe reports", safeReports.Count());
        }

        public bool safeDiff(List<int> input)
        {
            var last = input.FirstOrDefault();
            foreach (var number in input.Skip(1))
            {
                var diff = Math.Abs(last - number);
                if (diff < 1 || diff > 3)
                {
                    return false;
                }

                last = number;
            }

            return true;
        }

        public bool allHigher(List<int> input)
        {
            var last = input.FirstOrDefault();
            foreach (var number in input.Skip(1))
            {
                if (number <= last)
                {
                    return false;
                }

                last = number;
            }

            return true;
        }

        public bool allLower(List<int> input)
        {
            var last = input.FirstOrDefault();
            foreach (var number in input.Skip(1))
            {
                if (number >= last)
                {
                    return false;
                }

                last = number;
            }

            return true;
        }

        public static List<List<int>> ParseInput(string filePath)
        {
            return Helpers.ReadAllIntsPerLine(filePath);
        }
    }
}