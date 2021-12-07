using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_07
{
    //https://adventofcode.com/2021/day/07#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: The Treachery of Whales. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<int> input)
        {
            var maxPosition = input.Max();
            FuelCost(maxPosition); // Pre compute some fuel costs
            var moveCosts = new Dictionary<int, int>();

            for (int i = 0; i <= maxPosition; i++)
            {
                int fuelCost = 0;

                foreach (var crab in input)
                {
                    fuelCost += FuelCost(Math.Abs(crab - i));
                }

                moveCosts[i] = fuelCost;
            }

            var orderedMoves = moveCosts.OrderBy(move => move.Value);
            var bestMove = orderedMoves.First();

            Log.Information("The best position for the crabs to aling on is {hPos} which costs {fuel} fuel.",
                bestMove.Key, bestMove.Value);
        }

        private Dictionary<int, int> ComputedCosts = new Dictionary<int, int>();

        private int FuelCost(int distance)
        {
            if (ComputedCosts.ContainsKey(distance))
            {
                return ComputedCosts[distance];
            }

            var gaussCost = (distance * (distance + 1)) / 2;
            ComputedCosts[distance] = gaussCost;

            return gaussCost;
        }
    }
}