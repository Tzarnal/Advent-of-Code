using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;

namespace Day_09
{
    //https://adventofcode.com/2015/day/9
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: All in a Single Night. Part One."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public void Solve(Dictionary<string, City> CityGraph)
        {
            var cities = CityGraph.Count();

            var routes = CityGraph.Select(c => Travel(CityGraph, cities, c.Key));
            var shortest = routes.OrderBy(r => r.distance).First();

            Log.Information("Shortest route is {r}: {d}", shortest.route, shortest.distance);
        }

        public (string route, int distance) Travel(Dictionary<string, City> CityGraph, int depth, string start)
        {
            string route = "";
            int distance = 0;
            string current = start;

            route += $"{current}=>";
            var shortestConnection = CityGraph[current].Connections.OrderBy(kvp => kvp.Value).First();
            depth--;

            while (depth > 0)
            {
                shortestConnection = CityGraph[current].Connections.OrderBy(kvp => kvp.Value)
                                                                        .Where(kvp => !route.Contains(kvp.Key))
                                                                        .First();

                distance += shortestConnection.Value;
                current = shortestConnection.Key;
                route += $"{current}=>";

                depth--;
            }

            route = route.Trim('>');
            route = route.Trim('=');

            return (route, distance);
        }

        private Dictionary<string, City> ParseInput(string filePath)
        {
            var CityGraph = new Dictionary<string, City>();

            foreach (var line in Helpers.ReadStringsFile(filePath))
            {
                (string cityA, string cityB, int distance) connection = line.Extract<(string, string, int)>(@"(.+) to (.+) = (\d+)");

                if (!CityGraph.ContainsKey(connection.cityA))
                {
                    var newCity = new City
                    {
                        Name = connection.cityA
                    };

                    CityGraph.Add(connection.cityA, newCity);
                }

                if (!CityGraph.ContainsKey(connection.cityB))
                {
                    var newCity = new City
                    {
                        Name = connection.cityB
                    };

                    CityGraph.Add(connection.cityB, newCity);
                }

                CityGraph[connection.cityA].Connections[connection.cityB] = connection.distance;
                CityGraph[connection.cityB].Connections[connection.cityA] = connection.distance;
            }

            return CityGraph;
        }

        public class City
        {
            public string Name;
            public Dictionary<string, int> Connections;

            public City()
            {
                Connections = new Dictionary<string, int>();
            }
        }
    }
}