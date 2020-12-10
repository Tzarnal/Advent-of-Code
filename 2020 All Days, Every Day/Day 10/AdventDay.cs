using Advent;
using Serilog;

namespace Day_10
{
    public class AdventDay : IAdventDay
    {
        private IAdventProblem ProblemPart1;
        private IAdventProblem ProblemPart2;

        public AdventDay()
        {
            ProblemPart1 = new Part1();
            ProblemPart2 = new Part2();
        }

        public void SolveProblems()
        {
            Helpers.ProblemRunner(ProblemPart1);
            Helpers.ProblemRunner(ProblemPart2);
        }
    }
}