using Advent;

namespace Day_00
{
    public class AdventDay : IAdventDay
    {
        public IAdventProblem ProblemPart1 { get; set; }
        public IAdventProblem ProblemPart2 { get; set; }

        public AdventDay()
        {
            ProblemPart1 = new Part1();
            ProblemPart2 = new Part2();
        }

        public void PartOne()
        {
            ProblemPart1.Run();
        }

        public void PartTwo()
        {
            ProblemPart2.Run();
        }
    }
}