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
        public string ProblemName { get => "Day 00: Something Something. Part Two."; }

        public void Run()
        {
            var inputList = Helpers.ReadStringsFile("Day 01/input.txt");

            Log.Information("A Solution Can Be Found.");
        }
    }
}