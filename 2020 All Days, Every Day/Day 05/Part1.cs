using System;
using System.IO;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_05
{
    //https://adventofcode.com/2020/day/5
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Binary Boarding. Part One."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<string> input)
        {
            int maxSeatID = -1;
            int count = 0;

            foreach (var line in input)
            {
                var row = BinarySearch(line.Substring(0, 7), 127);
                var column = BinarySearch(line.Substring(7), 7);

                var seatID = (row * 8) + column;

                if (seatID > maxSeatID)
                    maxSeatID = seatID;

                count++;
            }

            Log.Information("Counted {count} passes. Highest seat ID: {maxSeatID}", count, maxSeatID);
        }

        public int BinarySearch(string input, int max)
        {
            var min = 0;

            foreach (var c in input)
            {
                if (c == 'F' || c == 'L')
                {
                    max = max - ((max - min) / 2);
                }
                else if (c == 'B' || c == 'R')
                {
                    min = max - ((max - min) / 2);
                }
            }

            return min;
        }

        private List<string> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath);
        }
    }
}