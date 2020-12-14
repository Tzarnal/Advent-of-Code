using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_14
{
    //https://adventofcode.com/2020/day/14
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Docking Data. Part One."; }

        public void Run()
        {
            //var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<Instruction> instructions)
        {
            var memory = new Dictionary<int, long>();
            var mask = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

            foreach (var instruction in instructions)
            {
                if (instruction.IsMask)
                {
                    mask = instruction.Mask;
                }
                else
                {
                    var binInput = Convert.ToString(instruction.Value, 2);
                    binInput = ApplyMask(binInput, mask);

                    memory[instruction.Adress] = Convert.ToInt64(binInput, 2);
                }
            }
            long awnser = memory.Sum(m => m.Value);

            Log.Information("After {instructions} instructions. Stored {memory} values in memory. Awnser {awnser}.",
                instructions.Count, memory.Count, awnser);
        }

        private string ApplyMask(string input, string mask)
        {
            string blanks = "000000000000000000000000000000000000";
            var output = "";

            if (input.Length != blanks.Length)
            {
                blanks = blanks.Substring(0, blanks.Length - input.Length);
                input = blanks + input;
            }

            for (var i = 0; i < mask.Length; i++)
            {
                if (mask[i] != 'X')
                {
                    output += mask[i];
                }
                else
                {
                    output += input[i];
                }
            }

            return output;
        }

        private List<Instruction> ParseInput(string filePath)
        {
            var input = Helpers.ReadStringsFile(filePath);

            var output = new List<Instruction>();

            foreach (var line in input)
            {
                Instruction data;

                if (line.StartsWith("mask"))
                {
                    var mask = line.Extract<string>(@"mask = (.+)");
                    data = new Instruction { IsMask = true, Mask = mask };
                }
                else
                {
                    var (adress, value) = line.Extract<(int, int)>(@"mem\[(\d+)\] = (\d+)");
                    data = new Instruction { IsMask = false, Adress = adress, Value = value };
                }

                output.Add(data);
            }

            return output;
        }
    }
}

public record Instruction
{
    public bool IsMask;
    public string Mask;
    public int Adress;
    public int Value;
}