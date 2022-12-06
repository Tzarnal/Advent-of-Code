using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_06
{
    //https://adventofcode.com/2022/day/6#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Tuning Trouble. Part Two."; }

        public void Run()
        {
            var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(string input)
        {
            var q = new Queue<Char>();
            var i = 1;

            foreach (var c in input)
            {
                if (q.Count() >= 14)
                {
                    q.Dequeue();
                }

                q.Enqueue(c);



                if (q.ToList().Distinct().Count() == 14)
                {
                    break;
                }

                i++;
            }

            Log.Information("Found start-of-message marker after {i} characters.", i);
        }
    }
}