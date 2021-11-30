using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_04
{
    //https://adventofcode.com/2016/day/04#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Security Through Obscurity. Part Two."; }

        public void Run()
        {
            var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public IEnumerable<Room> ValidatedRooms(List<Room> Rooms)
        {
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
                    yield return room;
                }
            }
        }

        public void Solve(List<Room> Rooms)
        {
            var alphabet = "abcdefghijklmnopqrstuvwxyz".ToArray();

            foreach (var room in ValidatedRooms(Rooms))
            {
                var roomName = "";

                foreach (var c in room.EncryptedNameDashes)
                {
                    if (c == '-')
                    {
                        roomName += c;
                        continue;
                    }

                    var alphabetIndex = Array.IndexOf(alphabet, c);
                    alphabetIndex += room.SectorId;
                    alphabetIndex %= alphabet.Length;

                    roomName += alphabet[alphabetIndex];
                }

                if (roomName.Contains("north"))
                {
                    Log.Information("Room with {north} in it: {roomName} with sectorID : {SectorId}",
                        "north", roomName, room.SectorId);
                }
            }
        }
    }
}