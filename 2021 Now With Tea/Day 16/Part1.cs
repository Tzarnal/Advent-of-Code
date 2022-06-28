using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_16
{
    //https://adventofcode.com/2021/day/16
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Packet Decoder. Part One."; }

        public void Run()
        {
            var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<string> input)
        {
            Log.Information("A Solution Can Be Found.");
        }

        public static List<string> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath);
        }
    }
}