using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_01
{
    //https://adventofcode.com/2022/day/1#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Calorie Counting. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<List<int>> input)
        {
            var inventoryTotals = new List<int>();

            foreach (var inventory in input)
            {
                var inventoryTotal = inventory.Sum(i => i);

                inventoryTotals.Add(inventoryTotal);
            }

            var top = inventoryTotals.OrderByDescending(i => i).Take(3);

            var topSum = top.Sum(i => i);

            Log.Information("Found {topSum} calories among the top 3.",
                topSum);
            
        }
    }
}