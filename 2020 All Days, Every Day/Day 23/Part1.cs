using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_23
{
    //https://adventofcode.com/2020/day/23
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Crab Cups. Part One."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList, 10);
            //Solve(testinputList, 100);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList, 100);
        }

        public void Solve(List<int> input, int moves)
        {
            int currentCup = 0;
            var cups = new List<int>(input);

            for (int i = 1; i <= moves; i++)
            {
                //Starting State
                var cupValue = cups[currentCup];


                List<int> pickupCups = new();

                //Pick up 3 cups
                for (int take = 0; take < 3; take++)
                {
                    var pickupIndex = (currentCup + 1);
                    if (pickupIndex >= cups.Count)
                        pickupIndex = 0;

                    pickupCups.Add(cups[pickupIndex]);
                    cups.RemoveAt(pickupIndex);
                }


                //Determine destination cup
                var destinationValue = cupValue - 1;
                while (!cups.Contains(destinationValue))
                {
                    //Log.Debug("Didn't find {newCupValue}", destinationValue);
                    destinationValue--;
                    if (destinationValue < 0)
                    {
                        destinationValue = input.Count;
                    }
                }

                var destination = cups.IndexOf(destinationValue);


                //Place 3 cups back clockwise of index
                pickupCups.Reverse();
                for (int place = 0; place < 3; place++)
                {
                    var placeDown = (destination + 1) % input.Count;

                    cups.Insert(placeDown, pickupCups[place]);
                }

                //The crab selects a new current cup: the cup which is immediately clockwise of the current cup.
                currentCup = cups.IndexOf(cupValue) + 1;
                currentCup %= cups.Count;
            }


            var startIndex = cups.IndexOf(1);
            var labels = "";
            for (var i = 1; i < 9; i++)
            {
                labels += cups[(startIndex + i) % cups.Count].ToString();
            }

            Log.Information("Labels on the cup are {labels} ", labels);
        }

        private List<int> ParseInput(string filePath)
        {
            return File.ReadAllText(filePath).Select(c => int.Parse(c.ToString())).ToList();
        }
    }
}