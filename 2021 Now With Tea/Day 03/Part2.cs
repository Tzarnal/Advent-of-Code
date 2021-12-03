using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_03
{
    //https://adventofcode.com/2021/day/03#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Binary Diagnostic. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<string> input)
        {
            var numberLength = input.First().Length;

            var oxygenCandidates = input.Where(x => x[0] == GammaBit(input, 0)).ToList();

            for (var i = 1; i < numberLength; i++)
            {
                if (oxygenCandidates.Count() == 1)
                {
                    break;
                }

                var mostCommonBit = GammaBit(oxygenCandidates, i);
                oxygenCandidates = oxygenCandidates.Where(o => o[i] == mostCommonBit).ToList();
            }

            var co2Candidates = input.Where(x => x[0] == EpsilonBit(input, 0)).ToList();

            for (var i = 1; i < numberLength; i++)
            {
                if (co2Candidates.Count() == 1)
                {
                    break;
                }

                var mostCommonBit = EpsilonBit(co2Candidates, i);
                co2Candidates = co2Candidates.Where(o => o[i] == mostCommonBit).ToList();
            }

            var oxygenNumber = oxygenCandidates[0];
            var oxygenNumberDecimal = Convert.ToInt64(oxygenNumber, 2);

            var co2Number = co2Candidates[0];
            var co2NumberDecimal = Convert.ToInt64(co2Number, 2);

            Log.Information("After {input} of numbers the Oxygen Generator Rating is {oxygenNumber}[{oxygenNumberDecimal}] and the CO2 Scrubber Rating {co2Number}[{co2NumberDecimal}]. The life support rating is {awnser}",
                 input.Count,
                 oxygenNumber, oxygenNumberDecimal,
                 co2Number, co2NumberDecimal,
                 oxygenNumberDecimal * co2NumberDecimal);
        }

        public char GammaBit(List<string> input, int pos)
        {
            var oneCounts = 0;
            var zeroCounts = 0;

            foreach (var binaryNumber in input)
            {
                if (binaryNumber[pos] == '1')
                {
                    oneCounts++;
                }
                else
                {
                    zeroCounts++;
                }
            }

            if (oneCounts >= zeroCounts)
            {
                return '1';
            }

            return '0';
        }

        public char EpsilonBit(List<string> input, int pos)
        {
            var oneCounts = 0;
            var zeroCounts = 0;

            foreach (var binaryNumber in input)
            {
                if (binaryNumber[pos] == '1')
                {
                    oneCounts++;
                }
                else
                {
                    zeroCounts++;
                }
            }

            if (zeroCounts <= oneCounts)
            {
                return '0';
            }

            return '1';
        }
    }
}