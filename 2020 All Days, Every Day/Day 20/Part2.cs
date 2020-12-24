using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_20
{
    //https://adventofcode.com/2020/day/20#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Jurassic Jigsaw. Part Two."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            //Solve(inputList);
        }

        public void Solve(List<Tile> input)
        {
            
            Log.Information("Unsolved so far.");
        }

        

        private List<Tile> ParseInput(string filePath)
        {
            var output = new List<Tile>();

            var input = File.ReadAllText(filePath);
            var chunks = input.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);

            foreach (var chunk in chunks)
            {
                var bits = chunk.Split(":");

                var tile = new Tile(bits[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList());
                tile.TileID = bits[0];
                tile.TileIDNumber = bits[0].Extract<int>(@"Tile (\d+)");

                output.Add(tile);
            }

            return output;
        }
    }
}