using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_13
{
    //https://adventofcode.com/2022/day/13#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Distress Signal. Part Two."; }

        public void Run()
        {
            // testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(Signal Left, Signal Right)> input)
        {
            var signalList = new List<Signal>();
            foreach (var record in input)
            {
                signalList.Add(record.Left);
                signalList.Add(record.Right);
            }

            //Add Divider Packets
            var firstSeperator = new Signal(new Signal(2));
            var secondSeperator = new Signal(new Signal(6));

            signalList.Add(firstSeperator);
            signalList.Add(secondSeperator);

            //Sort List, we already wrote a compartor so just make the lambda wrangle it into the results
            //.Sort needs.
            signalList.Sort((Signal a, Signal b) =>
            {
                var (equal, correct) = Part1.CompareRecords(a, b);
                if (equal)
                {
                    return 0;
                }

                if (correct)
                {
                    return -1;
                }

                return 1;
            });

            //Grab the decoder key
            var decoderKey = signalList.IndexOf(firstSeperator) + 1;
            decoderKey *= (signalList.IndexOf(secondSeperator) + 1);

            Log.Information("The Decoder key is {key}.", decoderKey);
        }
    }
}