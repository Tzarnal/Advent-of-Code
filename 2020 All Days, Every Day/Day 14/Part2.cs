using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_14
{
    //https://adventofcode.com/2020/day/14#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Docking Data. Part Two."; }

        public void Run()
        {
            //var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<Instruction> instructions)
        {
            var memory = new Dictionary<long, long>();
            string mask = "000000000000000000000000000000000000";

            foreach (var instruction in instructions)
            {
                if (instruction.IsMask)
                {
                    mask = instruction.Mask;
                }
                else
                {
                    var binAdress = Convert.ToString(instruction.Adress, 2);
                    foreach (var adress in ApplyMask(binAdress, mask))
                    {
                        memory[adress] = instruction.Value;
                    }
                }
            }
            long awnser = memory.Sum(m => m.Value);

            Log.Information("After {instructions} instructions. Stored {memory} values in memory. Awnser {awnser}.",
                instructions.Count, memory.Count, awnser);
        }

        private List<long> ApplyMask(string input, string mask)
        {
            string blanks = "000000000000000000000000000000000000";

            if (input.Length != blanks.Length)
            {
                blanks = blanks.Substring(0, blanks.Length - input.Length);
                input = blanks + input;
            }

            var results = Dive(MaskString(input, mask));
            return results.Select(r => Convert.ToInt64(r, 2)).ToList();
        }

        private HashSet<string> Dive(string input)
        {
            var output = new HashSet<string>();

            if (input.Count("X") == 0)
            {
                output.Add(input);
                return output;
            }

            if (input.Count("X") == 1)
            {
                var place = input.IndexOf("X");
                var part1 = input.Substring(0, place);
                var part2 = input.Substring(place + 1);

                output.Add(part1 + "1" + part2);
                output.Add(part1 + "0" + part2);

                return output;
            }

            if (input.Count("X") > 1)
            {
                int place = input.Length / 2;

                foreach (var vPart1 in Dive(input.Substring(0, place)))
                {
                    foreach (var vPart2 in Dive(input.Substring(place)))
                    {
                        output.Add(vPart1 + vPart2);
                    }
                }

                return output;
            }

            return output;
        }

        private string MaskString(string input, string mask)
        {
            var output = "";
            for (var i = 0; i < input.Length; i++)
            {
                if (mask[i] is '1' or 'X')
                {
                    output += mask[i];
                }
                else
                {
                    output += input[i].ToString();
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