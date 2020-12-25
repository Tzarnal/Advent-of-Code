using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent;

namespace Day_11
{
    public class TextGridFerry : TextGrid
    {
        public TextGridFerry(List<string> Data) : base(Data)
        {
        }

        public TextGridFerry(TextGridFerry Grid) : base(Grid)
        {
        }

        public int OccupiedSeats()
        {
            return CountInGrid("#");
        }

        public int AdjacentOccupiedSeatsVector(int x, int y)
        {
            var adjacent = 0;
            foreach (var mod in AdjacentDirections)
            {
                foreach (var cell in CellsAlongPath(x, y, mod).Skip(1))
                {
                    if (cell == "L")
                    {
                        break;
                    }

                    if (cell == "#")
                    {
                        adjacent++;
                        break;
                    }
                }
            }

            return adjacent;
        }
    }
}