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
    //https://adventofcode.com/2021/day/15#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Chiton. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(int[,] input)
        {
            var largeGridX = input.GetLength(0) * 5;
            var largeGridY = input.GetLength(1) * 5;

            var largeGrid = new int[largeGridX, largeGridY];

            for (var x = 0; x < largeGrid.GetLength(0); x++)
            {
                for (var y = 0; y < largeGrid.GetLength(1); y++)
                {
                    var smallX = x % input.GetLength(0);
                    var smallY = y % input.GetLength(1);

                    var extraX = x / input.GetLength(0);
                    var extraY = y / input.GetLength(1);

                    var value = input[smallX, smallY] + extraX + extraY;
                    while (value > 9)
                    {
                        value -= 9;
                    }

                    largeGrid[x, y] = value;
                }
            }

            var grid = new GridAStar(largeGrid);
            //grid.ConsolePrint();

            var start = new Location(0, 0);
            var end = new Location(largeGrid.GetLength(0) - 1, largeGrid.GetLength(1) - 1);

            var path = new AStarSearch(grid, start, end);
            var costToEnd = path.CostSoFar.Last().Value;

            Log.Information("The lowest risk possible to the end is: {costToEnd}", costToEnd);
        }
    }
}