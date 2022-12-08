using Advent;
using Serilog;

namespace Day_8
{
    //https://adventofcode.com/2022/day/8
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Treetop Tree House. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<string> input)
        {
            var gridSize = input.Count;
            var treeGrid = new Grid<int>(gridSize, gridSize, 0);

            var visibleTrees = new List<(int value, int x, int y)>();

            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    treeGrid[x, y] = int.Parse(input[x][y].ToString());
                }
            }

            foreach (var tree in treeGrid.AllCells())
            {
                //skipping 1 because the origin point is part of the cells along a path
                var leftTrees = treeGrid.CellsAlongPath(tree.x, tree.y, Grid<int>.Left).Skip(1);
                var rightTrees = treeGrid.CellsAlongPath(tree.x, tree.y, Grid<int>.Right).Skip(1);
                var upTrees = treeGrid.CellsAlongPath(tree.x, tree.y, Grid<int>.Up).Skip(1);
                var downTrees = treeGrid.CellsAlongPath(tree.x, tree.y, Grid<int>.Down).Skip(1);

                var leftVisible = !leftTrees.Select(t => t >= tree.value).Any(b => b);
                var rightVisible = !rightTrees.Select(t => t >= tree.value).Any(b => b);
                var upVisible = !upTrees.Select(t => t >= tree.value).Any(b => b);
                var downVisible = !downTrees.Select(t => t >= tree.value).Any(b => b);

                if (leftVisible || rightVisible || upVisible || downVisible)
                {
                    visibleTrees.Add(tree);
                }
            }

            //treeGrid.ConsolePrint();

            Log.Information("There are {sum} trees visible from outside the grid.", visibleTrees.Count);
        }

        public static List<string> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath);
        }
    }
}