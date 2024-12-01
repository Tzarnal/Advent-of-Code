using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Runtime.CompilerServices;

namespace Day_02
{
    //https://adventofcode.com/2023/day/2
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Cube Conundrum. Part One."; }

        private Dictionary<string, int> maxCounts = new Dictionary<string, int>
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<Game> input)
        {
            var possibleGameIds = new List<string>();

            foreach (var game in input)
            {
                if (IsValidGame(game))
                {
                    possibleGameIds.Add(game.Id);
                }
            }

            var gameIdSum = possibleGameIds.Sum(id => int.Parse(id));

            Log.Information("There are {count} valid games. Their id's sum to {sum}",
                possibleGameIds.Count,
                gameIdSum);
        }

        public bool IsValidGame(Game game)
        {
            foreach (var move in game.Moves)
            {
                foreach (var kvp in move)
                {
                    if (kvp.Value > maxCounts[kvp.Key])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static List<Game> ParseInput(string filePath)
        {
            var Games = new List<Game>();
            foreach (var line in File.ReadAllLines(filePath))
            {
                var gameNr = line.Split(':')[0].Split("Game ")[1];

                var game = new Game
                {
                    Id = gameNr
                };

                var moveSets = line.Split(':')[1].Split(';');
                foreach (var moveset in moveSets)
                {
                    var moves = moveset.Split(',');
                    var GameMoves = new Dictionary<string, int>();
                    foreach (var move in moves)
                    {
                        var moveElements = move.Trim().Split(' ');
                        var count = int.Parse(moveElements[0]);
                        var color = moveElements[1];

                        GameMoves.Add(color, count);
                    }

                    game.Moves.Add(GameMoves);
                }

                Games.Add(game);
            }

            return Games;
        }
    }

    public class Game
    {
        public string Id { get; set; }
        public List<Dictionary<string, int>> Moves { get; set; }

        public Game()
        {
            Id = "";
            Moves = [];
        }
    }
}