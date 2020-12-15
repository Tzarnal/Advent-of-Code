using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_15
{
    //https://adventofcode.com/2020/day/15
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Rambunctious Recitation. Part One."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            foreach (var inputNumbers in testinputList)
            {
                Solve(inputNumbers);
            }

            //Solve(testinputList.First());

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            foreach (var inputNumbers in inputList)
            {
                Solve(inputNumbers);
            }
        }

        public void AddOrInitialize(Dictionary<int, List<int>> dict, int key, int value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key].Add(value);
            }
            else
            {
                dict[key] = new List<int>();
                dict[key].Add(value);
            }
        }

        public void Solve(List<int> input)
        {
            var spokenNumbers = new Dictionary<int, List<int>>();
            int lastNumber = 0;

            int i = 1;

            foreach (var n in input)
            {
                AddOrInitialize(spokenNumbers, n, i);
                lastNumber = n;
                //Log.Verbose("Turn {turn}. Spoken: {lastNumber}", i, lastNumber);
                i++;
            }

            for (i = i; i <= 2020; i++)
            {
                if (spokenNumbers.ContainsKey(lastNumber))
                {
                    if (spokenNumbers[lastNumber].Count > 1)
                    {
                        var lastTurns = spokenNumbers[lastNumber].OrderByDescending(s => s).Take(2).ToArray();
                        lastNumber = lastTurns[0] - lastTurns[1];
                    }
                    else
                    {
                        lastNumber = 0;
                    }
                    AddOrInitialize(spokenNumbers, lastNumber, i);
                }
                else
                {
                    AddOrInitialize(spokenNumbers, lastNumber, i);
                    lastNumber = 0;
                }
                //Log.Verbose("Turn {turn}. Spoken: {lastNumber}", i, lastNumber);
            }

            Log.Information("For input {@input} after turn 2020 the number {lastNumber} was spoken.", input, lastNumber);
        }

        private List<List<int>> ParseInput(string filePath)
        {
            var input = Helpers.ReadStringsFile(filePath);
            var output = new List<List<int>>();

            foreach (var line in input)
            {
                var numbers = line.Split(",").Select(n => int.Parse(n)).ToList();
                output.Add(numbers);
            }

            return output;
        }
    }
}