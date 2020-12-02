using System;
using System.IO;
using System.Collections.Generic;
using Serilog;
using Advent;

namespace Day_01
{
    //Problem URL
    public class Part2 : IAdventProblem
    {
        private string Dayname = "01";
        public string ProblemName { get => $"Day {Dayname}:  Not Quite Lisp. Part Two."; }

        public void Run()
        {
            var inputCharacters = File.ReadAllText($"Day {Dayname}/input.txt");

            int floor = 0;
            var directionCount = 0;

            foreach (char c in inputCharacters)
            {
                directionCount++;
                if (c == '(')
                {
                    floor++;
                }
                else if (c == ')')
                {
                    floor--;
                }

                if (floor < 0)
                {
                    Log.Information("Santa went through {directionCount} directions and ended up on floor {floor}.", directionCount, floor);
                    return;
                }
            }

            Log.Information("Santa went through {directionCount} directions and ended up on floor {floor}.", directionCount, floor);
        }
    }
}