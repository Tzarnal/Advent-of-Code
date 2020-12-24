using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_24
{
    //https://adventofcode.com/2020/day/24
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Lobby Layout. Part One."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<string> input)
        {
            var tileFloor = new HexGrid();

            foreach (var line in input)
            {
                (int x, int y) gridPos = (0, 0);
                int linePos = 0;

                while (linePos + 2 <= line.Length)
                {
                    var twoCharInstruction = line.Substring(linePos, 2);
                    if (twoCharInstruction is "se" or "sw" or "nw" or "ne")
                    {
                        gridPos = tileFloor.RelativeTile(gridPos, twoCharInstruction);
                        //Log.Verbose("{instruction} => {pos}", twoCharInstruction, gridPos);
                        linePos += 2;
                    }
                    else
                    {
                        var instruction = line.Substring(linePos, 1);
                        gridPos = tileFloor.RelativeTile(gridPos, instruction);
                        //Log.Verbose("{instruction} => {pos}", instruction, gridPos);
                        linePos += 1;
                    }
                }

                //possible dangling  single character instruction
                if (linePos + 1 <= line.Length)
                {
                    var instruction = line.Substring(linePos, 1);
                    gridPos = tileFloor.RelativeTile(gridPos, instruction);
                    //Log.Verbose("Dangle {instruction} => {pos}", instruction, gridPos);
                }
                tileFloor.Flip(gridPos);
            }

            Log.Information("After {instructions} instructions there are {count} black tiles."
                , input.Count, tileFloor.Count());
        }

        private List<string> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath);
        }
    }
}