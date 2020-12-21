using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_21
{
    //https://adventofcode.com/2020/day/21
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Allergen Assessment. Part One."; }

        public void Run()
        {
            var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<(HashSet<string> Ingredients, List<string> Allergens)> input)
        {
            var allAllergens = input.SelectMany(i => i.Allergens).Distinct().ToArray();

            var allergernsToIngredients =
                allAllergens.Select(allergens =>
                    (allergen: allergens,
                     candidates: input.Where(i => i.Allergens.Contains(allergens))
                             .Select(f => f.Ingredients)
                             .Aggregate((a, next) => new HashSet<string>(a.Intersect(next)))))
                             .ToDictionary(c => c.Item1, c => c.Item2);

            var allAllergenIngredients = allergernsToIngredients.SelectMany(allergen => allergen.Value).Distinct();

            var output = input.Sum(food => food.Ingredients.Count(ingredient => !allAllergenIngredients.Contains(ingredient)));

            Log.Information("Found {count} occurences of non allergenic ingredients in {totalfoods} foods."
                , output, input.Count());
        }

        private List<(HashSet<string> Ingredients, List<string> allergens)> ParseInput(string filePath)
        {
            var input = File.ReadAllLines(filePath);

            var output = new List<(HashSet<string> Ingredients, List<string> allergens)>();

            foreach (var line in input)
            {
                var split = line.Split("(contains");

                var ingredients = split[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToHashSet();

                var allergens = split[1].Replace(")", "").Replace(",", "").Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

                output.Add((ingredients, allergens));
            }

            return output;
        }
    }
}