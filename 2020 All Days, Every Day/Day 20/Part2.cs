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
            var (cornerTiles, sideTiles) = GetTileGroups(input);
            var centerTiles = input.Except(cornerTiles).Except(sideTiles).ToList();

            var image = AssembleTiles(centerTiles, cornerTiles, sideTiles, (int)Math.Sqrt(input.Count));

            for (int x = 0; x < image.GetLength(0); x++)
            {
                for (int y = 0; y < image.GetLength(1); y++)
                {
                    image[x, y].ConsolePrint();
                }
            }

            Log.Information("A solution can be found.");
        }

        public Tile[,] AssembleTiles(List<Tile> CenterTiles, List<Tile> CornerTiles, List<Tile> SideTiles, int ImageSize)
        {
            var image = new Tile[ImageSize, ImageSize];

            var startCorner = CornerTiles[0];
            CornerTiles.Remove(startCorner);

            //Rotate a corner untill its right side can match a side tile
            while (SideTiles
                .Where(t => t.PossibleEdges
                    .Contains(startCorner.RightEdgeInt()))
                .Count() != 1)
            {
                startCorner.RotateRight();
            }

            //Ensure the bottom edge is also capable of matching a side tile
            if (SideTiles
                .Where(t => t.PossibleEdges
                    .Contains(startCorner.BottomEdgeInt()))
                .Count() != 1)
            {
                startCorner.FlipVertical();
            }

            //Starting corner, Top Left
            image[0, 0] = startCorner;
            var lastTile = startCorner;

            //Top Edge
            for (int i = 1; i < ImageSize - 1; i++)
            {
                //Find the next tile
                var nextTile = SideTiles.First(t => t.PossibleEdges.Contains(lastTile.RightEdgeInt()));

                //Remove from available
                SideTiles.Remove(nextTile);

                //Rotate correctly
                if (!nextTile.RotateEdgeToLeft(lastTile.RightEdgeInt()))
                {
                    Log.Error("Couldn't rotate Top Edge Tile correctly");
                }

                image[0, i] = nextTile;
                lastTile = nextTile;
            }

            //Top Right Corner
            var topRightCorner = CornerTiles.First(t => t.PossibleEdges
                .Contains(lastTile.RightEdgeInt()));

            CornerTiles.Remove(topRightCorner);

            if (!topRightCorner.RotateEdgeToLeft(lastTile.RightEdgeInt()))
            {
                Log.Error("Couldn't rotate Top Right Corner Tile correctly");
            }

            image[0, ImageSize - 1] = topRightCorner;

            //Left Edge
            lastTile = image[0, 0];
            for (int i = 1; i < ImageSize - 1; i++)
            {
                //Find the next tile
                var nextTile = SideTiles.First(t => t.PossibleEdges.Contains(lastTile.BottomEdgeInt()));

                //Remove from available
                SideTiles.Remove(nextTile);

                //Rotate correctly
                if (!nextTile.RotateEdgeToTop(lastTile.BottomEdgeInt()))
                {
                    Log.Error("Couldn't rotate Left Edge Tile correctly");
                }

                image[i, 0] = nextTile;
                lastTile = nextTile;
            }

            //Bottom Left Corner
            var bottomLeftCorner = CornerTiles.First(t => t.PossibleEdges
                .Contains(lastTile.BottomEdgeInt()));

            CornerTiles.Remove(bottomLeftCorner);

            if (!bottomLeftCorner.RotateEdgeToTop(lastTile.BottomEdgeInt()))
            {
                Log.Error("Couldn't rotate Bottom Left Corner Tile correctly");
            }

            image[ImageSize - 1, 0] = bottomLeftCorner;

            //Right Edge
            lastTile = image[0, ImageSize - 1];
            for (int i = 1; i < ImageSize - 1; i++)
            {
                //Find the next tile
                var nextTile = SideTiles.First(t => t.PossibleEdges.Contains(lastTile.BottomEdgeInt()));

                //Remove from available
                SideTiles.Remove(nextTile);

                //Rotate correctly
                if (!nextTile.RotateEdgeToTop(lastTile.BottomEdgeInt()))
                {
                    Log.Error("Couldn't rotate Right Edge Tile correctly");
                }

                image[i, ImageSize - 1] = nextTile;
                lastTile = nextTile;
            }

            //Bottom Right Corner
            var bottomRightCorner = CornerTiles.First(t => t.PossibleEdges
                .Contains(lastTile.BottomEdgeInt()));

            CornerTiles.Remove(bottomRightCorner);

            if (!bottomRightCorner.RotateEdgeToTop(lastTile.BottomEdgeInt()))
            {
                Log.Error("Couldn't rotate Bottom Right Corner Tile correctly");
            }

            image[ImageSize - 1, ImageSize - 1] = bottomRightCorner;

            //Bottom Edge
            lastTile = image[ImageSize - 1, 0];
            for (int i = 1; i < ImageSize - 1; i++)
            {
                //Find the next tile
                var nextTile = SideTiles.First(t => t.PossibleEdges.Contains(lastTile.RightEdgeInt()));

                //Remove from available
                SideTiles.Remove(nextTile);

                //Rotate correctly
                if (!nextTile.RotateEdgeToLeft(lastTile.RightEdgeInt()))
                {
                    Log.Error("Couldn't rotate Bottom Edge Tile correctly");
                }

                image[ImageSize - 1, i] = nextTile;
                lastTile = nextTile;
            }

            //Center tiles
            for (int x = 1; x < ImageSize - 1; x++)
            {
                for (int y = 1; y < ImageSize - 1; y++)
                {
                    lastTile = image[x, y - 1];

                    //Find the next tile
                    var nextTile = CenterTiles.First(t => t.PossibleEdges.Contains(lastTile.RightEdgeInt()));

                    //Remove from available
                    CenterTiles.Remove(nextTile);

                    //Rotate correctly
                    if (!nextTile.RotateEdgeToLeft(lastTile.RightEdgeInt()))
                    {
                        Log.Error("Couldn't rotate Centre tile correctly");
                    }

                    image[x, y] = nextTile;
                }
            }

            return image;
        }

        public (List<Tile> Corners, List<Tile> Sides) GetTileGroups(List<Tile> input)
        {
            var tileEdges = new Dictionary<int, List<int>>();
            var edgeCount = new Dictionary<int, int>();

            foreach (var tile in input)
            {
                var edges = tile.MinEdges();
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
            var sides = new List<Tile>();

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
                    var corner = input.First(t => t.TileIDNumber == tileId);
                    corners.Add(corner);
                }

                if (count == 3)
                {
                    var side = input.First(t => t.TileIDNumber == tileId);
                    sides.Add(side);
                }
            }

            return (corners, sides);
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