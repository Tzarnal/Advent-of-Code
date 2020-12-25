using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_25
{
    //https://adventofcode.com/2020/day/25
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Combo Breaker. Part One."; }

        public void Run()
        {
            //var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            Solve(ParseInput($"Day {Dayname}/input.txt"));
        }

        public void Solve((int, int) input)
        {
            var (cardPublicKey, doorPublicKey) = input;

            var cardLoopSize = FindLoopNumber(cardPublicKey);
            var encryptionKey = CalculateKey(doorPublicKey, cardLoopSize);

            Log.Information("The cards loopsize was {cardLoopSize} together with the door public key {doorPublicKey} the awnser is {awnser}"
                , cardLoopSize, doorPublicKey, encryptionKey);
        }

        public long CalculateKey(long subjectKey, long loopSize)
        {
            long key = 1;
            for (int i = 0; i < loopSize; i++)
            {
                key *= subjectKey;
                key %= 20201227;
            }

            return key;
        }

        public long FindLoopNumber(long finalKey)
        {
            long key = 7;
            var count = 1;
            while (key != finalKey)
            {
                key *= 7;
                key %= 20201227;

                count++;
            }

            return count;
        }

        private (int, int) ParseInput(string filePath)
        {
            var input = Helpers.ReadStringsFile(filePath);
            var cardPublicKey = int.Parse(input[0]);
            var doorPublicKey = int.Parse(input[1]);

            return (cardPublicKey, doorPublicKey);
        }
    }
}