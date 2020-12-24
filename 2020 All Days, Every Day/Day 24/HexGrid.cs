using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_24
{
    public class HexGrid
    {
        public Dictionary<(int x, int y), bool> Tiles;

        public HexGrid()
        {
            Tiles = new Dictionary<(int X, int Y), bool>();
        }

        public HexGrid(HexGrid original)
        {
            Tiles = new Dictionary<(int X, int Y), bool>(original.Tiles);
        }

        public (int x, int y) RelativeTile((int X, int Y) GridPos, string Direction)
        {
            return RelativeTile(GridPos.X, GridPos.Y, Direction);
        }

        public (int x, int y) RelativeTile(int X, int Y, string Direction)
        {
            switch (Direction)
            {
                case "e":
                    X += 1;
                    break;

                case "se":
                    Y += 1;
                    break;

                case "sw":
                    X -= 1;
                    Y += 1;
                    break;

                case "w":
                    X -= 1;
                    break;

                case "nw":
                    Y -= 1;
                    break;

                case "ne":
                    X += 1;
                    Y -= 1;
                    break;
            }

            return (X, Y);
        }

        public List<(int x, int y)> AdjacentTiles((int X, int Y) GridPos)
        {
            var modifiers = new List<(int x, int y)>
            {
                //e   se    sw     w      nw     ne
                (1,0),(0,1),(-1,1),(-1,0),(0,-1),(1,-1)
            };

            List<(int x, int y)> adjacent = new();
            foreach (var mod in modifiers)
            {
                (int X, int Y) adjacentPos = (GridPos.X + mod.x, GridPos.Y + mod.y);
                adjacent.Add(adjacentPos);
            }
            return adjacent;
        }

        public int AdjacentBlackTiles((int X, int Y) GridPos)
        {
            var adjacent = AdjacentTiles(GridPos);
            var count = 0;

            foreach (var tile in adjacent)
            {
                if (IsBlack(tile))
                {
                    count++;
                }
            }

            return count;
        }

        public bool Flip((int X, int Y) GridPos)
        {
            return Flip(GridPos.X, GridPos.Y);
        }

        public bool Flip(int X, int Y)
        {
            if (Tiles.ContainsKey((X, Y)))
            {
                Tiles[(X, Y)] = !Tiles[(X, Y)];
                return Tiles[(X, Y)];
            }

            Tiles.Add((X, Y), true);
            return Tiles[(X, Y)];
        }

        public bool IsBlack((int X, int Y) GridPos)
        {
            if (Tiles.ContainsKey(GridPos))
            {
                return Tiles[GridPos];
            }

            return false;
        }

        public HashSet<(int X, int Y)> ActiveTiles()
        {
            var activeTiles = new HashSet<(int X, int Y)>();

            foreach (var (tile, _) in Tiles)
            {
                var adjacent = AdjacentTiles(tile);
                activeTiles.UnionWith(adjacent);
                activeTiles.Add(tile);
            }

            return activeTiles;
        }

        public int Count()
        {
            int blackTiles = 0;
            foreach (var tile in Tiles)
            {
                if (tile.Value)
                {
                    blackTiles++;
                }
            }

            return blackTiles;
        }
    }
}