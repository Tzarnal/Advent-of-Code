using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Collections.Generic;
using Serilog;
using Advent;
using System.Text;

namespace Day_04
{
    //Problem URL
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: The Ideal Stocking Stuffer Part Two."; }

        public void Run()
        {
            //var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            Solve("ckczppom");
        }

        public void Solve(string input)
        {
            var md5Hasher = MD5.Create();
            var i = 1;
            while (true)
            {
                var hashInput = input + i.ToString();
                var hash = md5Hasher.ComputeHash(Encoding.ASCII.GetBytes(hashInput));
                var hashText = BitConverter.ToString(hash).Replace("-", "");

                //Log.Verbose("Found hash {hashText}. For {hashInput}.", hashText, hashInput);

                if (hashText.StartsWith("000000"))
                {
                    Log.Information("Found hash {hashText}. For {hashInput}. Awnser: {i}", hashText, hashInput, i);
                    return;
                }

                i++;
            }
        }
    }
}