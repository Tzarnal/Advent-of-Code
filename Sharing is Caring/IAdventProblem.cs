using System;
using System.Collections.Generic;
using System.Text;

namespace Advent
{
    public interface IAdventProblem
    {
        public void Run();

        public string ProblemName { get; }
    }
}