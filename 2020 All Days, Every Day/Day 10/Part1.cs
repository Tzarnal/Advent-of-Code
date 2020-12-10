using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_10
{
    //https://adventofcode.com/2020/day/10
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Adapter Array. Part One."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<int> input)
        {
            input.Add(0);//Initial zero to avoid off by one
            input = input.OrderBy(i => i).ToList();
            input.Add(input.Max() + 3);//Final device is +3  jolts

            var differences = input.Zip(input.Skip(1), (f, s) => s - f);
            var threejolts = differences.Where(i => i == 3).Count();
            var onejolts = differences.Where(i => i == 1).Count();

            Log.Information("Found {onejolts} one jolt differences and {threejolts} three jolt differences. Product {product}",
                onejolts, threejolts, onejolts * threejolts);
        }

        private List<int> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath).ConvertAll(i => int.Parse(i));
        }
    }
}