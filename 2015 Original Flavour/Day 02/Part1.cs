using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;
using Serilog;
using Advent;

namespace Day_02
{
    //https://adventofcode.com/2015/day/2
    internal class Part1 : IAdventProblem
    {
        private string Dayname = "02";
        public string ProblemName { get => $"Day {Dayname}: I Was Told There Would Be No Math Part One."; }

        public void Run()
        {
            var inputList = Helpers.ReadStringsFile($"Day {Dayname}/input.txt");
            var inputBoxes = ParseInput(inputList);

            double runningTotal = 0;
            var totalBoxes = 0;

            foreach (var box in inputBoxes)
            {
                totalBoxes++;

                var firstSide = (box.X * box.Y);
                var secondSide = (box.Y * box.Z);
                var thirdSide = (box.Z * box.X);
                var slack = Math.Min(Math.Min(firstSide, secondSide), thirdSide);

                runningTotal += (firstSide * 2) + (secondSide * 2) + (thirdSide * 2) + slack;
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