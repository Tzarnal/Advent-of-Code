using Advent;
using Serilog;

namespace Day_18
{
    public class AdventDay : IAdventDay
    {
        private IAdventProblem ProblemPart1;
        private IAdventProblem ProblemPart2;
        private IAdventProblem ProblemPart3;

        public AdventDay()
        {
            ProblemPart1 = new Part1();
            ProblemPart2 = new Part2();
            ProblemPart3 = new Part2Proper();
        }

        public void SolveProblems()
        {
            Helpers.ProblemRunner(ProblemPart1);
            Helpers.ProblemRunner(ProblemPart2);
            Helpers.ProblemRunner(ProblemPart3);
        }
    }
}