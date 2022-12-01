using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_01
{
    //https://adventofcode.com/2022/day/1
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Calorie Counting. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<List<int>> input)
        {
            var inventoryTotals = new List<int>();

            foreach(var inventory in input)
            {
                var inventoryTotal = inventory.Sum(i => i);

                inventoryTotals.Add(inventoryTotal);
            }

            var most = inventoryTotals.OrderByDescending(i => i).FirstOrDefault();

            Log.Information("Found {most} calories is the most calories among the Elves.",
                most);

        }

        public static List<List<int>> ParseInput(string filePath)
        {
            return Helpers.ReadAllRecordsInt(filePath);
        }
    }
}