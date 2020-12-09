using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent
{
    public record HGCcExecutionResult
    {
        public long Accumulator;
        public int Cursor;
        public bool InfiniteLoopDetected;
        public bool NormalExit;
    }
}