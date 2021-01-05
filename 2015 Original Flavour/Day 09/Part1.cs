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
            //var testinputList = ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinputList);

            var inputList = ParseInput($"Day {Dayname}/input.txt");
            Solve(inputList);
        }

        public static void Solve(Dictionary<string, City> CityGraph)
        {
            var cities = CityGraph.Count;

            //Generate a route for every possible starting city
            var routes = CityGraph.Select(c => Travel(CityGraph, cities, c.Key));

            //Order by route length pick the first
            var (route, distance) = routes.OrderBy(r => r.distance).First();

            Log.Information("Shortest route is {r}: {d}", route, distance);
        }

        public static (string route, int distance) Travel(Dictionary<string, City> CityGraph, int depth, string start)
        {
            string route = "";
            int distance = 0;
            string current = start;

            //First step on the route, does not actually travel a distance.
            route += $"{current}";

            //Order the connections from the current location by distance, then pick the first/shortest
            var shortestConnection = CityGraph[current].Connections
                .OrderBy(kvp => kvp.Value).First();
            depth--;

            while (depth > 0)
            {
                //Order the connections from the current location by distance, then pick the first that has not yet been visted.
                shortestConnection = CityGraph[current].Connections
                    .OrderBy(kvp => kvp.Value)
                    .First(kvp => !route.Contains(kvp.Key));

                //Add the location to the route, add the travel distance to the total
                distance += shortestConnection.Value;
                current = shortestConnection.Key;
                route += $"=>{current}";

                //Finished a step
                depth--;
            }

            return (route, distance);
        }

        public static Dictionary<string, City> ParseInput(string filePath)
        {
            var CityGraph = new Dictionary<string, City>();

            foreach (var line in Helpers.ReadStringsFile(filePath))
            {
                (string cityA, string cityB, int distance) =
                    line.Extract<(string, string, int)>(@"(.+) to (.+) = (\d+)");

                if (!CityGraph.ContainsKey(cityA))
                {
                    var newCity = new City(cityA);
                    CityGraph.Add(cityA, newCity);
                }

                if (!CityGraph.ContainsKey(cityB))
                {
                    var newCity = new City(cityB);
                    CityGraph.Add(cityB, newCity);
                }

                CityGraph[cityA].Connections[cityB] = distance;
                CityGraph[cityB].Connections[cityA] = distance;
            }

            return CityGraph;
        }
    }

    public class City
    {
        public string Name;
        public Dictionary<string, int> Connections;

        public City(string name)
        {
            Connections = new Dictionary<string, int>();
            Name = name;
        }
    }
}