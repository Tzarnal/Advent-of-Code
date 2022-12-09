using Advent;
using Advent.AoCLib;
using Serilog;

namespace Day_09
{
    //https://adventofcode.com/2022/day/9
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Rope Bridge. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<(string direction, int distance)> input)
        {
            var head = new IntVector2();
            var tail = new IntVector2();

            var tailLocations = new HashSet<IntVector2>
            {
               tail.Copy
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

                    var difference = head - tail;

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
                        tail.X += Math.Sign(difference.X);
                        tail.Y += Math.Sign(difference.Y);
                    }

                    tailLocations.Add(tail.Copy);
                }
            }

            Log.Information("The rope tail visited {count} distinct locations.", tailLocations.Count);
        }

        public static List<(string direction, int distance)> ParseInput(string filePath)
        {
            var output = new List<(string direction, int distance)>();

            foreach (var line in File.ReadAllLines(filePath))
            {
                var chunks = line.Split(' ');

                output.Add((chunks[0], int.Parse(chunks[1])));
            }

            return output;
        }
    }
}