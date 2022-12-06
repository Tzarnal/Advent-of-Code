using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_06
{
    //https://adventofcode.com/2022/day/6
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Tuning Trouble. Part One."; }

        public void Run()
        {
            var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(string input)
        {
            var q = new Queue<Char>();
            var i = 1;
                
            foreach(var c in input)
            {
                if (q.Count() >= 4)
                {
                    q.Dequeue();
                }

                q.Enqueue(c);

                

                if(q.ToList().Distinct().Count() == 4)
                {
                    break;
                }

                i++;
            }
            
            Log.Information("Found start-of-packet marker after {i} characters.", i);
        }

        public static string ParseInput(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}