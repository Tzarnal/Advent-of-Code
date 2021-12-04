using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_04
{
    //https://adventofcode.com/2021/day/04#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Giant Squid. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve((int[] BingoNumbers, List<TextGrid> Bingocards) input)
        {
            var bingoCards = input.Bingocards;
            var wonCards = new List<TextGrid>();
            TextGrid lastCard = bingoCards.First();
            int lastNumber = 0;

            foreach (var bingoNumber in input.BingoNumbers)
            {
                foreach (var card in bingoCards)
                {
                    if (wonCards.Contains(card))
                    {
                        continue;
                    }

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
                        wonCards.Add(card);
                        lastCard = card;
                        lastNumber = bingoNumber;
                    }
                }
            }

            var totalCardValue = 0;

            for (var x = 0; x < 5; x++)
            {
                for (var y = 0; y < 5; y++)
                {
                    if (lastCard[x, y] != "*")
                    {
                        totalCardValue += int.Parse(lastCard[x, y]);
                    }
                }
            }

            var score = totalCardValue * lastNumber;

            Log.Information("Earliest winning card won on number {bingoNumber}, total card value {totalCardValue}, score: {score}",
                lastNumber, totalCardValue, score);
            return;
        }
    }
}