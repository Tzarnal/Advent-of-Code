using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_02
{
    //https://adventofcode.com/2021/day/02
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Dive!. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(string direction, int amount)> input)
        {
            var depth = 0;
            var horizontalPosition = 0;

            foreach (var command in input)
            {
                switch (command.direction)
                {
                    case "forward":
                        horizontalPosition += command.amount;
                        break;

                    case "up":
                        depth -= command.amount;
                        break;

                    case "down":
                        depth += command.amount;
                        break;

                    default:
                        Log.Warning("Uknown command: {command}",
                            command.direction);
                        break;
                }
            }

            var product = depth * horizontalPosition;

            Log.Information("After {count} instructions depth is {depth}, horizontal position is {horizontalPosition}, product: {product}",
                input.Count, depth, horizontalPosition, product);
        }

        public static List<(string direction, int amount)> ParseInput(string filePath)
        {
            var output = new List<(string direction, int amounts)>();

            foreach (var line in File.ReadAllLines(filePath))
            {
                var elements = line.Split(' ');

                var direction = elements[0];
                var amount = int.Parse(elements[1]);

                output.Add((direction, amount));
            }

            return output;
        }
    }
}