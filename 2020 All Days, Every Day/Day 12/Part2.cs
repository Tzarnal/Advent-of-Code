using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_12
{
    //https://adventofcode.com/2020/day/12#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Rain Risk. Part Two."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<(string direction, int value)> input)
        {
            long shipNorthSouth = 0;
            long shipEastWest = 0;

            long waypointNorthSouth = 1;
            long waypointEastWest = 10;

            foreach (var instruction in input)
            {
                switch (instruction.direction)
                {
                    case "F":
                        shipNorthSouth += (waypointNorthSouth * instruction.value);
                        shipEastWest += (waypointEastWest * instruction.value);
                        break;

                    case "N":
                        waypointNorthSouth += instruction.value;
                        break;

                    case "S":
                        waypointNorthSouth -= instruction.value;
                        break;

                    case "E":
                        waypointEastWest += instruction.value;
                        break;

                    case "W":
                        waypointEastWest -= instruction.value;
                        break;

                    case "L":
                        (waypointNorthSouth, waypointEastWest) = Turn(waypointNorthSouth, waypointEastWest, instruction.value * -1);
                        break;

                    case "R":
                        (waypointNorthSouth, waypointEastWest) = Turn(waypointNorthSouth, waypointEastWest, instruction.value);
                        break;

                    default:
                        Log.Warning("Defaulted on : {@instruction}", instruction);
                        break;
                }
            }
            var awnser = Math.Abs(shipNorthSouth) + Math.Abs(shipEastWest);

            Log.Information("After {count} instruction the awnser is {awnser}.", input.Count, awnser);
        }

        private (long, long) Turn(long northSouth, long eastWest, int degrees)
        {
            long newNorthSouth;
            long newEastWest;

            degrees = (360 + degrees) % 360;

            do
            {
                newNorthSouth = eastWest * -1;
                newEastWest = northSouth;

                northSouth = newNorthSouth;
                eastWest = newEastWest;

                degrees -= 90;
            } while (degrees > 0);

            return (northSouth, eastWest);
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