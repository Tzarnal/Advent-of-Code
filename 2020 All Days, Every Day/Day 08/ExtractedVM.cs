using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_08
{
    //https://adventofcode.com/2020/day/8#part2
    public class ExtractedVM : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Extrated VM"; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<(string operation, int argument)> instructions)
        {
            var VM = new HGCc(instructions);

            VM.Run();
            var failedAttempts = 0;

            foreach (var instructionIndex in VM.ExecutionRecord)
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

                VM = new HGCc(newInstructions);
                var result = VM.Run();

                if (!result.InfiniteLoopDetected)
                {
                    Log.Information("After {failedAttempts} attempts. Changed {@instruction} to {@newInstructions}. Accumulator was {accumulator}.",
                        failedAttempts, instructions[instructionIndex], newInstructions[instructionIndex], result.Accumulator);
                    return;
                }

                failedAttempts++;
            }

            Log.Information("Could not find an awnser.");
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