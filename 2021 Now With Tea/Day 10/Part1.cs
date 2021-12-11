using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_10
{
    //https://adventofcode.com/2021/day/10
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Syntax Scoring. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<string> input)
        {
            double errorScore = 0;

            var scoreTable = new Dictionary<char, int>
            {
                {')',3 },
                {']',57 },
                {'}', 1197 },
                {'>', 25137 },
            };

            var bracketPairs = new Dictionary<char, char>
            {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' },
                { '<', '>' },
            };

            foreach (var line in input)
            {
                var bracketCounter = new Stack<char>();
                foreach (var rune in line)
                {
                    if (bracketPairs.ContainsKey(rune))
                    {
                        bracketCounter.Push(rune);
                    }
                    else if (bracketPairs.ContainsValue(rune))
                    {
                        var topRune = bracketCounter.Peek();
                        if (bracketPairs[topRune] != rune)
                        {
                            //Log.Information("Expected {e}, but found {f} instead",
                            //    bracketPairs[topRune], rune);

                            errorScore += scoreTable[rune];

                            break;
                        }
                        else
                        {
                            _ = bracketCounter.Pop();
                        }
                    }
                    else
                    {
                        Log.Warning("Uknown Character: {rune}", rune);
                    }
                }
            }

            Log.Information("Total syntax error score is: {errorScore}",
                        errorScore);
        }

        public static List<string> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath);
        }
    }
}