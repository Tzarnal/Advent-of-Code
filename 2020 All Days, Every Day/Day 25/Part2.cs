using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_25
{
    //https://adventofcode.com/2020/day/25#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Combo Breaker. Part Two."; }

        public void Run()
        {
            Log.Information("Merry Christmas. There was never a Day 25 Part 2");
        }
    }
}