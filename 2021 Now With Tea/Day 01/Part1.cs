using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_01
{
    //https://adventofcode.com/2021/day/01
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Sonar Sweep. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<int> input)
        {
            var previous = 0;
            var increases = 0;

            foreach (var i in input)
            {
                if (previous == 0)
                {
                    previous = i;
                    continue;
                }

                if (i > previous)
                {
                    increases++;
                }

                previous = i;
            }

            Log.Information("Found {increases} measurments that are larger than the previous.",
                increases);
        }

        public static List<int> ParseInput(string filePath)
        {
            return Helpers.ReadAllIntsFile(filePath);
        }
    }
}