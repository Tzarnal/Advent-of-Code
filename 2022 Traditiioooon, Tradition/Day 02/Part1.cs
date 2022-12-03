using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_02
{
    //https://adventofcode.com/2022/day/2
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Rock Paper Scissors. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        

        public void Solve(List<(string pick, string counter)> strategy)
        {
            var translate = new Dictionary<string, string>
            {
                { "X","A" }, //Rock
                { "Y","B" }, //Paper 
                { "Z","C" }, //Sciscors
            };
            
            var scores = new Dictionary<string, int>
            {
                { "A", 1 },
                { "B", 2 },
                { "C", 3 },
            };

            var eval = new Dictionary<string, int>
            {
                {"AA", 3 },
                {"BB", 3 },
                {"CC", 3 },

                {"AB", 0 },
                {"AC", 6 },

                {"BA", 6 },
                {"BC", 0 },
 
                {"CA", 0 },
                {"CB", 6 },
            };

            var totalScore = 0;

            foreach(var pair in strategy ) 
            {
                var pick = pair.pick;
                var counter = translate[pair.counter];
                
                var play = $"{counter}{pick}";
                var playScore = eval[play];

                totalScore += playScore + scores[counter];
            }

            Log.Information("Total score according to strategy guide is {totalScore}.", totalScore);
        }

        public static List<(string pick, string counter)> ParseInput(string filePath)
        {
            var strategy = new List<(string pick, string counter)>();
            
            var lines = Helpers.ReadStringsFile(filePath);
            foreach(var line in lines)
            {
                var hands = line.Split(' ');

                strategy.Add((hands[0], hands[1]));
            }

            return strategy;
        }
    }
}
