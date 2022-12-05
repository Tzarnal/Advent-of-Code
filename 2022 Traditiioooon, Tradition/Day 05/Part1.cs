using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_05
{
    //https://adventofcode.com/2022/day/5
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Supply Stacks. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve((List<(int count, int source, int destination)> Moves, Dictionary<int, Stack<string>> Stacks) input)
        {
            var (moves, stacks) = input;
            
            foreach(var move in moves)
            {
                for(var i = move.count; i > 0; i--)
                {
                    stacks[move.destination].Push(stacks[move.source].Pop());
                }
            }

            var tops = "";
            foreach(var stack in stacks)
            {
                tops += stack.Value.Pop();
            }

            Log.Information("The tops of the stacks read {tops}.", tops);
        }

        public static (List<(int count, int source, int destination)> Moves, Dictionary<int, Stack<string>> Stacks) ParseInput(string filePath)
        {
            var split = File.ReadAllText(filePath).Split($"{Environment.NewLine}{Environment.NewLine}");
            
            var crateMoves = new List<(int count, int source, int destination)>();
            var crateStacks = new Dictionary<int,Stack<string>>();

            //Parse moves
            foreach(var line in split[1].Split(Environment.NewLine))
            {
                var (count, source, destination) = line
                    .Extract<(int, int, int)>("move (\\d+) from (\\d+) to (\\d+)");

                crateMoves.Add((count, source, destination));
            }

            //Seperate labels from stacks
            var stacks = split[0].Split(Environment.NewLine);
            var stackLabel = stacks.Last();
            
            //Create the stacks from their labels
            foreach(var label in Helpers.ReadAllIntsStrings(stackLabel))
            {
                crateStacks.Add(label, new Stack<string>() );
            }

            //Grab the stacks, and the tallest stack
            stacks = stacks[..(stacks.Length-1)];
            var maxStack = stacks.Select(s => s.Length).Max();

            //Parse the stacks into actual Stacks
            for(int x=0; x < stacks.Length; x++)
            {
                for (int y = 1; y < maxStack; y += 4)
                {
                    var crate = stacks[x][y].ToString();

                    if (!string.IsNullOrWhiteSpace(crate))
                    {
                        crateStacks[y / 4 + 1].Push(crate);
                    }
                                        
                }
            }

            //Reverse the stacks
            foreach (var label in Helpers.ReadAllIntsStrings(stackLabel))
            {
                crateStacks[label] = crateStacks[label].Invert();
            }


            return (crateMoves, crateStacks);
        }
    }
}