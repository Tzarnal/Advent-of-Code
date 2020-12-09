using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_09
{
    //https://adventofcode.com/2020/day/9
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Encoding Error. Part One."; }

        public void Run()
        {
            //var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList, 5);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList, 25);
        }

        public void Solve(List<double> input, int window)
        {
            for (var i = window; i < input.Count; i++)
            {
                var summedNumbers = new HashSet<double>();
                var candidateSubset = input.Skip(i - window).Take(window);
                foreach (var number in candidateSubset)
                {
                    var sums = candidateSubset.Distinct().Select(c => c + number);
                    summedNumbers.UnionWith(sums);
                }

                if (!summedNumbers.Contains(input[i]))
                {
                    Log.Information("After {i} numbers. {input} is not a sum of the {window} previous numbers.", i, input[i], window);
                    return;
                }
            }

            Log.Error("Could not find weakness.");
        }

        private List<double> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath).Select(n => double.Parse(n)).ToList();
        }
    }
}