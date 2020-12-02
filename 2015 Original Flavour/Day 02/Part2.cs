using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;
using Serilog;
using Advent;

namespace Day_02
{
    //https://adventofcode.com/2015/day/2#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: I Was Told There Would Be No Math. Part Two."; }

        public void Run()
        {
            var inputList = Helpers.ReadStringsFile($"Day {Dayname}/input.txt");
            var inputBoxes = ParseInput(inputList);

            double runningTotal = 0;
            var totalBoxes = 0;

            foreach (var box in inputBoxes)
            {
                totalBoxes++;

                var sides = new List<float> { box.X, box.Y, box.Z };
                sides.Sort();

                var ribbonLegth = (sides[0] * 2) + (sides[1] * 2);
                var bowRibbonLegth = box.X * box.Y * box.Z;

                runningTotal += ribbonLegth + bowRibbonLegth;
            }

            Log.Information("The total required wrapping paper for {totalBoxes} boxes is {runningTotal}", totalBoxes, runningTotal);
        }

        private List<Vector3> ParseInput(List<string> inputList)
        {
            var vectors = new List<Vector3>();
            foreach (var inputLine in inputList)
            {
                var numbers = inputLine.Split('x');
                var newVector = new Vector3();

                newVector.X = float.Parse(numbers[0]);
                newVector.Y = float.Parse(numbers[1]);
                newVector.Z = float.Parse(numbers[2]);

                vectors.Add(newVector);
            }

            return vectors;
        }
    }
}