using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_03
{
    //https://adventofcode.com/2016/day/03#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Squares With Three Sides. Part Two."; }

        public void Run()
        {
            //var testinput = ReOrder(Part1.ParseInput($"Day {Dayname}/inputTest.txt"));
            //Solve(testinput);

            var input = ReOrder(Part1.ParseInput($"Day {Dayname}/input.txt"));
            Solve(input);
        }

        public void Solve(List<List<int>> input)
        {
            var possible = 0;

            foreach (var line in input)
            {
                var orderedLine = line.OrderBy(l => l).ToArray();

                var sum = orderedLine[0] + orderedLine[1];

                if (sum > orderedLine[2])
                {
                    possible++;
                }
            }

            Log.Information("Found {possible} possible triangles.",
                possible);
        }

        public List<List<int>> ReOrder(List<List<int>> input)
        {
            var output = new List<List<int>>();

            var data = input.ToArray();

            for (var i = 0; i < data.Length; i += 3)
            {
                var line0 = new List<int>();
                var line1 = new List<int>();
                var line2 = new List<int>();

                for (var j = 0; j < 3; j++)
                {
                    line0.Add(data[i + j][0]);
                    line1.Add(data[i + j][1]);
                    line2.Add(data[i + j][2]);
                }

                output.Add(line0);
                output.Add(line1);
                output.Add(line2);
            }

            return output;
        }
    }
}