using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_04
{
    //https://adventofcode.com/2023/day/04#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Scratchcards. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public double scoreCard(ScratchCard card)
        {
            double hits = 0;

            foreach (var number in card.numbers)
            {
                if (card.winningNumbers.Contains(number))
                {
                    hits++;
                }
            }

            return hits;
        }

        public void Solve(List<ScratchCard> input)
        {
            var totalCards = 0;
            var cardCounts = new Dictionary<int, int>();

            for (int i = 0; i < input.Count; i++)
            {
                cardCounts[i] = 1;
            }

            var cardIndex = 0;

            foreach (var card in input)
            {
                var cardCount = cardCounts[cardIndex];
                totalCards += cardCount;
                var score = scoreCard(card);

                for (int i = 0; i < score; i++)
                {
                    cardCounts[cardIndex + i + 1] += cardCount;
                }

                cardIndex++;
            }

            Log.Information("Ended up with a total of {count} scratchcards.", totalCards);
        }
    }
}