using Advent;
using Serilog;

namespace Day_08
{
    //https://adventofcode.com/2022/day/8#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Treetop Tree House. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<string> input)
        {
            var gridSize = input.Count;
            var treeGrid = new Grid<int>(gridSize, gridSize, 0);

            var sceneryScores = new List<int>();

            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    treeGrid[x, y] = int.Parse(input[x][y].ToString());
                }
            }

            foreach (var (value, x, y) in treeGrid.AllCells())
            {
                //skipping 1 because the origin point is part of the cells along a path
                var leftTrees = treeGrid.CellsAlongPath(x, y, Grid<int>.Left).Skip(1);
                var rightTrees = treeGrid.CellsAlongPath(x, y, Grid<int>.Right).Skip(1);
                var upTrees = treeGrid.CellsAlongPath(x, y, Grid<int>.Up).Skip(1);
                var downTrees = treeGrid.CellsAlongPath(x, y, Grid<int>.Down).Skip(1);

                var treeLines = new Dictionary<IEnumerable<int>, int>
                {
                    { leftTrees, 0 },
                    { rightTrees, 0 },
                    { upTrees, 0 },
                    { downTrees, 0 }
                };

                foreach (var treeline in treeLines)
                {
                    foreach (var t in treeline.Key)
                    {
                        treeLines[treeline.Key]++;
                        if (t >= value)
                            break;
                    }
                }

                var score = 1;

                foreach (var treeline in treeLines)
                {
                    score *= treeline.Value;
                }

                sceneryScores.Add(score);
            }

            Log.Information("The highest scenery score is {maxScore}.", sceneryScores.Max());
        }
    }
}