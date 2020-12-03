using System;
using System.IO;
using System.Collections.Generic;
using Serilog;
using Advent;
using System.Numerics;

namespace Day_03
{
    //https://adventofcode.com/2020/day/3#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname = "03";
        public string ProblemName { get => $"Day {Dayname}: Toboggan Trajectory. Part Two."; }

        public void Run()
        {
            var inputList = ParseInput($"Day {Dayname}/input.txt");

            Solve(inputList);
        }

        public void Solve(Slope SlopeData)
        {
            var slopes = new List<Vector2>
            {
                new Vector2(1, 1),
                new Vector2(1, 3),
                new Vector2(1, 5),
                new Vector2(1, 7),
                new Vector2(2, 1)
            };

            var slopeResults = new List<int>();
            long total = 1;

            foreach (var slope in slopes)
            {
                var trees = DriveCar(SlopeData, slope);
                slopeResults.Add(trees);
                total *= trees;
            }

            Log.Information("Slope results {@slopeResults}. Awnser is {total}", slopeResults, total);
        }

        private int DriveCar(Slope SlopeData, Vector2 vector)
        {
            var x = 0;
            var y = 0;

            int treeCount = 0;

            while (x < SlopeData.SlopeData.GetLength(0))
            {
                if (SlopeData[x, y] == SlopeThing.Tree)
                {
                    treeCount++;
                }

                x += (int)vector.X;
                y += (int)vector.Y;
            }

            return treeCount;
        }

        private Slope ParseInput(string filePath)
        {
            var data = Helpers.ReadStringsFile(filePath);

            return new Slope(data);
        }
    }
}