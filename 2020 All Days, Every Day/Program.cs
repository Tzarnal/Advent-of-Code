using Serilog;
using System.IO;
using Advent.Framework;

namespace Advent
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var cli = new CLI();
            CLI.Process(args);
        }
    }
}