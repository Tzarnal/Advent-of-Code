using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_21
{
    //https://adventofcode.com/2020/day/21#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Allergen Assessment. Part Two."; }

        public void Run()
        {
            var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<(HashSet<string> Ingredients, List<string> Allergens)> input)
        {
            var allAllergens = input.SelectMany(i => i.Allergens).Distinct().ToArray();

            //Map ingredients with allergens
            var allergernsToIngredients =
                allAllergens.Select(allergens =>
                    (allergen: allergens,
                     candidates: input.Where(i => i.Allergens.Contains(allergens))
                             .Select(f => f.Ingredients)
                             .Aggregate((a, next) => new HashSet<string>(a.Intersect(next)))))
                             .ToDictionary(c => c.Item1, c => c.Item2);

            var allAllergenIngredients = allergernsToIngredients.SelectMany(allergen => allergen.Value).Distinct();

            var cdiList = new Dictionary<string, HashSet<string>>();

            //Some sudoku
            while (allergernsToIngredients.Count > 0)
            {
                var singleIngredientAllergens = allergernsToIngredients.Where(kvp => kvp.Value.Count() == 1);

                foreach (var singleIngredientAllergen in singleIngredientAllergens)
                {
                    var ingredient = singleIngredientAllergen.Value.First();
                    Log.Verbose(ingredient);

                    //Remove from the map, add to the cdi
                    allergernsToIngredients.Remove(singleIngredientAllergen.Key);
                    cdiList.Add(singleIngredientAllergen.Key, singleIngredientAllergen.Value);

                    //Remove from remaining
                    foreach (var allergern in allergernsToIngredients)
                    {
                        allergern.Value.Remove(ingredient);
                    }
                }
            }

            var cdiListOrdered = cdiList.OrderBy(kvp => kvp.Key);
            var cdiOutput = string.Join(",", cdiListOrdered.Select(kvp => kvp.Value.First()));

            Log.Information("Canonical dangerous ingredient list: {list}", cdiOutput);
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