using System;
using System.Collections.Generic;
using System.Text;

namespace Advent
{
    public interface IAdventBenchmark
    {
        public IAdventProblem ProblemPart1 { get; }
        public IAdventProblem ProblemPart2 { get; }
    }
}