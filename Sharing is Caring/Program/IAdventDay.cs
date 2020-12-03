using System;
using System.Collections.Generic;
using System.Text;

namespace Advent
{
    public interface IAdventDay
    {
        public IAdventProblem ProblemPart1 { get; }
        public IAdventProblem ProblemPart2 { get; }
    }
}