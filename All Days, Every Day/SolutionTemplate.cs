using System.Collections.Generic;
using System;
using System.IO;
using Advent;
using Serilog;

namespace Day_00
{
    //Page Link
    internal class Part : IAdventProblem
    {
        public string ProblemName { get => "Day 00: Something Something. Part One."; }

        public void Run()
        {
            var inputFile = File.ReadAllLines("Day 00/input.txt");

            Log.Information("A Solution Can Be Found.");
        }
    }
}