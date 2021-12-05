using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Drawing;

namespace Day_05
{
    //https://adventofcode.com/2021/day/05#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Hydrothermal Venture. Part Two."; }

        public void Run()
        {
            var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput, 9);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(Point Start, Point End)> input, int dimension = 99)
        {
            Log.Information("A Solution Can Be Found.");
        }
    }
}