using Advent;
using Serilog;

namespace Day_04
{
    public class AdventDay : IAdventDay
    {
        private IAdventProblem ProblemPart1;
        private IAdventProblem ProblemPart2;
        private IAdventProblem ProblemPart2Revised;

        public AdventDay()
        {
            ProblemPart1 = new Part1();
            ProblemPart2 = new Part2();
            ProblemPart2Revised = new Part2Revised();
        }

        public void SolveProblems()
        {
            Log.Information("Running {ProblemName}", ProblemPart1.ProblemName);
            ProblemPart1.Run();

            Log.Information("Running {ProblemName}", ProblemPart2.ProblemName);
            ProblemPart2.Run();

            Log.Information("Running {ProblemName}", ProblemPart2Revised.ProblemName);
            ProblemPart2Revised.Run();
        }
    }
}