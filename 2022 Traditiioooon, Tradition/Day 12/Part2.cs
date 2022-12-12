using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using Advent.AoCLib;
using RegExtract;

namespace Day_12
{
    //https://adventofcode.com/2022/day/12#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Hill Climbing Algorithm. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<string> input)
        {
            var gridX = input.Count;
            var gridY = input[0].Length;

            var grid = new Grid<string>(gridX, gridY, ".");

            Location endPos = new();

            for (int x = 0; x < gridX; x++)
            {
                for (int y = 0; y < gridY; y++)
                {
                    var point = input[x][y].ToString();
                    if (point == "S")
                    {
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

            var starts = grid.CellsWithValue("a");
            var paths = new List<AStarSearch>();

            foreach (var start in starts)
            {
                var aStarGrid = new GridAStar(grid.InternalGrid);

                var startPos = new Location(start.x, start.y);

                var path = new AStarSearch(aStarGrid, startPos, endPos);

                var endsPoints = path.CostSoFar.Where(p => p.Key.x == endPos.x && p.Key.y == endPos.y);
                if (endsPoints.Any())
                {
                    paths.Add(path);
                }
            }

            var lowestPathCost = paths.Select(p => p.CostSoFar.Last().Value).Min();

            Log.Information("Fewest possible steps to best signal is {cost}.", lowestPathCost);
        }
    }
}