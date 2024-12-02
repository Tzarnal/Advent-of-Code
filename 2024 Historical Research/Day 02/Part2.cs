using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_02
{
    //https://adventofcode.com/2024/day/02#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Red-Nosed Reports. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
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

        public bool tryRemovingLevels(List<int> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                List<int> line = new(input);
                line.RemoveAt(i);

                if (!safeDiff(line))
                {
                    continue;
                }

                if (allHigher(line) || allLower(line))
                {
                    return true;
                }
            }

            return false;
        }

        public void Solve(List<List<int>> input)
        {
            var safeReports = new List<List<int>>();

            foreach (var line in input)
            {
                if (tryRemovingLevels(line))
                {
                    safeReports.Add(line);
                }
            }

            Log.Information("Found {count} safe reports", safeReports.Count());
        }
    }
}