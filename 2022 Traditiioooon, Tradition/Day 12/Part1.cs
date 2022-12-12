using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using Advent.AoCLib;

namespace Day_12
{
    //https://adventofcode.com/2022/day/12
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Hill Climbing Algorithm. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<string> input)
        {
            var gridX = input.Count;
            var gridY = input[0].Length;

            var grid = new Grid<string>(gridX, gridY, ".");

            Location startPos = new();
            Location endPos = new();

            for (int x = 0; x < gridX; x++)
            {
                for (int y = 0; y < gridY; y++)
                {
                    var point = input[x][y].ToString();
                    if (point == "S")
                    {
                        startPos = new(x, y);
                        point = "a";
                    }
                    else if (point == "E")
                    {
                        endPos = new(x, y);
                        point = "z";
                    }

                    grid[x, y] = point;
                }
            }

            var aStarGrid = new GridAStar(grid.InternalGrid);
            var path = new AStarSearch(aStarGrid, startPos, endPos);

            var pathCost = path.CostSoFar.Last().Value;
            Log.Information("Fewest possible steps to best signal is {cost}.", pathCost);
        }

        public static List<string> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath);
        }
    }

    public class GridAStar : Grid<string>, WeightedGraph<Location>
    {
        public GridAStar(string[,] inputArray) : base(inputArray)
        {
            InternalGrid = new string[inputArray.GetLength(0), inputArray.GetLength(1)];

            AdjacentDirections = new List<(int x, int y)>
            {
                Up, Down, Left, Right
            };

            for (var x = 0; x < InternalGrid.GetLength(0); x++)
            {
                for (var y = 0; y < InternalGrid.GetLength(1); y++)
                {
                    this[x, y] = inputArray[x, y];
                }
            }
        }

        public static bool CanReach(string start, string destination)
        {
            var sValue = (int)start[0];
            var dValue = (int)destination[0];

            //Can go down any amount, and go between equal elevation
            if (sValue >= dValue)
            {
                return true;
            }

            //Can go up one step
            if (dValue - sValue <= 1)
            {
                return true;
            }

            return false;
        }

        public double Cost(Location a, Location b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        public IEnumerable<Location> Neighbors(Location id)
        {
            var validNeighbors = new List<Location>();

            foreach (var neighbor in AdjacentCellsEnumerate(id.x, id.y))
            {
                if (CanReach(InternalGrid[id.x, id.y], InternalGrid[neighbor.x, neighbor.y]))
                {
                    validNeighbors.Add(new Location(neighbor.x, neighbor.y));
                }
            }

            return validNeighbors;
        }
    }
}