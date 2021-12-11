using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_11
{
    //https://adventofcode.com/2021/day/11#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Dumbo Octopus. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(int[,] input)
        {
            var octoGrid = new Grid<int>(input);
            var flashGrid = new Grid<bool>(new bool[input.GetLength(0), input.GetLength(1)]);

            var i = 0;

            while (true)
            {
                foreach (var octopus in octoGrid.AllCells())
                {
                    octoGrid[octopus.x, octopus.y]++;
                }

                bool flashCheck = true;

                while (flashCheck)
                {
                    flashCheck = false;
                    foreach (var octopus in octoGrid.AllCells())
                    {
                        if (octopus.value > 9 && flashGrid[octopus.x, octopus.y] != true)
                        {
                            flashCheck = true;
                            flashGrid[octopus.x, octopus.y] = true;

                            foreach (var adjacentOctopus in octoGrid.AdjacentCellsEnumerate(octopus.x, octopus.y))
                            {
                                octoGrid[adjacentOctopus.x, adjacentOctopus.y]++;
                            }
                        }
                    }
                }

                if (flashGrid.CountInGrid(false) == 0)
                {
                    break;
                }

                foreach (var flash in flashGrid.CellsWithValue(true))
                {
                    octoGrid[flash.x, flash.y] = 0;
                    flashGrid[flash.x, flash.y] = false;
                }

                i++;
            }

            Log.Information("Final flash step is {i}", i + 1);
        }
    }
}