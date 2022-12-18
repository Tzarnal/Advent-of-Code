using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Security.Cryptography.X509Certificates;

namespace Day_17
{
    //https://adventofcode.com/2022/day/17
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Pyroclastic Flow. Part One."; }

        public void Run()
        {
            var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(string input)
        {
            var gridHeight = (2022 * 4) +10;
            var tetrisGrid = new Grid<string>(gridHeight, 7, ".");
            
            
            var rocksFallen = 0;
            var tallestRock = 0;

            var finished = false;
            var needNewRock = true;

            while (!finished)
            {
                //Get a rock if needed
                if (needNewRock)
                {
                    needNewRock = false;
                }

                //Jets push rock

                //Rock falls
                rocksFallen++;

                //Hey whats the tallest rock right now ?
                var rocksPoints = tetrisGrid.CellsWithValue("#");
                if(rocksPoints.Any())
                {
                    tallestRock = gridHeight - tetrisGrid.CellsWithValue("#").Max(c => c.x);
                }


            }

            Log.Information("A Solution Can Be Found.");
        }

        public static string ParseInput(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}