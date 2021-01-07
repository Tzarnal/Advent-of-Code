using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_13
{
    //https://adventofcode.com/2015/day/13#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Knights of the Dinner Table. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(Dictionary<string, Dictionary<string, int>> input)
        {
            var names = input.Select(d => d.Key).ToList();

            //So, add yourself to the list, and give all happiness relationships that involve you a score of 0.
            input.Add("Me", new Dictionary<string, int>());

            foreach (var name in names)
            {
                input["Me"].Add(name, 0);
                input[name].Add("Me", 0);
            }

            //Last verse same as the first
            Part1.Solve(input);
        }
    }
}