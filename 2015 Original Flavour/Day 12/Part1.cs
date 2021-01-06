using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Text.RegularExpressions;

namespace Day_12
{
    //https://adventofcode.com/2015/day/12
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: JSAbacusFramework.io. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<int> input)
        {
            var awnser = input.Sum();
            Log.Information("Sum of all integers in input file is {awnser}", awnser);
        }

        public static List<int> ParseInput(string filePath)
        {
            return Helpers.ReadAllIntsFile(filePath);
        }
    }
}