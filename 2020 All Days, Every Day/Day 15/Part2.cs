using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_15
{
    //Problem URL
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Rambunctious Recitation. Part Two."; }

        public void Run()
        {
            var inputList = ParseInput($"Day {Dayname}/input.txt");

            Solve(inputList[0]);
        }

        public void Remember(Dictionary<int, Memory> dict, int key, int value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key].Remember(value);
            }
            else
            {
                dict[key] = new Memory();
                dict[key].Remember(value);
            }
        }

        public void Solve(List<int> input)
        {
            var spokenNumbers = new Dictionary<int, Memory>();
            int lastNumber = 0;

            int i = 1;

            foreach (var n in input)
            {
                Remember(spokenNumbers, n, i);
                lastNumber = n;
                i++;
            }

            for (; i <= 30000000; i++)
            {
                if (spokenNumbers.ContainsKey(lastNumber))
                {
                    if (spokenNumbers[lastNumber].MentionedTwice)
                    {
                        lastNumber = spokenNumbers[lastNumber].Difference;
                    }
                    else
                    {
                        lastNumber = 0;
                    }
                    Remember(spokenNumbers, lastNumber, i);
                }
                else
                {
                    Remember(spokenNumbers, lastNumber, i);
                    lastNumber = 0;
                }
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