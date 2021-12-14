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
    //https://adventofcode.com/2021/day/14
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Extended Polymerization. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve((string Template, Dictionary<string, string> Rules) input)
        {
            var polymer = input.Template;
            for (int i = 0; i < 10; i++)
            {
                var newPolymer = new StringBuilder();

                foreach (var pair in
                    polymer.Zip(polymer.Skip(1), (left, right) => left + "" + right))
                {
                    newPolymer.Append(pair[0]);
                    newPolymer.Append(input.Rules[pair]);
                }

                newPolymer.Append(polymer.Last());

                polymer = newPolymer.ToString();
            }

            var counts = polymer
                            .Distinct()
                            .Select(
                                c => (c, polymer.Count(c.ToString()))
                             )
                            .OrderByDescending(n => n.Item2)
                            .ToList();

            var difference = counts.First().Item2 - counts.Last().Item2;

            Log.Information("Difference in quantities is {difference}", difference);
        }

        public static (string Template, Dictionary<string, string> Rules) ParseInput(string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            var template = lines[0];
            var rules = new Dictionary<string, string>();

            foreach (var line in lines.Skip(2))
            {
                var (leftSide, rightSide) = line.Extract<(string, string)>(@"(.+) \-\> (.+)");

                //Simple aproach
                rules.Add(leftSide, rightSide);
            }

            return (template, rules);
        }
    }
}