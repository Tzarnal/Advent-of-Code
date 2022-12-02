using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Diagnostics.Metrics;

namespace Day_02
{
    //https://adventofcode.com/2022/day/2#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Rock Paper Scissors. Part Two."; }

        public void Run()
        {
            var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(string pick, string counter)> strategy)
        {           
            var scores = new Dictionary<string, int>
            {
                { "A", 1 },
                { "B", 2 },
                { "C", 3 },
            };

            var win = new Dictionary<string, string>
            {
                {"A", "B" },
                {"B", "C" },
                {"C", "A" },
            };

            var loose = new Dictionary<string, string>
            {
                {"A", "C" },
                {"B", "A" },
                {"C", "B" },
            };

            var totalScore = 0;

            foreach (var pair in strategy)
            {
                var pick = pair.pick;
                var plan = pair.counter;
                var counter = "?";

                switch (plan)
                {
                    case "X": //lose
                        totalScore += 0; // for the loss
                        counter = loose[pick];
                        break;

                    case "Y": //draw
                        totalScore += 3; // for the draw
                        counter = pick;
                        break;

                    case "Z": //win
                        totalScore += 6; // for the win
                        counter = win[pick];
                        break;
                }

                totalScore += scores[counter];
            }

            Log.Information("Total score according to strategy guide is {totalScore}.", totalScore);
        }
    }
}