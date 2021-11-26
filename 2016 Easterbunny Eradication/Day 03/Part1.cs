using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_03
{
    //https://adventofcode.com/2016/day/03
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Squares With Three Sides. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
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

        public static List<List<int>> ParseInput(string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            var output = new List<List<int>>();

            foreach (var line in lines)
            {
                output.Add(Helpers.ReadAllIntsStrings(line));
            }

            return output;
        }
    }
}