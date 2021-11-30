using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_04
{
    //https://adventofcode.com/2016/day/04
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Security Through Obscurity. Part One."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<Room> Rooms)
        {
            var sum = 0;

            foreach (var room in Rooms)
            {
                var charCounts = new Dictionary<char, int>();

                foreach (var c in room.EncryptedName)
                {
                    if (!charCounts.ContainsKey(c))
                        charCounts[c] = 0;

                    charCounts[c]++;
                }

                var checkSumValues = charCounts.OrderByDescending(c => c.Value).ThenBy(c => c.Key).Take(5);

                var checksum = "";
                foreach (var c in checkSumValues)
                {
                    checksum += c.Key;
                }

                if (room.Checksum == checksum)
                {
                    sum += room.SectorId;
                }
            }

            Log.Information("The sum of sector IDs of real rooms is {sum}",
                sum);
        }

        public static List<Room> ParseInput(string filePath)
        {
            var output = new List<Room>();

            foreach (var line in File.ReadAllLines(filePath))
            {
                var room = new Room();

                var sections = line.Split('-');

                room.EncryptedNameElements = sections[0..^1].ToList();

                var subsection = sections[^1].Split('[');

                var sectorID = int.Parse(subsection[0]);
                room.SectorId = sectorID;

                var checksum = subsection[1].Trim(']');
                room.Checksum = checksum;

                output.Add(room);
            }

            return output;
        }
    }

    public class Room
    {
        public List<string> EncryptedNameElements;
        public int SectorId;
        public string Checksum;

        public String EncryptedName => string.Concat(EncryptedNameElements);

        public String EncryptedNameDashes => string.Join('-', EncryptedNameElements);
    }
}