using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_06
{
    //https://adventofcode.com/2015/day/6
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Probably a Fire Hazard. Part One."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<Command> Commands)
        {
            var lights = new bool[1000, 1000];
            var lightChanges = 0;

            foreach (var command in Commands)
            {
                for (int x = command.Start.x; x <= command.End.x; x++)
                {
                    for (int y = command.Start.y; y <= command.End.y; y++)
                    {
                        lightChanges++;
                        switch (command.Operation)
                        {
                            case "toggle":
                                lights[x, y] = !lights[x, y];
                                break;

                            case "on":
                                lights[x, y] = true;
                                break;

                            case "off":
                                lights[x, y] = false;
                                break;
                        }
                    }
                }
            }

            var count = 0;
            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y < 1000; y++)
                {
                    if (lights[x, y]) count++;
                }
            }

            Log.Information("Changed lights {lightChanges} times. Lights lit: {count}", lightChanges, count);
        }

        private List<Command> ParseInput(string filePath)
        {
            var commands = new List<Command>();

            foreach (var line in Helpers.ReadStringsFile(filePath))
            {
                var chunks = line.Replace("turn ", "").Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var command = new Command
                {
                    Operation = chunks[0],
                    Start = ParseCoords(chunks[1]),
                    End = ParseCoords(chunks[3])
                };

                commands.Add(command);
            }

            return commands;
        }

        private (int x, int y) ParseCoords(string input)
        {
            var inputChunks = input.Split(',');
            return (int.Parse(inputChunks[0]), int.Parse(inputChunks[1]));
        }
    }
}