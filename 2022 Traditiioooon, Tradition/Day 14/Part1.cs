using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using Advent.AoCLib;

namespace Day_14
{
    //https://adventofcode.com/2022/day/14
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Regolith Reservoir. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<List<IntVector2>> input)
        {
            var cave = BuildCave(input);
            var sandOrigin = new IntVector2(0, 500);

            var sandFlowing = true;
            while (sandFlowing)
            {
                var sandUnitSettled = false;
                var sandUnit = new IntVector2(sandOrigin);

                while (!sandUnitSettled)
                {
                    //Sand falls out of cave check
                    var unitDown = sandUnit + Directions.DownRight;
                    if (unitDown.X >= cave.InternalGrid.GetLength(0) || unitDown.Y >= cave.InternalGrid.GetLength(1))
                    {
                        sandFlowing = false;
                        break;
                    }
                                        
                    //Sand falling
                    if (cave[sandUnit + Directions.Down] == ".")
                    {
                        sandUnit += Directions.Down;
                    }
                    else if (cave[sandUnit + Directions.DownLeft] == ".")
                    {
                        sandUnit += Directions.DownLeft;
                    }
                    else if (cave[sandUnit + Directions.DownRight] == ".")
                    {
                        sandUnit += Directions.DownRight;
                    }
                    else
                    {
                        sandUnitSettled = true;

                        cave[sandUnit] = "o";
                    }
                }
            }

            var totalSand = cave.CountInGrid("o");

            cave.FileClearPrint("Day 14 Part 1 Cave.txt");
            Log.Information("There are {sand} units of sand at rest in the cave.", totalSand);
        }

        public static Grid<string> BuildCave(List<List<IntVector2>> input)
        {
            var xSize = 1;
            var ySize = 1;

            //Find Size
            foreach (var rockLine in input)
            {
                foreach (var line in rockLine)
                {
                    if (xSize < line.X)
                    {
                        xSize = line.X;
                    }

                    if (ySize < line.Y)
                    {
                        ySize = line.Y;
                    }
                }
            }

            //Create Cave
            var cave = new Grid<string>(xSize + 1, ySize + 1, ".");

            //Draw rock lines
            foreach (var rockLine in input)
            {
                var originPoint = rockLine.First();
                foreach (var destinityPoint in rockLine.Skip(1))
                {
                    var difference = destinityPoint - originPoint;

                    var trajectory = (Math.Sign(difference.X), Math.Sign(difference.Y));
                    var distance = int.Max(Math.Abs(difference.X), Math.Abs(difference.Y));

                    cave.ReplaceValueAlongPath(".", "#", originPoint.X, originPoint.Y, trajectory, distance);

                    originPoint = destinityPoint;
                }
            }

            //Here, finally, we have a cave
            return cave;
        }

        public static List<List<IntVector2>> ParseInput(string filePath)
        {
            var lines = Helpers.ReadStringsFile(filePath);
            var rocks = new List<List<IntVector2>>();

            foreach (var line in lines)
            {
                var rockLine = new List<IntVector2>();
                var chunks = line.Split(" -> ");

                foreach (var chunk in chunks)
                {
                    var coord = chunk.Split(',');
                    var rock = new IntVector2(int.Parse(coord[1]), int.Parse(coord[0]));

                    rockLine.Add(rock);
                }

                rocks.Add(rockLine);
            }

            return rocks;
        }
    }
}