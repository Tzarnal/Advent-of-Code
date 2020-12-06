using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_03
{
    //https://adventofcode.com/2015/day/3#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Perfectly Spherical Houses in a Vacuum Part Two."; }

        public void Run()
        {
            //var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            Solve(ParseInput($"Day {Dayname}/input.txt"));
        }

        public void Solve(IEnumerable<(int hor, int ver)> data)
        {
            (int x, int y) santa = (0, 0);
            (int x, int y) roboSanta = (0, 0);
            var roboSantaToggle = false;

            var presents = new Dictionary<(int, int), int>();

            presents[(0, 0)] = presents.GetValueOrDefault((0, 0)) + 1;

            foreach (var (hor, ver) in data)
            {
                if (!roboSantaToggle)
                {
                    santa.x += hor;
                    santa.y += ver;
                    presents[(santa.x, santa.y)] = presents.GetValueOrDefault((santa.x, santa.y)) + 1;
                }
                else
                {
                    roboSanta.x += hor;
                    roboSanta.y += ver;
                    presents[(roboSanta.x, roboSanta.y)] = presents.GetValueOrDefault((roboSanta.x, roboSanta.y)) + 1;
                }

                roboSantaToggle = !roboSantaToggle;
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