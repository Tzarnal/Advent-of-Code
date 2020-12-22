using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_22
{
    //https://adventofcode.com/2020/day/22#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Crab Combat. Part Two."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList.Player1Deck, testinputList.Player2Deck);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList.Player1Deck, inputList.Player2Deck);
        }

        public int _gameCounter = 1;

        //Returns true if player 1 wins
        public bool Solve(List<int> Player1Deck, List<int> Player2Deck, int Game = 1, int callingGame = 0)
        {
            var roundCount = 1;
            while (Player1Deck.Count > 0 && Player2Deck.Count > 0)
            {
                // Before either player deals a card, if there was a previous round in this game that had exactly the same cards in the same order in the same players' decks, the game instantly ends in a win for player 1.
                if (CheckMemory(Game, Player1Deck, Player2Deck))
                {
                    return true;
                }

                //The  round begins with both players drawing the top card of their decks:
                var player1Card = Player1Deck[0];
                Player1Deck.RemoveAt(0);

                var player2Card = Player2Deck[0];
                Player2Deck.RemoveAt(0);

                //Gonna need these
                var Player1Win = false;
                var Player2Win = false;

                //We can recurse
                if (Player1Deck.Count >= player1Card && Player2Deck.Count >= player2Card)
                {
                    _gameCounter++;

                    Player1Win = Solve(Player1Deck.Take(player1Card).ToList(), Player2Deck.Take(player2Card).ToList(), _gameCounter, Game);
                    Player2Win = !Player1Win;
                }

                //Play the round as normal
                if (!Player1Win && !Player2Win)
                {
                    if (player1Card > player2Card)
                    {
                        Player1Win = true;
                    }
                    else if (player1Card < player2Card)
                    {
                        Player2Win = true;
                    }
                }

                //process win / loss
                //Player 1 has the higher card, so both cards move to the bottom of player 1's deck such that 9 is above 5.
                if (Player1Win)
                {
                    Player1Deck.Add(player1Card);
                    Player1Deck.Add(player2Card);
                }
                else if (Player2Win)
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

            if (Player1Deck.Count == 0)
            {
                if (callingGame == 0)
                    Winner(Player2Deck, "player 2", roundCount);
                return false;
            }
            else
            {
                if (callingGame == 0)
                    Winner(Player1Deck, "player 1", roundCount);
                return true;
            }
        }

        public void Winner(List<int> winningDeck, string WinningPlayer, int RoundCount)
        {
            var i = winningDeck.Count;
            var score = winningDeck.Sum(c => c * (i--));

            Log.Information("{winningPlayer} won with a score of {score} after {RoundCount} rounds",
                WinningPlayer, score, RoundCount);
        }

        public HashSet<string> _gameMemory = new();

        public bool CheckMemory(int Game, List<int> Player1Deck, List<int> Player2Deck)
        {
            var player1deckString = string.Join(",", Player1Deck);
            var player2deckString = string.Join(",", Player2Deck);

            var memory = $"{Game}: {player1deckString} || {player2deckString}";

            if (_gameMemory.Contains(memory))
            {
                return true;
            }

            _gameMemory.Add(memory);
            return false;
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