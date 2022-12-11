using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_04
{
    //https://adventofcode.com/2022/day/4#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Camp Cleanup. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(IEnumerable<int> Left, IEnumerable<int> Right)> input)
        {
            var overlaps = 0;

            foreach (var pair in input)
            {
                if (Overlaps(pair.Left, pair.Right))
                {
                    overlaps++;
                }
                else if (Overlaps(pair.Right, pair.Left))
                {
                    overlaps++;
                }
            }

            Log.Information("Found {overlaps} assingment pairs where one range overlaps the other.", overlaps);
        }

        public static bool Overlaps(IEnumerable<int> needle, IEnumerable<int> haystack)
        {
            var overlap = haystack.Intersect(needle);

            if (overlap.Count() > 0 )
            {
                return true;
            }

            return false;
        }
    }
}