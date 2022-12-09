using Advent;
using Advent.AoCLib;
using Serilog;

namespace Day_09
{
    //https://adventofcode.com/2022/day/9#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Rope Bridge. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(string direction, int distance)> input)
        {
            var head = new IntVector2();
            
            var tail = new IntVector2[9];
            for(int i=0; i<9; i++)
            {
                tail[i] = new IntVector2();
            }

            var tailLocations = new HashSet<IntVector2>
            {
                tail[8].Copy
            };

            foreach (var instruction in input)
            {
                for (int step = 0; step < instruction.distance; step++)
                {
                    switch (instruction.direction)
                    {
                        case "U":
                            head.Y++;
                            break;

                        case "D":
                            head.Y--;
                            break;

                        case "R":
                            head.X++;
                            break;

                        case "L":
                            head.X--;
                            break;

                        default:
                            Log.Warning("Incorrect instruction: {direction}", instruction.direction);
                            break;
                    }

                    var lastKnot = head;
                    for (int i = 0; i < 9; i++)
                    {
                        tail[i] = MoveKnot(lastKnot, tail[i]);

                        lastKnot = tail[i];
                    }

                    tailLocations.Add(tail[8]);
                }
            }

            Log.Information("The rope tail visited {count} distinct locations.", tailLocations.Count);
        }

        private IntVector2 MoveKnot(IntVector2 a, IntVector2 b)
        {
            var difference = a - b;

            var NewKnot = new IntVector2(b);

            //in fact, the head (H) and tail (T) must always be touching
            //(diagonally adjacent and even overlapping both count as touching

            //A difference higher than 1 in either direction means they no longer touch.
            //Abs lets us bypass some akwward negative to positive wrangling
            if (Math.Abs(difference.X) > 1 || Math.Abs(difference.Y) > 1)
            {
                //Regardless of the difference between positions we always move at
                //most 1 position on either axis
                //We know the direction based on positive or negative in the difference
                //Math.Sign turns a positive number of any size to 1 and a negative number of any size to -1
                NewKnot.X += Math.Sign(difference.X);
                NewKnot.Y += Math.Sign(difference.Y);
            }

            return NewKnot;
        }
    }
}