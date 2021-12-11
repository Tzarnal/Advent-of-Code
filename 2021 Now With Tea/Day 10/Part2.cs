using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_10
{
    //https://adventofcode.com/2021/day/10#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Syntax Scoring. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<string> input)
        {
            var scoreTable = new Dictionary<char, int>
            {
                {')', 1 },
                {']', 2 },
                {'}', 3 },
                {'>', 4 },
            };

            var bracketPairs = new Dictionary<char, char>
            {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' },
                { '<', '>' },
            };

            var incompleteStacks = new List<Stack<char>>();

            //Part 1
            foreach (var line in input)
            {
                var valid = true;
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
                            valid = false;
                            break;
                        }
                        else
                        {
                            _ = bracketCounter.Pop();
                        }
                    }
                }

                if (valid)
                {
                    incompleteStacks.Add(bracketCounter);
                }
            }

            var scoresList = new List<long>();

            //Part 2
            foreach (var stack in incompleteStacks)
            {
                var scoreQueue = new Queue<char>();

                while (stack.Count > 0)
                {
                    var topRune = stack.Pop();
                    var matchingRune = bracketPairs[topRune];

                    scoreQueue.Enqueue(matchingRune);
                }

                long lineScore = 0;
                while (scoreQueue.Count > 0)
                {
                    lineScore *= 5;
                    lineScore += scoreTable[scoreQueue.Dequeue()];
                }

                scoresList.Add(lineScore);
            }

            var middleScore = scoresList
                .OrderByDescending(s => s)
                .ToArray()
                [scoresList.Count / 2];

            Log.Information("Total syntax error score is: {score}",
                middleScore);
        }
    }
}