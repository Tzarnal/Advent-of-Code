using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_10
{
    //https://adventofcode.com/2022/day/10#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Cathode-Ray Tube. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(Queue<Instruction> instructions)
        {
            Log.Information("The awnser is displayed below: ");
            Console.WriteLine();
            
            var registerX = 1;
            var cycle = 0;

            string crtLine = "";

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

                //Add to the current line
                var difference = Math.Abs((cycle-1) % 40 - registerX);
                if(difference == 0 || difference == 1)
                {
                    crtLine += "#";
                }
                else
                {
                    crtLine += ".";
                }

                //Handle displaying line
                if(cycle % 40 == 0)
                {
                    Log.Verbose(Helpers.ClearGridString( crtLine));                    
                    crtLine = "";
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
        }
    }
}