using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_22
{
    //https://adventofcode.com/2020/day/22
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Crab Combat. Part One."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList.Player1Deck, testinputList.Player2Deck);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList.Player1Deck, inputList.Player2Deck);
        }

        public int _gameCounter = 1;

        public void Solve(List<int> Player1Deck, List<int> Player2Deck, int Game = 1, int callingGame = 1)
        {
            var roundCount = 1;
            while (Player1Deck.Count > 0 && Player2Deck.Count > 0)
            {
                //The  round begins with both players drawing the top card of their decks:
                var player1Card = Player1Deck[0];
                Player1Deck.RemoveAt(0);

                var player2Card = Player2Deck[0];
                Player2Deck.RemoveAt(0);

                //Player 1 has the higher card, so both cards move to the bottom of player 1's deck such that 9 is above 5.

                if (player1Card > player2Card)
                {
                    Player1Deck.Add(player1Card);
                    Player1Deck.Add(player2Card);
                }
                else if (player1Card < player2Card)
                {
                    Player2Deck.Add(player2Card);
                    Player2Deck.Add(player1Card);
                }
                else if (player1Card == player2Card)
                {
                    Log.Error("DRAW!");
                }

                roundCount++;
            }

            List<int> winningDeck;
            var winningPlayer = "";

            if (Player1Deck.Count == 0)
            {
                winningDeck = Player2Deck;
                winningPlayer = "Player 1";
            }
            else
            {
                winningDeck = Player1Deck;
                winningPlayer = "Player 2";
            }

            var i = winningDeck.Count;
            var score = winningDeck.Sum(c => c * (i--));

            Log.Information("{winningPlayer} won with a score of {score}",
                winningPlayer, score);
        }

        private (List<int> Player1Deck, List<int> Player2Deck) ParseInput(string filePath)
        {
            var input = File.ReadAllText(filePath);
            var chunks = input.Split("Player 2:");

            var player1Lines = chunks[0].Replace("Player 1:", "").Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var player2Lines = chunks[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var player1Values = player1Lines.Select(v => int.Parse(v)).ToList();
            var player2Values = player2Lines.Select(v => int.Parse(v)).ToList();

            return (player1Values, player2Values);
        }
    }
}