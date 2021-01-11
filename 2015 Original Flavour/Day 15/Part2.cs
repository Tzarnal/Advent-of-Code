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
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}:  Science for Hungry People. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
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
                var (cookieScore, cookieCalories) = CookieScoreAndCalories(cookieRecipe);

                if (cookieCalories == 500 && cookieScore > bestCookieScore)
                {
                    bestCookieScore = cookieScore;
                    bestCookieRecipe = cookieRecipe;
                }
            }

            Log.Information("The best cookie scored {score}.", bestCookieScore);
        }

        public static (long Score, long Calories) CookieScoreAndCalories(Dictionary<Ingredient, int> Recipe)
        {
            long capacity = 0;
            long durability = 0;
            long flavor = 0;
            long texture = 0;

            long calories = 0;

            foreach (var (ingredient, amount) in Recipe)
            {
                capacity += ingredient.Capacity * amount;
                durability += ingredient.Durability * amount;
                flavor += ingredient.Flavor * amount;
                texture += ingredient.Texture * amount;

                calories += ingredient.Calories * amount;
            }

            if (capacity < 0) capacity = 0;
            if (durability < 0) durability = 0;
            if (flavor < 0) flavor = 0;
            if (texture < 0) texture = 0;

            if (calories < 0) calories = 0;

            return (capacity * durability * flavor * texture, calories);
        }
    }
}