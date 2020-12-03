using System;
using System.IO;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_03
{
    //https://adventofcode.com/2020/day/3
    internal class Part1 : IAdventProblem
    {
        private string Dayname = "03";
        public string ProblemName { get => $"Day {Dayname}: Toboggan Trajectory. Part One."; }

        public void Run()
        {
            var inputList = ParseInput($"Day {Dayname}/input.txt");

            Solve(inputList);
        }

        public void Solve(Slope SlopeData)
        {
            var x = 0;
            var y = 0;

            var treeCount = 0;

            while (x < SlopeData.SlopeData.GetLength(0))
            {
                if (SlopeData[x, y] == SlopeThing.Tree)
                {
                    treeCount++;
                }

                x += 1;
                y += 3;
            }

            Log.Information("Ended on {x},{y} having encountered {treeCount} trees", x, y, treeCount);
        }

        private Slope ParseInput(string filePath)
        {
            var data = Helpers.ReadStringsFile(filePath);

            return new Slope(data);
        }
    }
}