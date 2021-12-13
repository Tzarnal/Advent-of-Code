using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_13
{
    //https://adventofcode.com/2021/day/13
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Transparent Origami. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve((List<(int x, int y)> Points, List<(string direction, int amount)> Folds) input)
        {
            var maxGridX = input.Points.Max(p => p.x) + 1;
            var maxGridY = input.Points.Max(p => p.y) + 1;

            var paper = new Grid<string>(maxGridX, maxGridY, " ");

            foreach (var point in input.Points)
            {
                paper[point.x, point.y] = "#";
            }

            //paper.ConsolePrint();

            Grid<string> foldedPaper;

            foreach (var fold in input.Folds.Take(1))
            {
                int foldX, foldY;
                if (fold.direction == "x")
                {
                    foldX = fold.amount;
                    foldY = paper.InternalGrid.GetLength(1);
                }
                else
                {
                    foldX = paper.InternalGrid.GetLength(0);
                    foldY = fold.amount;
                }

                foldedPaper = new Grid<string>(foldX, foldY, " ");

                foreach (var cell in paper.CellsWithValue("#"))
                {
                    if (fold.direction == "x")
                    {
                        if (cell.x > fold.amount)
                        {
                            int x = fold.amount - (cell.x - fold.amount);
                            foldedPaper[x, cell.y] = cell.value;
                        }
                        else
                        {
                            foldedPaper[cell.x, cell.y] = cell.value;
                        }
                    }
                    else
                    {
                        if (cell.y > fold.amount)
                        {
                            int y = fold.amount - (cell.y - fold.amount);
                            foldedPaper[cell.x, y] = cell.value;
                        }
                        else
                        {
                            foldedPaper[cell.x, cell.y] = cell.value;
                        }
                    }
                }

                paper = foldedPaper;
                //paper.ConsolePrint();
            }

            var count = paper.CountInGrid("#");
            Log.Information("Found {count} '#' on paper after first fold", count);
        }

        public static (List<(int x, int y)> Points, List<(string direction, int amount)> Folds) ParseInput(string filePath)
        {
            var split = File.ReadAllText(filePath).Split("\r\n\r\n");

            var folds = new List<(string direction, int amount)>();
            foreach (var line in split[1].Split(Environment.NewLine))
            {
                var (direction, rule) = line.Extract<(string, int)>(@"fold along (.)=(\d+)");
                folds.Add((direction, rule));
            }

            var points = new List<(int x, int y)>();
            foreach (var line in split[0].Split(Environment.NewLine))
            {
                var (x, y) = line.Extract<(int, int)>(@"(\d+),(\d+)");
                points.Add((x, y));
            }

            return (points, folds);
        }
    }
}