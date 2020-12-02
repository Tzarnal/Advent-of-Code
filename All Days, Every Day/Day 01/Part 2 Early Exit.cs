using Advent;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

namespace Day_01
{
    //https://adventofcode.com/2020/day/1#part2
    internal class Part2EarlyExit : IAdventProblem
    {
        public string ProblemName { get => "Day 1: Report Repair. Part Two. (Early Exit)"; }

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

            foreach (var number1 in inputList)
            {
                foreach (var number2 in inputList)
                {
                    foreach (var number3 in inputList)
                    {
                        if (number1 + number2 + number3 == 2020)
                        {
                            var product = number1 * number2 * number3;
                            Log.Information("Found it: {number1}+{number2}+{number3} = 2020. Product: {product}",
                                number1, number2, number3, product);
                            return; //This return would make it More Efficient, so i've left it out for a joke.
                        }
                    }
                }
            }
        }
    }
}