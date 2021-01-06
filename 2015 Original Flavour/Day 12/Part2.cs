using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Day_12
{
    //https://adventofcode.com/2015/day/12#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: JSAbacusFramework.io. Part Two."; }

        public void Run()
        {
            //var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(ExpandoObject data)
        {
            var ints = FindNumbers(data);

            var awnser = ints.Sum();
            Log.Information("Sum of all integers in input JSON, ignoring red elements is {awnser}", awnser);
        }

        //Handle JSOn objects, in code represented as ExpandoObject
        public List<Int64> FindNumbers(ExpandoObject data)
        {
            if (data.Any(d => d.Value is string value && value == "red"))
            {
                return new();
            }

            var numbers = new List<Int64>();

            foreach (var (_, element) in data)
            {
                if (element is Int64 number)
                {
                    numbers.Add(number);
                }

                if (element is ExpandoObject subObject)
                {
                    numbers.AddRange(FindNumbers(subObject));
                }

                if (element is List<object> list)
                {
                    numbers.AddRange(Dive(list));
                }
            }

            return numbers;
        }

        //Handle JSON Arrays, in code represented as lists of object
        public List<Int64> Dive(List<object> data)
        {
            //Note no Red check for Arrays

            var numbers = new List<Int64>();

            foreach (var element in data)
            {
                if (element is Int64 number)
                {
                    numbers.Add(number);
                }

                if (element is ExpandoObject subObject)
                {
                    numbers.AddRange(FindNumbers(subObject));
                }

                if (element is List<object> list)
                {
                    numbers.AddRange(Dive(list));
                }
            }

            return numbers;
        }

        public static ExpandoObject ParseInput(string filePath)
        {
            var file = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<ExpandoObject>(file, new ExpandoObjectConverter());
        }
    }
}