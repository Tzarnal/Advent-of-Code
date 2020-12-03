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
            //var testinputList = ParseInput($"Day {Dayname}/input.txt");

            //Solve(testinputList);
            Solve(inputList);
        }

        public void Solve(Slope SlopeData)
        {
            var x = 0;
            var y = 0;

            var treeCount = 0;

            while (x < SlopeData.SlopeData.GetLength(0))
            {
                try
                {
                    if (SlopeData[x, y] == Thing.Tree)
                    {
                        treeCount++;
                    }
                    //Log.Debug("[{x},{y}] is {SlopeData}. Treecount: {treeCount}", x, y, SlopeData[x, y], treeCount);
                }
                catch (Exception)
                {
                    //Exceptions are a control structure now
                    break;
                }

                x += 1;
                y += 3;
            }

            Log.Information("Ended on {x},{y} having encountered {treeCount} trees", x, y, treeCount);
        }

        private Slope ParseInput(string filePath)
        {
            var data = Helpers.ReadStringsFile(filePath);
            var width = data[0].Length;

            var slopeData = new Thing[data.Count, width];

            for (int x = 0; x < data.Count; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    if (data[x][y] == '#')
                    {
                        slopeData[x, y] = Thing.Tree;
                    }
                }
            }

            return new Slope { SlopeData = slopeData };
        }
    }
}