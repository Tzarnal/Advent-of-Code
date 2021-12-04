using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_04
{
    //https://adventofcode.com/2021/day/04
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Giant Squid. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve((int[] BingoNumbers, List<TextGrid> Bingocards) input)
        {
            var bingoCards = input.Bingocards;

            foreach (var bingoNumber in input.BingoNumbers)
            {
                foreach (var card in bingoCards)
                {
                    card.ReplaceAllofValue(bingoNumber.ToString(), "*");
                    var bingoLines = new List<string>();

                    for (var i = 0; i < 5; i++)
                    {
                        bingoLines.Add(
                            string.Concat(
                                card.CellsAlongPath(i, 0, TextGrid.Right)
                                )
                            );

                        bingoLines.Add(
                            string.Concat(
                                card.CellsAlongPath(0, i, TextGrid.Down)
                            )
                        );
                    }

                    if (bingoLines.Contains("*****"))
                    {
                        var totalCardValue = 0;

                        for (var x = 0; x < 5; x++)
                        {
                            for (var y = 0; y < 5; y++)
                            {
                                if (card[x, y] != "*")
                                {
                                    totalCardValue += int.Parse(card[x, y]);
                                }
                            }
                        }

                        var score = totalCardValue * bingoNumber;

                        Log.Information("Earliest winning card won on number {bingoNumber}, total card value {totalCardValue}, score: {score}",
                            bingoNumber, totalCardValue, score);
                        return;
                    }
                }
            }
        }

        public static (int[] BingoNumbers, List<TextGrid> Bingocards) ParseInput(string filePath)
        {
            var allText = File.ReadAllText(filePath);
            var splitText = allText.Split("\r\n\r\n");

            var bingoNumber = splitText[0].Split(',').Select(c => int.Parse(c)).ToArray();
            var bingoCards = new List<TextGrid>();

            foreach (var bingo in splitText.Skip(1))
            {
                var bingoList = bingo.Replace("  ", " ").Split("\r\n").Select(b => b.Trim()).ToList();

                var bingoGrid = new TextGrid(bingoList, " ");

                bingoCards.Add(bingoGrid);
            }

            return (bingoNumber, bingoCards);
        }
    }
}