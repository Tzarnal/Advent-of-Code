using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_2
{
    //https://adventofcode.com/2016/day/2
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Bathroom Security. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
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

            oneKey.DownNode = fourKey; fourKey.UpNode = oneKey;
            oneKey.RightNode = twoKey; twoKey.LeftNode = oneKey;

            twoKey.DownNode = fiveKey; fiveKey.UpNode = twoKey;
            twoKey.RightNode = threeKey; threeKey.LeftNode = twoKey;

            threeKey.DownNode = sixKey; sixKey.UpNode = threeKey;

            fourKey.DownNode = sevenKey; sevenKey.UpNode = fourKey;
            fourKey.RightNode = fiveKey; fiveKey.LeftNode = fourKey;

            fiveKey.DownNode = eightKey; eightKey.UpNode = fiveKey;
            fiveKey.RightNode = sixKey; sixKey.LeftNode = fiveKey;

            sixKey.DownNode = nineKey; nineKey.UpNode = sixKey;

            sevenKey.RightNode = eightKey; eightKey.LeftNode = sevenKey;

            eightKey.RightNode = nineKey; nineKey.LeftNode = eightKey;

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

        public static List<string> ParseInput(string filePath)
        {
            return Helpers.ReadStringsFile(filePath);
        }
    }

    public class KeyPadKey
    {
        public KeyPadKey UpNode;
        public KeyPadKey DownNode;
        public KeyPadKey LeftNode;
        public KeyPadKey RightNode;

        public string Value { get; set; }

        public KeyPadKey Up()
        {
            if (UpNode != null)
            {
                return UpNode;
            }

            return this;
        }

        public KeyPadKey Down()
        {
            if (DownNode != null)
            {
                return DownNode;
            }

            return this;
        }

        public KeyPadKey Left()
        {
            if (LeftNode != null)
            {
                return LeftNode;
            }

            return this;
        }

        public KeyPadKey Right()
        {
            if (RightNode != null)
            {
                return RightNode;
            }

            return this;
        }
    }
}