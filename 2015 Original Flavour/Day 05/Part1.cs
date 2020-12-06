using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_05
{
    //https://adventofcode.com/2015/day/5#part2
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Doesn't He Have Intern-Elves For This? Part One."; }

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
                    nice++;
            }

            Log.Information("Found {nice} nice strings in {input} strings.", nice, input.Count());
        }

        public bool IsNice(string input)
        {
            var dissallowedStrings = new string[] { "ab", "cd", "pq", "xy" };
            var vowels = "aeiou";

            foreach (var dissallowedString in dissallowedStrings)
            {
                if (input.Contains(dissallowedString))
                {
                    return false;
                }
            }

            var vowelCount = 0;

            foreach (var vowel in vowels)
            {
                vowelCount += input.Count(vowel.ToString());
            }

            if (vowelCount < 3)
                return false;

            for (var i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == input[i + 1])
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