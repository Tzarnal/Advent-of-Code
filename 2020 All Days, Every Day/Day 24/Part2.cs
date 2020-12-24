using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_24
{
    //https://adventofcode.com/2020/day/24#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Lobby Layout. Part Two."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<string> input)
        {
            var tileFloor = InputToFloor(input);

            for (int i = 0; i < 100; i++)
            {
                var referenceFloor = new HexGrid(tileFloor);

                foreach (var tile in referenceFloor.ActiveTiles())
                {
                    var adjacentBlack = referenceFloor.AdjacentBlackTiles(tile);
                    var isBlack = referenceFloor.IsBlack(tile);

                    if (isBlack && (adjacentBlack == 0 || adjacentBlack > 2))
                    {
                        tileFloor.Flip(tile);
                    }
                    else if (!isBlack && adjacentBlack == 2)
                    {
                        tileFloor.Flip(tile);
                    }
                }
            }

            Log.Information("Our final Hurrah for Cellular Automata, Tile Floors this time gives : {count}",
                tileFloor.Count());
        }

        public HexGrid InputToFloor(List<string> input)
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

            return tileFloor;
        }

        private List<string> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath);
        }
    }
}