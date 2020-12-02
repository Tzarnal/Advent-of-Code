using System;
using System.IO;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_03
{
    //https://adventofcode.com/2020/day/3#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname = "03";
        public string ProblemName { get => $"Day {Dayname}: Something Something. Part Two."; }

        public void Run()
        {
            var inputList = ParseInput($"Day {Dayname}/input.txt");
            var testinputList = ParseInput($"Day {Dayname}/input.txt");

            Solve(testinputList);
            Solve(inputList);
        }

        public void Solve(List<string> input)
        {
            Log.Information("A Solution Can Be Found.");
        }

        private List<string> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath);
        }
    }
}