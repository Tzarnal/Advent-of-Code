using Advent;
using Serilog;

namespace Day_08
{
    public class AdventDay : IAdventDay
    {
        private IAdventProblem ProblemPart1;
        private IAdventProblem ProblemPart2;
        private IAdventProblem ExtractedVM;

        public AdventDay()
        {
            ProblemPart1 = new Part1();
            ProblemPart2 = new Part2();
            ExtractedVM = new ExtractedVM();
        }

        public void SolveProblems()
        {
            Helpers.ProblemRunner(ProblemPart1);
            Helpers.ProblemRunner(ProblemPart2);
            Helpers.ProblemRunner(ExtractedVM);
        }
    }
}