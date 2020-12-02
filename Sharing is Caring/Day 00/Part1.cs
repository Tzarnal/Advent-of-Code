using System;
using System.IO;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_00
{
    //Problem URL
    internal class Part1 : IAdventProblem
    {
        public string ProblemName { get => "Day 00: Something Something. Part One."; }

        public void Run()
        {
            var inputList = Helpers.ReadStringsFile("Day 01/input.txt");
        }
    }
}