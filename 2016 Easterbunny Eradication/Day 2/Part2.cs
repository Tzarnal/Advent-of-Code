using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_2
{
    //https://adventofcode.com/2016/day/2#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Bathroom Security. Part Two."; }

        public void Run()
        {
            var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public KeyPadKey BuildKeyPad()
        {
            var oneKey = new KeyPadKey { Value = "1" };
            var twoKey = new KeyPadKey { Value = "2" };
            var threeKey = new KeyPadKey { Value = "3" };
            var fourKey = new KeyPadKey { Value = "4" };
            var fiveKey = new KeyPadKey { Value = "5" };
            var sixKey = new KeyPadKey { Value = "6" };
            var sevenKey = new KeyPadKey { Value = "7" };
            var eightKey = new KeyPadKey { Value = "8" };
            var nineKey = new KeyPadKey { Value = "9" };
            var aKey = new KeyPadKey { Value = "A" };
            var bKey = new KeyPadKey { Value = "B" };
            var cKey = new KeyPadKey { Value = "C" };
            var dKey = new KeyPadKey { Value = "D" };

            oneKey.DownNode = threeKey; threeKey.UpNode = oneKey;

            twoKey.RightNode = threeKey; threeKey.LeftNode = twoKey;
            twoKey.DownNode = sixKey; sixKey.UpNode = twoKey;

            threeKey.RightNode = fourKey; fourKey.LeftNode = threeKey;
            threeKey.DownNode = sevenKey; sevenKey.UpNode = threeKey;

            fourKey.DownNode = eightKey; eightKey.UpNode = fourKey;

            fiveKey.RightNode = sixKey; sixKey.LeftNode = fiveKey;

            sixKey.RightNode = sevenKey; sevenKey.LeftNode = sixKey;
            sixKey.DownNode = aKey; aKey.UpNode = sixKey;

            sevenKey.RightNode = eightKey; eightKey.LeftNode = sevenKey;
            sevenKey.DownNode = bKey; bKey.UpNode = sevenKey;

            eightKey.RightNode = nineKey; nineKey.LeftNode = eightKey;
            eightKey.DownNode = cKey; cKey.UpNode = eightKey;

            aKey.RightNode = bKey; bKey.LeftNode = aKey;

            bKey.RightNode = cKey; cKey.LeftNode = bKey;
            bKey.DownNode = dKey; dKey.UpNode = bKey;

            return fiveKey;
        }

        public void Solve(List<string> input)
        {
            var keyPad = BuildKeyPad();
            var solution = "";

            foreach (var line in input)
            {
                foreach (var c in line)
                {
                    switch (c)
                    {
                        case 'U':
                            keyPad = keyPad.Up();
                            break;

                        case 'D':
                            keyPad = keyPad.Down();
                            break;

                        case 'L':
                            keyPad = keyPad.Left();
                            break;

                        case 'R':
                            keyPad = keyPad.Right();
                            break;
                    }
                }

                solution += keyPad.Value;
            }

            Log.Information("Bathroom keycode: {solution}",
                solution);
        }
    }
}