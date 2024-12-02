using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_04
{
    //https://adventofcode.com/2023/day/04
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Scratchcards. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<ScratchCard> input)
        {
            var cardScores = new List<double>();

            foreach (var card in input)
            {
                double hits = 0;

                foreach (var number in card.numbers)
                {
                    if (card.winningNumbers.Contains(number))
                    {
                        hits++;
                    }
                }

                if (hits > 0)
                {
                    var score = Math.Pow(2, hits - 1);
                    if (score == 0)
                    {
                        score = 1;
                    }

                    cardScores.Add(score);
                }
            }

            var cardScoreSum = cardScores.Sum();

            Log.Information("Found {count} winning cards with a sum of {sum} points.",
                cardScores.Count, cardScoreSum);
        }

        public static List<ScratchCard> ParseInput(string filePath)
        {
            var cards = new List<ScratchCard>();

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var name = line.Split(':')[0];
                var numberssplit = line.Split(":")[1].Split("|");

                var winningNumbers = Helpers.ReadAllIntsStrings(numberssplit[0]);
                var numbers = Helpers.ReadAllIntsStrings(numberssplit[1]);

                cards.Add(new ScratchCard
                {
                    name = name,
                    winningNumbers = winningNumbers,
                    numbers = numbers,
                });
            }

            return cards;
        }
    }

    public record ScratchCard
    {
        required public string name;
        required public List<int> winningNumbers;
        required public List<int> numbers;
    }
}