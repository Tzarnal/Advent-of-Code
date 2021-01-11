using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_15
{
    //Problem URL
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}:  Science for Hungry People. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<Ingredient> Ingredients)
        {
            var totalIngredients = Ingredients.Count;

            long bestCookieScore = 0;
            Dictionary<Ingredient, int> bestCookieRecipe;

            foreach (var Amounts in Helpers.PartitionIntoPossibleParts(100, totalIngredients))
            {
                var cookieRecipe = new Dictionary<Ingredient, int>();
                for (int i = 0; i < totalIngredients; i++)
                {
                    cookieRecipe.Add(Ingredients[i], Amounts[i]);
                }
                var cookieScore = CookieScore(cookieRecipe);

                if (cookieScore > bestCookieScore)
                {
                    bestCookieScore = cookieScore;
                    bestCookieRecipe = cookieRecipe;
                }
            }

            Log.Information("The best cookie scored {score}.", bestCookieScore);
        }

        public static long CookieScore(Dictionary<Ingredient, int> Recipe)
        {
            long capacity = 0;
            long durability = 0;
            long flavor = 0;
            long texture = 0;

            foreach (var (ingredient, amount) in Recipe)
            {
                capacity += ingredient.Capacity * amount;
                durability += ingredient.Durability * amount;
                flavor += ingredient.Flavor * amount;
                texture += ingredient.Texture * amount;
            }

            if (capacity < 0) capacity = 0;
            if (durability < 0) durability = 0;
            if (flavor < 0) flavor = 0;
            if (texture < 0) texture = 0;

            return capacity * durability * flavor * texture;
        }

        public static List<Ingredient> ParseInput(string filePath)
        {
            var input = Helpers.ReadStringsFile(filePath);

            var ingredients = new List<Ingredient>();
            foreach (var line in input)
            {
                var ingredient = line.Extract<Ingredient>(
                    @"(?<Name>.+): capacity (?<Capacity>-?\d+), durability (?<Durability>-?\d+), flavor (?<Flavor>-?\d+), texture (?<Texture>-?\d+), calories (?<Calories>-?\d+)");

                ingredients.Add(ingredient);
            }

            return ingredients;
        }
    }

    public record Ingredient
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Durability { get; set; }
        public int Flavor { get; set; }
        public int Texture { get; set; }
        public int Calories { get; set; }
    }
}