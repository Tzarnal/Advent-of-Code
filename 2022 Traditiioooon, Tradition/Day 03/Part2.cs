using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_03
{
    //https://adventofcode.com/2022/day/3#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Rucksack Reorganization. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<string> input)
        {
            var groups = input.Count / 3;
            var total = 0;

            for (var i = 0; i < groups; i++) 
            {
                var groupLines = input.Skip(i * 3).Take(3);
                
                var counts = new Dictionary<char, int>();
                foreach(var line in groupLines)
                {
                    var items = line.ToList().Distinct();
                    foreach(var c in items)
                    {
                        counts.Increment(c);
                    }
                }

                var badge = counts.Where(c => c.Value == 3).First();
                total += Part1.ToPriority(badge.Key);
            }

            Log.Information("The sum of pass priorities is {total}.", total);
        }
    }
}