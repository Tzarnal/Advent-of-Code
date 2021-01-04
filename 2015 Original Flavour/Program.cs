using Serilog;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Advent.Framework;

namespace Advent
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CLI.Process(args);//Exit program now if this is a Debug build. If not continue and do a benchmark
        }
    }
}