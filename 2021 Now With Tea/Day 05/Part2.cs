using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Drawing;

namespace Day_05
{
    //https://adventofcode.com/2021/day/05#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Hydrothermal Venture. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput, 10);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(Point Start, Point End)> input, int dimension = 1000)
        {
            var grid = new TextGrid(dimension, dimension);

            foreach (var line in input)
            {
                var xDiff = line.Start.X - line.End.X;
                var yDiff = line.Start.Y - line.End.Y;

                //Vertical Line
                if (xDiff == 0)
                {
                    if (yDiff > 0)
                    {
                        //Forwards

                        grid.ReplaceValueAlongPath("1", "2", line.Start.Y, line.Start.X, TextGrid.Up, yDiff);
                        grid.ReplaceValueAlongPath(".", "1", line.Start.Y, line.Start.X, TextGrid.Up, yDiff);
                    }
                    else
                    {
                        //Backwards

                        grid.ReplaceValueAlongPath("1", "2", line.End.Y, line.End.X, TextGrid.Up, Math.Abs(yDiff));
                        grid.ReplaceValueAlongPath(".", "1", line.End.Y, line.End.X, TextGrid.Up, Math.Abs(yDiff));
                    }
                }
                //Horizontal line
                else if (yDiff == 0)
                {
                    if (xDiff > 0)
                    {
                        //Forwards

                        grid.ReplaceValueAlongPath("1", "2", line.Start.Y, line.Start.X, TextGrid.Left, xDiff);
                        grid.ReplaceValueAlongPath(".", "1", line.Start.Y, line.Start.X, TextGrid.Left, xDiff);
                    }
                    else
                    {
                        //Backwards

                        grid.ReplaceValueAlongPath("1", "2", line.End.Y, line.End.X, TextGrid.Left, Math.Abs(xDiff));
                        grid.ReplaceValueAlongPath(".", "1", line.End.Y, line.End.X, TextGrid.Left, Math.Abs(xDiff));
                    }
                }
                else if (xDiff > 0)
                {
                    if (yDiff > 0)
                    {
                        grid.ReplaceValueAlongPath("1", "2", line.Start.Y, line.Start.X, TextGrid.UpLeft, yDiff);
                        grid.ReplaceValueAlongPath(".", "1", line.Start.Y, line.Start.X, TextGrid.UpLeft, yDiff);
                    }
                    else
                    {
                        grid.ReplaceValueAlongPath("1", "2", line.End.Y, line.End.X, TextGrid.UpRight, Math.Abs(yDiff));
                        grid.ReplaceValueAlongPath(".", "1", line.End.Y, line.End.X, TextGrid.UpRight, Math.Abs(yDiff));
                    }
                }
                else if (xDiff < 0)
                {
                    if (yDiff > 0)
                    {
                        grid.ReplaceValueAlongPath("1", "2", line.Start.Y, line.Start.X, TextGrid.UpRight, yDiff);
                        grid.ReplaceValueAlongPath(".", "1", line.Start.Y, line.Start.X, TextGrid.UpRight, yDiff);
                    }
                    else
                    {
                        grid.ReplaceValueAlongPath("1", "2", line.End.Y, line.End.X, TextGrid.UpLeft, Math.Abs(yDiff));
                        grid.ReplaceValueAlongPath(".", "1", line.End.Y, line.End.X, TextGrid.UpLeft, Math.Abs(yDiff));
                    }
                }
                else
                {
                    Log.Warning("unrendered line");
                }
            }
            Log.Information("Two lines overlap in {count} points.",
                    grid.CountInGrid("2"));
        }
    }
}