using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_05
{
    //https://adventofcode.com/2015/day/5
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Doesn't He Have Intern-Elves For This? Part Two."; }

        public void Run()
        {
            //var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<string> input)
        {
            var nice = 0;
            foreach (var line in input)
            {
                if (IsNice(line))
                {
                    nice++;
                }
            }

            Log.Information("Found {nice} nice strings in {input} strings.", nice, input.Count());
        }

        public bool IsNice(string input)
        {
            var targetStrings = new List<string>();

            for (var i = 0; i < input.Length - 1; i++)
            {
                var pair = $"{input[i]}{input[i + 1]}";
                if (!targetStrings.Contains(pair))
                {
                    targetStrings.Add(pair);
                }
            }

            bool hasDouble = false;
            foreach (var targetString in targetStrings)
            {
                var diff = input.Count(targetString);
                if (diff > 1)
                {
                    hasDouble = true;
                    break;
                }
            }

            if (!hasDouble)
            {
                return false;
            }

            for (var i = 0; i < input.Length - 2; i++)
            {
                if (input[i] == input[i + 2])
                {
                    return true;
                }
            }

            return false;
        }

        private List<string> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath);
        }
    }
}