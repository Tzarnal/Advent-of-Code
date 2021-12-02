using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_02
{
    //https://adventofcode.com/2021/day/02#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Dive!. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(string direction, int amount)> input)
        {
            var depth = 0;
            var horizontalPosition = 0;
            var aim = 0;

            foreach (var command in input)
            {
                switch (command.direction)
                {
                    case "forward":
                        horizontalPosition += command.amount;
                        depth += aim * command.amount;
                        break;

                    case "up":
                        aim -= command.amount;
                        break;

                    case "down":
                        aim += command.amount;
                        break;

                    default:
                        Log.Warning("Uknown command: {command}",
                            command.direction);
                        break;
                }
            }

            var product = depth * horizontalPosition;

            Log.Information("After {count} instructions depth is {depth}, horizontal position is {horizontalPosition}, aim is {aim}, product: {product}",
                input.Count, depth, horizontalPosition, aim, product);
        }
    }
}