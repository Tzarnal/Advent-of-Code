using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Serilog;
using Advent;
using RegExtract;
using System.Diagnostics;

namespace Day_16
{
    //https://adventofcode.com/2022/day/16
    public class Part1 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Proboscidea Volcanium. Part One."; }

        public void Run()
        {
            var testinput = ParseInput($"Day {Dayname}/inputTest.txt");
            Solve(testinput);

            var input = ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(Dictionary<string, Valve> valves)
        {
            //Pre Compute all relevant routes
            FindRoutes(valves);

            var rooms = valves.Select(v => v.Value);
            var startRoom = valves["AA"];

            Log.Information("A Solution Can Be Found.");
        }

        public static void FindRoutes(Dictionary<string, Valve> valves)
        {
            //We don't really need to plan routes from rooms that will only ever be intermediate steps, and rooms with 0 flow are
            //All intermediate steps.
            var destinationRooms = valves.Where(r => r.Value.FlowRate > 0).Select(r => r.Key).ToList();
            destinationRooms.Add("AA"); // AA may or may not have flow but since we start there we need routes to leave it

            foreach (var room in destinationRooms)
            {
                var currentRoom = valves[room];
                foreach (var targetRoom in destinationRooms)
                {
                    //Don't need a route back to the same room
                    if (currentRoom.Name == targetRoom)
                    {
                        continue;
                    }

                    //If we already have this route, skip it ( we generate the 1 step routes from input directly )
                    if (currentRoom.Routes.ContainsKey(targetRoom))
                    {
                        continue;
                    }

                    var (_, Route) = Traverse(targetRoom, new List<string>(), currentRoom, currentRoom.Name);
                    currentRoom.Routes.Add(targetRoom, Route);
                }
            }
        }

        private static (bool FoundRoute, List<string> Route) Traverse(string targetRoom, List<string> route, Valve currentRoom, string start)
        {
            var nRoute = new List<string>(route);
            nRoute.Add(currentRoom.Name);

            //We are the destination
            if (currentRoom.Name == targetRoom)
            {
                return (true, nRoute);
            }

            //Found destination one step down the graph
            if (currentRoom.Connections.ContainsKey(targetRoom))
            {
                nRoute.Add(targetRoom);
                return (true, nRoute);
            }

            var potentialRoutes = new List<(bool FoundRoute, List<string> Route)>();

            //Find all routes to target room
            foreach (var connection in currentRoom.Connections)
            {
                //Avoid looping
                if (nRoute.Contains(connection.Key))
                {
                    continue;
                }

                var result = Traverse(targetRoom, nRoute, connection.Value, start);
                if (result.FoundRoute)
                {
                    potentialRoutes.Add(result);
                }
            }

            //If we found any routes return the shortest
            if (potentialRoutes.Any())
            {
                return potentialRoutes.OrderBy(r => r.Route.Count).First();
            }

            //Din't find anything
            return (false, nRoute);
        }

        public static Dictionary<string, Valve> ParseInput(string filePath)
        {
            var valves = new Dictionary<string, Valve>();

            foreach (var line in File.ReadAllLines(filePath))
            {
                var (valveName, flowRate, connections) = line.Extract<(string, int, string)>(
                    "Valve (.+) has flow rate=(\\d+); tunnels? leads? to valves? (.*)");

                var valveConnections = connections.Split(", ").ToList();

                var valve = new Valve(valveName, flowRate, valveConnections);
                valves.Add(valveName, valve);
            }

            foreach (var v in valves.Values)
            {
                v.BuildConnections(valves);
            }

            return valves;
        }
    }

    public class Valve
    {
        public int FlowRate;
        public string Name;
        public bool Open;

        public List<string> ConnectionStrings;
        public Dictionary<string, Valve> Connections;
        public Dictionary<string, List<string>> Routes;

        public Valve()
        {
            Name = "??";
            FlowRate = 0;

            ConnectionStrings = new();
            Connections = new();
            Routes = new();
        }

        public Valve(string name, int flowRate, List<string> connections)
        {
            Name = name;
            FlowRate = flowRate;

            ConnectionStrings = connections;
            Connections = new();
            Routes = new();
        }

        public Valve(string name, int flowRate)
        {
            Name = name;
            FlowRate = flowRate;

            ConnectionStrings = new();
            Connections = new();
            Routes = new();
        }

        public void BuildConnections(Dictionary<string, Valve> connectionList)
        {
            foreach (var conection in ConnectionStrings)
            {
                var route = new List<string>();
                route.Add(Name);
                route.Add(conection);

                Connections.Add(conection, connectionList[conection]);
                Routes.Add(conection, route);
            }
        }
    }
}