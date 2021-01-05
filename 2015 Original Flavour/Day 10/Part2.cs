using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Advent;
using RegExtract;

namespace Day_10
{
    //https://adventofcode.com/2015/day/10#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Elves Look, Elves Say. Part Two."; }

        public void Run()
        {
            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Part1.Solve(input, 50); //Same problem as part 1 just 10 more iterations
        }
    }
}