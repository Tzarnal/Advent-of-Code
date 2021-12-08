using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_08
{
    //https://adventofcode.com/2021/day/08#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Seven Segment Search. Part Two."; }

        public void Run()
        {
            var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(List<string> Digits, List<string> Output)> input)
        {
            var total = 0;

            foreach (var display in input)
            {
                var Translation = new Dictionary<int, string>();

                var digits = display.Digits.OrderBy(d => d.Length).ToArray();

                //The regardless of how things are wired up 1, 4, 7 and 8 are always
                //2,3,4,7 segments which are always in the same array positions
                //since its sorted by segment count
                Translation[1] = digits[0];  // 1
                Translation[4] = digits[2];  // 4
                Translation[7] = digits[1];  // 7
                Translation[8] = digits[9];  // 8

                //Collect the 5 and 6 segment piles
                var fiveSegmentPile = digits.Where(d => d.Length == 5);
                var sixSegmentPile = digits.Where(d => d.Length == 6);

                //3 is 5 segments, and contains both of 1's segments
                var three = digits
                    .Where(d => d.Length == 5 && d.Contains(Translation[1][0]) && d.Contains(Translation[1][1]));
                Translation[3] = three.First();

                fiveSegmentPile = fiveSegmentPile.Where(d => d != Translation[3]);

                //6 is 5 segments, and has only one segment in common with 1
                var six = digits
                    .Where(d => d.Length == 6)
                    .Where(d => (d.Contains(Translation[1][0]) && d.Contains(Translation[1][1])) == false);
                Translation[6] = six.First();

                sixSegmentPile = sixSegmentPile.Where(d => d != Translation[6]);

                //F segment is the only segment in one contained in 6
                var fSegment = Translation[1].Where(d => Translation[6].Contains(d)).First();

                //C segment is the non F segment in 1
                var cSegment = Translation[1].Replace(fSegment.ToString(), "");

                // 5 is 5 segments with the F segment or without the C segment
                var five = fiveSegmentPile.Where(d => d.Contains(fSegment)).First();
                Translation[5] = five;

                // 2 is 5 segments without F, or with the C segment, or the last remaining in the five segment pile
                var two = fiveSegmentPile.Where(d => d.Contains(cSegment)).First();
                Translation[2] = two;

                //0: Of the remaining six segment digits 4 has all of them in common with 9, but 1 is missing in 0
                var zero = "";
                foreach (var c in Translation[4])
                {
                    foreach (var digit in sixSegmentPile)
                    {
                        if (!digit.Contains(c))
                        {
                            zero = digit;
                            Translation[0] = zero;
                            sixSegmentPile = sixSegmentPile.Where(d => d != zero);
                        }
                    }
                }

                //9 remains from the 6 six segment pile now that 0 and 6 are removed
                var nine = sixSegmentPile.First();
                Translation[9] = nine;

                var outputValue = "";

                foreach (var value in display.Output)
                {
                    outputValue += Translation.First(t => t.Value == value).Key; ;
                }

                var outputValueInt = int.Parse(outputValue);
                total += outputValueInt;
            }

            Log.Information("Totalled the output values are {total}",
                total);
        }
    }
}