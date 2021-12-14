using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Text;

namespace Day_14
{
    //https://adventofcode.com/2021/day/14#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Extended Polymerization. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve((string Template, Dictionary<string, string> Rules) input)
        {
            var polymer = input.Template;
            var rules = input.Rules;

            var pairs = polymer
                .Zip(polymer.Skip(1), (left, right) => $"{left}{right}")
                .ToLookup(l => l, _ => 1)
                .ToDictionary(
                    k => k.Key,
                    k => (long)k.Sum()
                );

            var counts = polymer
                .Distinct()
                .ToDictionary(
                    k => k,
                    v => (long)polymer.Count(v.ToString())
                );

            for (int i = 0; i < 40; i++)
            {
                var newPairs = new Dictionary<string, long>();

                foreach (var pair in pairs)
                {
                    var firstChar = rules[pair.Key][0];

                    //Count things
                    if (counts.ContainsKey(firstChar))
                    {
                        counts[firstChar] += pair.Value;
                    }
                    else
                    {
                        counts.Add(firstChar, pair.Value);
                    }

                    //"Grow" Pairs
                    var leftSide = pair.Key[0] + rules[pair.Key];
                    if (newPairs.ContainsKey(leftSide))
                        newPairs[leftSide] += pair.Value;
                    else
                        newPairs.Add(leftSide, pair.Value);

                    var righSide = rules[pair.Key] + pair.Key[1];
                    if (newPairs.ContainsKey(righSide))
                        newPairs[righSide] += pair.Value;
                    else
                        newPairs.Add(righSide, pair.Value);
                }

                pairs = newPairs;
            }

            var difference = counts.Max(m => m.Value) - counts.Min(m => m.Value);

            Log.Information("Difference in quantities is {difference}", difference);
        }
    }
}