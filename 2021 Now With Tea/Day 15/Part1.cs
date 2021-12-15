using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using Advent.AoCLib;

namespace Day_15
{
    //https://adventofcode.com/2021/day/15
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Chiton. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(int[,] input)
        {
            var grid = new GridAStar(input);

            var start = new Location(0, 0);
            var end = new Location(input.GetLength(0) - 1, input.GetLength(1) - 1);

            var path = new AStarSearch(grid, start, end);
            var costToEnd = path.CostSoFar.Last().Value;

            Log.Information("The lowest risk possible to the end is: {costToEnd}", costToEnd);
        }

        public static int[,] ParseInput(string filePath)
        {
            var input = File.ReadAllLines(filePath);

            int x = input[0].Length;
            int y = input.Length;

            var numbers = new int[x, y];

            for (var i = 0; i < x; i++)
            {
                var lineNumbers = input[i].Select(
                    c => int.Parse(c.ToString())).ToArray();

                for (int j = 0; j < y; j++)
                {
                    numbers[i, j] = lineNumbers[j];
                }
            }

            return numbers;
        }
    }

    public class GridAStar : Grid<int>, WeightedGraph<Location>
    {
        public GridAStar(int[,] inputArray) : base(inputArray)
        {
            InternalGrid = new int[inputArray.GetLength(0), inputArray.GetLength(1)];

            AdjacentDirections = new List<(int x, int y)>
            {
                Up, Down, Left, Right
            };

            for (var x = 0; x < this.InternalGrid.GetLength(0); x++)
            {
                for (var y = 0; y < this.InternalGrid.GetLength(1); y++)
                {
                    this[x, y] = inputArray[x, y];
                }
            }
        }

        public double Cost(Location a, Location b)
        {
            return this[b.x, b.y];
        }

        public IEnumerable<Location> Neighbors(Location id)
        {
            return AdjacentCellsEnumerate(id.x, id.y)
                .Select(c => new Location(c.x, c.y));
        }
    }
}