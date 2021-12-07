using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_07
{
    //https://adventofcode.com/2021/day/07
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: The Treachery of Whales. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<int> input)
        {
            var maxPosition = input.Max();
            var fuelCosts = new Dictionary<int, double>();

            for (int i = 0; i <= maxPosition; i++)
            {
                double fuelCost = 0;

                foreach (var crab in input)
                {
                    fuelCost += Math.Abs(crab - i);
                }

                fuelCosts[i] = fuelCost;
            }

            var bestPosition = fuelCosts.OrderBy(f => f.Value).First();

            Log.Information("The best position for the crabs to aling on is {hPos} which costs {fuel} fuel.",
                bestPosition.Key, bestPosition.Value);
        }

        public static List<int> ParseInput(string filePath)
        {
            return Helpers.ReadAllIntsFile(filePath);
        }
    }
}