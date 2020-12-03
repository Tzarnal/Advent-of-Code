using System.Collections.Generic;
using System.IO;
using Advent;
using Serilog;

namespace Day_01
{
    //https://adventofcode.com/2020/day/1
    internal class Part1 : IAdventProblem
    {
        public string ProblemName { get => "Day 1: Report Repair. Part One."; }

        public void Run()
        {
            var inputFile = File.ReadAllLines("Day 01/input.txt");
            var inputList = new List<int>();

            foreach (var line in inputFile)
            {
                int c;
                if (int.TryParse(line, out c))
                {
                    inputList.Add(c);
                }
                else
                {
                    Log.Warning("Conversion Error: {c}", c);
                }
            }

            foreach (var inputN in inputList)
            {
                var difference = 2020 - inputN;

                if (inputList.Contains(difference))
                {
                    var product = inputN * difference;

                    Log.Information("Found it: {inputN} and {difference}. Product {product}", inputN, difference, product);
                    return;
                }
            }
        }
    }
}