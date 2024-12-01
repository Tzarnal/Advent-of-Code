using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_1
{
    //https://adventofcode.com/2024/day/1#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Historian Hysteria. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve((IEnumerable<int> left, IEnumerable<int> right) input)
        {
            var left = input.left.ToArray();
            var numberOccurence = input.right.GroupBy(x => x).ToDictionary(o => o.Key, c => c.Count());

            var similarityScore = 0;

            for (var i = 0; i < left.Length; i++)
            {
                var number = left[i];
                var count = 0;
                if (numberOccurence.TryGetValue(number, out int value))
                {
                    count = value;
                }

                similarityScore += count * number;
            }

            Log.Information("Over {count} pairs the similarity score is {sum}.",
                left.Count(),
                similarityScore);
        }
    }
}