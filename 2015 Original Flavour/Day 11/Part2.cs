using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_11
{
    //https://adventofcode.com/2015/day/11#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Corporate Policy. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public static void Solve(List<string> input)
        {
            foreach (var line in input)
            {
                var password = line;

                while (!Part1.IsValid(password))
                {
                    password = Part1.Increment(password);
                }

                Log.Information("{line} became {password}, but expired again", line, password);
                var password2 = Part1.Increment(password);

                while (!Part1.IsValid(password2))
                {
                    password2 = Part1.Increment(password2);
                }

                Log.Information("{password} became {password2}", password, password2);
            }
        }
    }
}