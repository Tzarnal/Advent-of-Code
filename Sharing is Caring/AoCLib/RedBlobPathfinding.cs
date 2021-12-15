using System;
using System.Collections.Generic;
using System.Text;

namespace Advent.AoCLib
{
    /// <summary>
    /// Sourced from the Excellent Redblob, who tought me a lot about the nuts and bolts of
    /// roguelike implementations and maintains a truely excellent website full of
    /// interactive educational material regarding roguelikes and 2d game mechanics
    ///
    /// https://www.redblobgames.com/pathfinding/a-star/implementation.html#csharp
    /// </summary>

    public interface WeightedGraph<L>
    {
        double Cost(Location a, Location b);

        IEnumerable<Location> Neighbors(Location id);
    }

    public struct Location
    {
        // Implementation notes: I am using the default Equals but it can
        // be slow. You'll probably want to override both Equals and
        // GetHashCode in a real project.

        public readonly int x, y;

        public Location(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    /* NOTE about types: in the main article, in the Python code I just
     * use numbers for costs, heuristics, and priorities. In the C++ code
     * I use a typedef for this, because you might want int or double or
     * another type. In this C# code I use double for costs, heuristics,
     * and priorities. You can use an int if you know your values are
     * always integers, and you can use a smaller size number if you know
     * the values are always small. */

    public class AStarSearch
    {
        public Dictionary<Location, Location> CameFrom
            = new Dictionary<Location, Location>();

        public Dictionary<Location, double> CostSoFar
            = new Dictionary<Location, double>();

        // Note: a generic version of A* would abstract over Location and
        // also Heuristic
        public static double Heuristic(Location a, Location b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        public AStarSearch(WeightedGraph<Location> graph, Location start, Location goal)
        {
            var frontier = new PriorityQueue<Location>();
            frontier.Enqueue(start, 0);

            CameFrom[start] = start;
            CostSoFar[start] = 0;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                if (current.Equals(goal))
                {
                    break;
                }

                foreach (var next in graph.Neighbors(current))
                {
                    double newCost = CostSoFar[current]
                        + graph.Cost(current, next);
                    if (!CostSoFar.ContainsKey(next)
                        || newCost < CostSoFar[next])
                    {
                        CostSoFar[next] = newCost;
                        double priority = newCost + Heuristic(next, goal);
                        frontier.Enqueue(next, priority);
                        CameFrom[next] = current;
                    }
                }
            }
        }
    }

    public class PriorityQueue<T>
    {
        // I'm using an unsorted array for this example, but ideally this
        // would be a binary heap. There's an open issue for adding a binary
        // heap to the standard C# library: https://github.com/dotnet/corefx/issues/574
        //
        // Until then, find a binary heap class:
        // * https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp
        // * http://visualstudiomagazine.com/articles/2012/11/01/priority-queues-with-c.aspx
        // * http://xfleury.github.io/graphsearch.html
        // * http://stackoverflow.com/questions/102398/priority-queue-in-net

        private List<Tuple<T, double>> elements = new List<Tuple<T, double>>();

        public int Count
        {
            get { return elements.Count; }
        }

        public void Enqueue(T item, double priority)
        {
            elements.Add(Tuple.Create(item, priority));
        }

        public T Dequeue()
        {
            int bestIndex = 0;

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Item2 < elements[bestIndex].Item2)
                {
                    bestIndex = i;
                }
            }

            T bestItem = elements[bestIndex].Item1;
            elements.RemoveAt(bestIndex);
            return bestItem;
        }
    }
}