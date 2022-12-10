using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_10
{
    //https://adventofcode.com/2022/day/10
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Cathode-Ray Tube. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(Queue<Instruction> instructions)
        {
            var interestingSignalStrengths = new List<int>();

            var registerX = 1;
            var cycle = 0;

            var measuringPoint = 20;

            Instruction? bufferedInstruction = null;

            while (instructions.Count > 0 || bufferedInstruction != null)
            {
                //Cycle starts
                cycle++;

                if (bufferedInstruction == null)
                {
                    var instruction = instructions.Dequeue();

                    if (instruction.Command != "noop")
                    {
                        bufferedInstruction = instruction;
                        bufferedInstruction.Cycle = 2;
                    }
                }

                if (measuringPoint == cycle)
                {
                    measuringPoint += 40;
                    interestingSignalStrengths.Add(cycle * registerX);
                }

                //Cycle "ends"
                if (bufferedInstruction != null)
                {
                    bufferedInstruction.Cycle--;
                    if (bufferedInstruction.Cycle <= 0)
                    {
                        registerX += bufferedInstruction.Value;
                        bufferedInstruction = null;
                    }
                }
            }

            var sum = interestingSignalStrengths.Sum();
            Log.Information("The sum of interesting signal strenghts is {sum}.", sum);
        }

        public static Queue<Instruction> ParseInput(string filePath)
        {
            var file = File.ReadAllLines(filePath);
            var output = new Queue<Instruction>();

            foreach (var line in file)
            {
                var chunks = line.Split(' ');

                var command = chunks[0];
                var value = 0;

                if (chunks.Length > 1)
                {
                    value = int.Parse(chunks[1]);
                }

                output.Enqueue(new Instruction(command, value));
            }

            return output;
        }
    }

    public class Instruction
    {
        public string Command;
        public int Value;
        public int Cycle;

        public Instruction(string command, int value)
        {
            Command = command;
            Value = value;
        }
    }
}