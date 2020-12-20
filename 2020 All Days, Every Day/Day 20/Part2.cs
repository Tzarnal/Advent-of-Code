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
            var corners = FindCorners(input);
            var imageSize = (int)Math.Sqrt(input.Count);

            var success = false;
            Tile[,] image;

            do
            {
                (success, image) = TryGetImage(input, corners[0], imageSize);
                corners[0].Rotate();
            } while (!success);

            Log.Information("The awnser can be found");
        }

        public (bool Success, Tile[,] Image) TryGetImage(List<Tile> input, Tile Start, int ImageSize)
        {
            Dictionary<Tile, List<int>> tilesWithEdges = new();

            foreach (var Tile in input)
            {
                //Don't try to match on the starting tile
                if (Tile.Equals(Start))
                {
                    continue;
                }
                tilesWithEdges.Add(Tile, Tile.GetPotentialEdges());
            }

            var image = new Tile[ImageSize, ImageSize];
            image[0, 0] = Start;

            for (var x = 0; x < image.GetLength(0); x++)
            {
                for (var y = 1; x < image.GetLength(0); y++)
                {
                    foreach (var (tile, edges) in tilesWithEdges)
                    {
                        if (x == 0 && y == 1)
                        {
                            var origRightEdge = image[0, 0].GetEdges().Right;
                            if (edges.Contains(origRightEdge))
                            {
                                if (!tile.RotateEdgeLeft(origRightEdge)) return (false, image);

                                image[x, y] = tile;
                            }
                        }
                        else if (y == 0)
                        {
                            var bottomEdge = image[x - 1, y].GetEdges().Bottom;
                            if (edges.Contains(bottomEdge))
                            {
                                if (!tile.RotateEdgeUp(bottomEdge)) return (false, image);

                                image[x, y] = tile;
                            }
                        }
                        else if (x == 0)
                        {
                            var rightEdge = image[x, y - 1].GetEdges().Right;
                            if (edges.Contains(rightEdge))
                            {
                                if (!tile.RotateEdgeLeft(rightEdge)) return (false, image);

                                image[x, y] = tile;
                            }
                        }

                        return (false, image);
                    }
                }
            }

            return (true, image);
        }

        public List<Tile> FindCorners(List<Tile> input)
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

            var corners = new List<Tile>();

            foreach (var (tileId, edges) in tileEdges)
            {
                var count = 0;
                var unUsedEdge = 0;

                foreach (var edge in edges)
                {
                    if (edgeCount[edge] == 2)
                    {
                        count++;
                    }
                    else if (edgeCount[edge] == 1)
                    {
                        unUsedEdge = edge;
                    }
                }

                if (count == 2)
                {
                    corners.Add(input.First(t => t.TileIDNumber == tileId));
                }
            }

            return corners;
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