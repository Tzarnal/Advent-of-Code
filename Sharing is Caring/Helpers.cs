using System.IO;
using System.Collections.Generic;
using Serilog;

namespace Advent
{
    public static class Helpers
    {
        public static List<int> ReadNumbersFile(string filePath)
        {
            var inputFile = File.ReadAllLines(filePath);
            var inputList = new List<int>();

            foreach (var line in inputFile)
            {
                int c;
                if (int.TryParse(line, out c))
                {
                    inputList.Add(c);
                }
                else
                {
                    Log.Warning("Conversion Error: {c}", c);
                }
            }

            return inputList;
        }

        public static List<string> ReadStringsFile(string filePath)
        {
            var inputFile = File.ReadAllLines(filePath);

            return new List<string>(inputFile);
        }

        public static string GetDayFromNamespace(object o)
        {
            var nameSpace = o.GetType().Namespace;

            var day = nameSpace.Split('_')[1].Trim();

            return day;
        }
    }
}