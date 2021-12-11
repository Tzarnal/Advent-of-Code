using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_11
{
    //https://adventofcode.com/2021/day/11
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Dumbo Octopus. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);
            //Solve(testinput, 100);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input, 100);
        }

        public void Solve(int[,] input, int steps = 10)
        {
            var octoGrid = new Grid<int>(input);
            var flashGrid = new Grid<bool>(new bool[input.GetLength(0), input.GetLength(1)]);

            //Log.Debug("Initial State");
            //octoGrid.ConsolePrint();

            var totalFlashes = 0;

            for (int i = 0; i < steps; i++)
            {
                //First, the energy level of each octopus increases by 1.
                foreach (var octopus in octoGrid.AllCells())
                {
                    octoGrid[octopus.x, octopus.y]++;
                }

                //Log.Debug("After Increments");
                //octoGrid.ConsolePrint();

                //Then, any octopus with an energy level greater than 9 flashes.
                //This increases the energy level of all adjacent octopuses by 1, including octopuses that are diagonally adjacent.
                //If this causes an octopus to have an energy level greater than 9, it also flashes.
                //This process continues as long as new octopuses keep having their energy level increased beyond 9.
                //(An octopus can only flash at most once per step.)

                bool flashCheck = true;

                while (flashCheck)
                {
                    flashCheck = false;
                    foreach (var octopus in octoGrid.AllCells())
                    {
                        //Then, any octopus with an energy level greater than 9 flashes.
                        //(An octopus can only flash at most once per step.)
                        if (octopus.value > 9 && flashGrid[octopus.x, octopus.y] != true)
                        {
                            flashCheck = true;
                            totalFlashes++;

                            flashGrid[octopus.x, octopus.y] = true;

                            foreach (var adjacentOctopus in octoGrid.AdjacentCellsEnumerate(octopus.x, octopus.y))
                            {
                                octoGrid[adjacentOctopus.x, adjacentOctopus.y]++;
                            }
                        }
                    }
                }

                //Finally, any octopus that flashed during this step has
                //its energy level set to 0, as it used all of its energy to flash.
                foreach (var flash in flashGrid.CellsWithValue(true))
                {
                    octoGrid[flash.x, flash.y] = 0;
                    flashGrid[flash.x, flash.y] = false;
                }

                //Log.Debug("After step {i}", i + 1);
                //octoGrid.ConsolePrint();
            }

            Log.Information("Total flashes {totalFlashes}", totalFlashes);
        }

        public static int[,] ParseInput(string filePath)
        {
            var input = File.ReadAllLines(filePath);

            int x = input[0].Length;
            int y = input.Length;

            var numbers = new int[x, y];

            for (var i = 0; i < x; i++)
            {
                var lineNumbers = input[i].Select(c => int.Parse(c.ToString())).ToArray();
                for (int j = 0; j < y; j++)
                {
                    numbers[i, j] = lineNumbers[j];
                }
            }

            return numbers;
        }
    }
}