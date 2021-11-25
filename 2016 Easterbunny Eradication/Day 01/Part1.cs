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
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: No Time for a Taxicab. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(string direction, int amount)> input)
        {
            double direction = 0;

            double northSouth = 0;
            double eastWest = 0;

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

                switch (direction)
                {
                    case 0: //north
                        northSouth += command.amount;
                        break;

                    case 1: //east
                        eastWest += command.amount;
                        break;

                    case 2: //south
                        northSouth -= command.amount;
                        break;

                    case 3: //west
                        eastWest -= command.amount;
                        break;
                }
            }

            var totalDistance = Math.Abs(eastWest) + Math.Abs(northSouth);

            Log.Information("Easter Bunny HQ is {totalDistance} blocks away."
                , totalDistance);
        }

        public static List<(string, int)> ParseInput(string filePath)
        {
            var inputFile = File.ReadAllText(filePath);

            var commands = new List<(string, int)>();

            foreach (var c in inputFile.Split(", ").ToList())
            {
                commands.Add((
                    c[0].ToString(),
                    int.Parse(c[1..])
                ));
            }

            return commands;
        }
    }
}