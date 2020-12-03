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
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Something Something. Part One."; }

        public void Run()
        {
            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);

            //var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);
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