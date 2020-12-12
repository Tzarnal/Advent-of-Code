using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_12
{
    //https://adventofcode.com/2020/day/12
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Rain Risk. Part One."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<(string direction, int value)> input)
        {
            long northSouth = 0;
            long eastWest = 0;
            int facing = 90;

            foreach (var instruction in input)
            {
                switch (instruction.direction)
                {
                    case "N":
                    case "F" when facing == 0:
                        northSouth += instruction.value;
                        break;

                    case "S":
                    case "F" when facing == 180:
                        northSouth -= instruction.value;
                        break;

                    case "E":
                    case "F" when facing == 90:
                        eastWest += instruction.value;
                        break;

                    case "W":
                    case "F" when facing == 270:
                        eastWest -= instruction.value;
                        break;

                    case "L":
                        facing = TurnLeft(facing, instruction.value);
                        break;

                    case "R":
                        facing = TurnRight(facing, instruction.value);
                        break;

                    default:
                        Log.Warning("Defaulted on : {@instruction}, {facing}", instruction, facing);
                        break;
                }

                //Log.Verbose("EastWest {eastWest}, NorthSouth {northSouth}. Facing {facing}.", eastWest, northSouth, facing);
            }
            var awnser = Math.Abs(northSouth) + Math.Abs(eastWest);

            Log.Information("After {count} instruction the awnser is {awnser}.", input.Count, awnser);
        }

        private int TurnLeft(int facing, int degrees)
        {
            return (360 + (facing - degrees)) % 360;
        }

        private int TurnRight(int facing, int degrees)
        {
            return (facing += degrees) % 360;
        }

        private List<(string direction, int value)> ParseInput(string filePath)
        {
            var input = Helpers.ReadStringsFile(filePath);
            var output = new List<(string direction, int value)>();

            foreach (var line in input)
            {
                var (dir, val) = line.Extract<(string, int)>(@"(\w)(\d+)");
                output.Add((dir, val));
            }

            return output;
        }
    }
}