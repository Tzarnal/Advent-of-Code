using Advent;
using Serilog;

namespace Day_11
{
    public class AdventDay : IAdventDay
    {
        private IAdventProblem ProblemPart1;
        private IAdventProblem ProblemPart2;
        private IAdventProblem ProblemPart1Grid;
        private IAdventProblem ProblemPart2Grid;

        public AdventDay()
        {
            ProblemPart1 = new Part1();
            ProblemPart2 = new Part2();
            ProblemPart1Grid = new Part1TextGrid();
            ProblemPart2Grid = new Part2TextGrid();
        }

        public void SolveProblems()
        {
            Helpers.ProblemRunner(ProblemPart1);
            Helpers.ProblemRunner(ProblemPart2);
            Helpers.ProblemRunner(ProblemPart1Grid);
            Helpers.ProblemRunner(ProblemPart2Grid);
        }
    }
}