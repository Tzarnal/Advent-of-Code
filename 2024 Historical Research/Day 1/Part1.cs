using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_1
{
    //https://adventofcode.com/2024/day/1
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Historian Hysteria. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve((IEnumerable<int> left, IEnumerable<int> right) input)
        {
            var sum = 0;

            var left = input.left.OrderByDescending(x => x).ToArray();
            var right = input.right.OrderByDescending(x => x).ToArray();

            for (var i = 0; i < left.Count(); i++)
            {
                var absSum = Math.Abs(left[i] - right[i]);
                sum += absSum;
            }

            Log.Information("Over {count} pairs the sum is {sum}.",
                left.Count(),
                sum);
        }

        public static (IEnumerable<int> left, IEnumerable<int> right) ParseInput(string filePath)
        {
            var left = new List<int>();
            var right = new List<int>();

            foreach (var line in File.ReadAllLines(filePath))
            {
                var bits = line.Split("   ");

                left.Add(int.Parse(bits[0]));
                right.Add(int.Parse(bits[1]));
            }

            return (left, right);
        }
    }
}