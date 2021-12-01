using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_01
{
    //https://adventofcode.com/2021/day/01#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Sonar Sweep. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<int> input)
        {
            var sums = new List<int>();

            for (var i = 0; i < input.Count - 2; i++)
            {
                sums.Add(input[i] + input[i + 1] + input[i + 2]);
            }

            var previous = 0;
            var increases = 0;

            foreach (var i in sums)
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

            Log.Information("Found {increases} sums that are larger than the previous.",
                increases);
        }
    }
}