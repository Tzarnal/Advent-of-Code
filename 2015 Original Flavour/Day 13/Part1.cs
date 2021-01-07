using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_13
{
    //https://adventofcode.com/2015/day/13
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Knights of the Dinner Table. Part One."; }

        public void Run()
        {
            var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(Dictionary<string, Dictionary<string, int>> input)
        {
            var names = input.Select(d => d.Key);
            var permutations = names.GetPermutations(names.Count());

            var bestHappiness = 0;
            var bestVariant = new List<string>();

            foreach (var variant in permutations)
            {
                var table = variant.ToList();
                var totalHappiness = 0;
                for (int i = 0; i < names.Count(); i++)
                {
                    var left = i - 1;
                    var right = i + 1;

                    if (right >= names.Count())
                    {
                        right = 0;
                    }

                    if (left < 0)
                    {
                        left = names.Count() - 1;
                    }

                    totalHappiness += input[table[i]][table[left]];
                    totalHappiness += input[table[i]][table[right]];
                }

                if (totalHappiness > bestHappiness)
                {
                    bestHappiness = totalHappiness;
                    bestVariant = variant.ToList();
                }
            }

            Log.Information("Best arrangement is {bestVariant} with a score of {bestHappiness}",
                bestVariant, bestHappiness);
        }

        public static Dictionary<string, Dictionary<string, int>> ParseInput(string filePath)
        {
            var relationships = new Dictionary<string, Dictionary<string, int>>();
            var relationshipLines = new List<(string Person, string Kind, int Happiness, string Relation)>();

            foreach (var line in (string[])File.ReadAllLines(filePath))
            {
                var relationshipLine = line.Extract<(string, string, int, string)>
                    (@"(.+) would (gain|lose) (\d+) happiness units by sitting next to (.+)\.");

                relationshipLines.Add(relationshipLine);
            }

            foreach (var relation in relationshipLines)
            {
                if (!relationships.ContainsKey(relation.Person))
                {
                    relationships.Add(relation.Person, new Dictionary<string, int>());
                }
            }

            foreach (var relation in relationshipLines)
            {
                var mod = 1;

                if (relation.Kind == "lose")
                {
                    mod = -1;
                }

                relationships[relation.Person].Add(relation.Relation, relation.Happiness * mod);
            }

            return relationships;
        }
    }
}