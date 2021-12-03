using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_03
{
    //https://adventofcode.com/2021/day/03
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Binary Diagnostic. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<string> input)
        {
            var numberLength = input.First().Length;
            var oneCounts = new int[numberLength];
            var zeroCounts = new int[numberLength];

            foreach (var binaryNumber in input)
            {
                for (var i = 0; i < numberLength; i++)
                {
                    if (binaryNumber[i] == '1')
                    {
                        oneCounts[i]++;
                    }
                    else
                    {
                        zeroCounts[i]++;
                    }
                }
            }

            var gammaRate = "";
            var epsilonRate = "";

            for (var i = 0; i < numberLength; i++)
            {
                if (oneCounts[i] > zeroCounts[i])
                {
                    gammaRate += "1";
                    epsilonRate += "0";
                }
                else
                {
                    gammaRate += "0";
                    epsilonRate += "1";
                }
            }

            var gammaRateDecimal = Convert.ToInt64(gammaRate, 2);
            var epsilonRateDecimal = Convert.ToInt64(epsilonRate, 2);

            Log.Information("After {input} of numbers the Gamma Rate is {gammaRate}[{gammaRateDecimal}] and the is {epsilonRate}[{epsilonRateDecimal}]. The power consumption is {awnser}",
                input.Count,
                gammaRate, gammaRateDecimal,
                epsilonRate, epsilonRateDecimal,
                gammaRateDecimal * epsilonRateDecimal);
        }

        public static List<string> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath);
        }
    }
}