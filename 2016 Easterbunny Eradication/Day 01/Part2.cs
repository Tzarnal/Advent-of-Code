using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_01
{
    //https://adventofcode.com/2016/day/1
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: No Time for a Taxicab. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(string direction, int amount)> input)
        {
            int direction = 0;

            int northSouth = 0;
            int eastWest = 0;

            List<(int x, int y)> locations = new List<(int x, int y)>();

            foreach (var command in input)
            {
                switch (command.direction)
                {
                    case "R":
                        if (direction == 0)
                        {
                            direction = 1;
                        }
                        else if (direction == 1)
                        {
                            direction = 2;
                        }
                        else if (direction == 2)
                        {
                            direction = 3;
                        }
                        else if (direction == 3)
                        {
                            direction = 0;
                        }
                        break;

                    case "L":
                        if (direction == 0)
                        {
                            direction = 3;
                        }
                        else if (direction == 1)
                        {
                            direction = 0;
                        }
                        else if (direction == 2)
                        {
                            direction = 1;
                        }
                        else if (direction == 3)
                        {
                            direction = 2;
                        }
                        break;
                }

                for (var i = 0; i < command.amount; i++)
                {
                    switch (direction)
                    {
                        case 0: //north
                            northSouth++;
                            break;

                        case 1: //east
                            eastWest++;
                            break;

                        case 2: //south
                            northSouth--;
                            break;

                        case 3: //west
                            eastWest--;
                            break;
                    }

                    var currentLocation = (northSouth, eastWest);

                    if (locations.Contains(currentLocation))
                    {
                        var totalDistance = Math.Abs(eastWest) + Math.Abs(northSouth);

                        Log.Information("Easter Bunny HQ is at {currentLocation} which is {totalDistance} blocks away.",
                            currentLocation, totalDistance);
                        return;
                    }

                    locations.Add(currentLocation);
                }
            }

            Log.Information($"Could not find an Easter Bunny HQ location.");
        }
    }
}