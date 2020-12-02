using System;
using System.IO;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_00
{
    //Problem URL
    public class Part2 : IAdventProblem
    {
        private string Dayname = "00";
        public string ProblemName { get => $"Day {Dayname}: Something Something. Part Two."; }

        public void Run()
        {
            var inputList = Helpers.ReadStringsFile($"Day {Dayname}/input.txt");

            Log.Information("A Solution Can Be Found.");
        }
    }
}