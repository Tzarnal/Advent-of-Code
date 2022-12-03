using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Runtime.CompilerServices;
using System.Net.WebSockets;

namespace Day_03
{
    //https://adventofcode.com/2022/day/3
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Rucksack Reorganization. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<string> input)
        {
            int total = 0;
            
            foreach (var line in input)
            {
                var (left, right) = line.CutInHalf();

                foreach(var c in left)
                {
                    if(right.Contains(c))
                    {                        
                        total += ToPriority(c);
                        break;
                    }
                }

            }
            
            Log.Information("The sum of priority is {total}.", total);
        }

        public static int ToPriority(char c)
        {
            var cString = c.ToString();
            var cValue = Convert.ToUInt32(c);
            
            if (cString.IsUpper())
            {
                return (int)cValue - 38;
            }
            else
            {
                return (int)cValue - 96;
            }
        }

        public static List<string> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath);
        }
    }
}