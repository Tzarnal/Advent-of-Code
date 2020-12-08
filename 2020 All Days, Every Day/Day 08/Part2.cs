using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_08
{
    //https://adventofcode.com/2020/day/8#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Handheld Halting. Part Two."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<(string operation, int argument)> instructions)
        {
            (_, _, _, var visitedInstructions) = FindInfiniteLoop(instructions);
            var failedAttempts = 0;

            foreach (var instructionIndex in visitedInstructions)
            {
                if (instructions[instructionIndex].operation == "acc")
                {
                    continue;
                }

                var newInstructions = new List<(string operation, int argument)>(instructions)
                {
                    [instructionIndex] = (instructions[instructionIndex].operation ==
                    "jmp" ? "nop" : "jmp", instructions[instructionIndex].argument)
                };

                (var cursor, var infiniteLoop, var accumulator, _) = FindInfiniteLoop(newInstructions);

                if (!infiniteLoop)
                {
                    Log.Information("After {failedAttempts} attempts. Changed {@instruction} to {@newInstructions}. Accumulator was {accumulator}.",
                        failedAttempts, instructions[instructionIndex], newInstructions[instructionIndex], accumulator);
                    return;
                }

                failedAttempts++;
            }

            Log.Information("Could not find an awnser.");
        }

        public (int cursor, bool infiniteLoop, long accumulator, List<int> visitedInstructions) FindInfiniteLoop(List<(string operation, int argument)> instructions)
        {
            long accumulator = 0;
            var visitedInstructions = new List<int>();

            var cursor = 0;
            while (cursor < instructions.Count)
            {
                if (visitedInstructions.Contains(cursor))
                {
                    return (visitedInstructions.Last(), true, accumulator, visitedInstructions);
                }

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

            return (cursor, false, accumulator, visitedInstructions);
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