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
    //https://adventofcode.com/2021/day/05
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Hydrothermal Venture. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput, 10);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(Point Start, Point End)> input, int dimension = 1000)
        {
            var grid = new TextGrid(dimension, dimension);

            foreach (var line in input)
            {
                //Vertical Line
                if (line.Start.X == line.End.X)
                {
                    var distance = line.Start.Y - line.End.Y;

                    if (distance > 0)
                    {
                        //Forwards
                        grid.ReplaceValueAlongPath("1", "2", line.Start.Y, line.Start.X, TextGrid.Up, distance);
                        grid.ReplaceValueAlongPath(".", "1", line.Start.Y, line.Start.X, TextGrid.Up, distance);
                    }
                    else
                    {
                        //Backwards
                        grid.ReplaceValueAlongPath("1", "2", line.End.Y, line.End.X, TextGrid.Up, Math.Abs(distance));
                        grid.ReplaceValueAlongPath(".", "1", line.End.Y, line.End.X, TextGrid.Up, Math.Abs(distance));
                    }
                }
                //Vertical line
                else if (line.Start.Y == line.End.Y)
                {
                    var distance = line.Start.X - line.End.X;

                    if (distance > 0)
                    {
                        //Forwards
                        grid.ReplaceValueAlongPath("1", "2", line.Start.Y, line.Start.X, TextGrid.Left, distance);
                        grid.ReplaceValueAlongPath(".", "1", line.Start.Y, line.Start.X, TextGrid.Left, distance);
                    }
                    else
                    {
                        //Backwards
                        grid.ReplaceValueAlongPath("1", "2", line.End.Y, line.End.X, TextGrid.Left, Math.Abs(distance));
                        grid.ReplaceValueAlongPath(".", "1", line.End.Y, line.End.X, TextGrid.Left, Math.Abs(distance));
                    }
                }
            }

            grid.ConsolePrint();

            Log.Information("Two lines overlap in {count} points.",
                grid.CountInGrid("2"));
        }

        public static List<(Point Start, Point End)> ParseInput(string filePath)
        {
            var points = new List<(Point Start, Point End)>();
            foreach (var line in File.ReadAllLines(filePath))
            {
                var (startX, startY, endX, endY) =
                    line.Extract<(int, int, int, int)>(
                        @"(\d+),(\d+) -> (\d+),(\d+)");

                points.Add((
                    new Point(startX, startY),
                    new Point(endX, endY)));
            }

            return points;
        }
    }
}