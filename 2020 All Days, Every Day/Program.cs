using Serilog;
using System.IO;

namespace Advent
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var cli = new CLI();
            cli.Process(args);
        }
    }
}