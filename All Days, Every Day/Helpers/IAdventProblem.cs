using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;

namespace Advent
{
    public interface IAdventProblem
    {
        public void Run();

        public string ProblemName { get; }
    }
}