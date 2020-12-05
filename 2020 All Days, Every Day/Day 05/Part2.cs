using Advent;
using Serilog;
using System;
using System.Collections.Generic;

namespace Day_05
{
    //https://adventofcode.com/2020/day/5#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Binary Boarding Part Two."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<string> input)
        {
            var seatList = new List<int>();

            foreach (var line in input)
            {
                var seatID2 = ItsBinaryYouIdiot(line);

                seatList.Add(seatID2);
            }

            for (var seat = 0; seat <= 1024; seat++)
            {
                if (!seatList.Contains(seat) && seatList.Contains(seat - 1)
                    && seatList.Contains(seat + 1))
                {
                    Log.Information("Your Seat ID is {seat}", seat);
                    return;
                }
            }

            Log.Information("Couldn't find Seat ID");
        }

        public int ItsBinaryYouIdiot(string input)
        {
            input = input.Replace('F', '0').Replace('L', '0');
            input = input.Replace('B', '1').Replace('R', '1');

            return Convert.ToInt32(input, 2);
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