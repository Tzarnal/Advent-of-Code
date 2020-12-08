using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_08
{
    //https://adventofcode.com/2020/day/8#part2
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Handheld Halting. Part One."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<(string operation, int argument)> instructions)
        {
            long accumulator = 0;
            var visitedInstructions = new HashSet<int>();

            var cursor = 0;
            while (!visitedInstructions.Contains(cursor))
            {
                visitedInstructions.Add(cursor);

                var (operation, argument) = instructions[cursor];

                switch (operation)
                {
                    case "nop":
                        cursor++;
                        break;

                    case "acc":
                        accumulator += argument;
                        cursor++;
                        break;

                    case "jmp":
                        cursor += argument;
                        break;
                }
            }

            Log.Information("After executing {visitedInstructions} instructions the accumulator was {accumulator}.", visitedInstructions.Count, accumulator);
        }

        private List<(string operation, int argument)> ParseInput(string filePath)
        {
            var input = Helpers.ReadStringsFile(filePath);
            var instructions = new List<(string operation, int argument)>();
            foreach (var line in input)
            {
                var elements = line.Split(' ');
                var instruction = (elements[0], int.Parse(elements[1]));

                instructions.Add(instruction);
            }

            return instructions;
        }
    }
}