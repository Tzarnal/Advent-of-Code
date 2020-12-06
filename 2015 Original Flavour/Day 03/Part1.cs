using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_03
{
    //https://adventofcode.com/2015/day/3
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Perfectly Spherical Houses in a Vacuum Part One."; }

        public void Run()
        {
            //Solve(ParseInput($"Day {Dayname}/inputTest.txt"));

            Solve(ParseInput($"Day {Dayname}/input.txt"));
        }

        public void Solve(IEnumerable<(int hor, int ver)> data)
        {
            int x = 0, y = 0;
            var presents = new Dictionary<(int, int), int>();

            presents[(x, y)] = presents.GetValueOrDefault((x, y)) + 1;

            foreach (var (hor, ver) in data)
            {
                x += hor;
                y += ver;

                presents[(x, y)] = presents.GetValueOrDefault((x, y)) + 1;
            }

            Log.Information("Visited {presents} houses.", presents.Count);
        }

        private IEnumerable<(int hor, int ver)> ParseInput(string filePath)
        {
            var lookup = new Dictionary<char, (int, int)>
            {
                { '<', (-1, 0) },
                { '>', (1, 0) },
                { '^', (0, 1) },
                { 'v', (0, -1) }
            };

            var text = File.ReadAllText(filePath);

            return text.Select(c => lookup[c]);
        }
    }
}