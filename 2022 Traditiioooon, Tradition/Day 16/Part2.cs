using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_16
{
    //https://adventofcode.com/2022/day/16#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Proboscidea Volcanium. Part Two."; }

        public void Run()
        {
            var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(Dictionary<string, Valve> valves)
        {
            Log.Information("A Solution Can Be Found.");
        }
    }

}