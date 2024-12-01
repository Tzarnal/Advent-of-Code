using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_02
{
    //https://adventofcode.com/2023/day/2#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Cube Conundrum. Part Two."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<List<(string color, int count)>> input)
        {
            var powersLevels = new List<int>();

            foreach (var game in input)
            {
                var colorMaxes = new Dictionary<string, int>();

                foreach (var move in game)
                {
                    if (!colorMaxes.ContainsKey(move.color))
                    {
                        colorMaxes.Add(move.color, move.count);
                    }

                    if (colorMaxes[move.color] < move.count)
                    {
                        colorMaxes[move.color] = move.count;
                    }
                }

                var power = 1;
                foreach (var (_, count) in colorMaxes)
                {
                    power *= count;
                }

                powersLevels.Add(power);
            }

            var powerSum = powersLevels.Sum();

            Log.Information("Across all games the sum of powerlevels is {sum}.", powerSum);
        }

        public static List<List<(string color, int count)>> ParseInput(string filePath)
        {
            var Games = new List<List<(string color, int count)>>();

            foreach (var line in File.ReadAllLines(filePath))
            {
                var GameMoves = new List<(string color, int count)>();

                var gameNr = line.Split(':')[0].Split("Game ")[1];

                var game = new Game
                {
                    Id = gameNr
                };

                var moveSets = line.Split(':')[1].Split(';');
                foreach (var moveset in moveSets)
                {
                    var moves = moveset.Split(',');
                    foreach (var move in moves)
                    {
                        var moveElements = move.Trim().Split(' ');
                        var count = int.Parse(moveElements[0]);
                        var color = moveElements[1];

                        GameMoves.Add((color, count));
                    }
                }

                Games.Add(GameMoves);
            }

            return Games;
        }
    }
}