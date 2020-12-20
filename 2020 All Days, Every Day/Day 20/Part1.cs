using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_20
{
    //https://adventofcode.com/2020/day/20
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Jurassic Jigsaw. Part One."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(List<Tile> input)
        {
            var tileEdges = new Dictionary<int, List<int>>();
            var edgeCount = new Dictionary<int, int>();

            foreach (var tile in input)
            {
                var edges = tile.GetEdgesMin();
                tileEdges.Add(tile.TileIDNumber, edges);

                foreach (var edge in edges)
                {
                    if (edgeCount.ContainsKey(edge))
                    {
                        edgeCount[edge]++;
                    }
                    else
                    {
                        edgeCount.Add(edge, 1);
                    }
                }
            }

            var corners = new List<int>();

            foreach (var (tileId, edges) in tileEdges)
            {
                var count = 0;

                foreach (var edge in edges)
                {
                    if (edgeCount[edge] == 2)
                    {
                        count++;
                    }
                }

                if (count == 2)
                {
                    corners.Add(tileId);
                }
            }

            long awnser = 1;
            foreach (var corner in corners)
            {
                awnser *= corner;
            }

            Log.Information("The awnser should be {awnser}", awnser);
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