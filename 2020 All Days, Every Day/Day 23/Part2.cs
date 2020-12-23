using Advent;
using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_23
{
    //https://adventofcode.com/2020/day/23#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: Crab Cups. Part Two."; }

        public void Run()
        {
            var testinputList = ParseInput($"Day {Dayname}/inputTest.txt", 1_000_000);
            //Solve(testinputList, 10);
            //Solve(testinputList, 100);
            //Solve(testinputList, 10_000_000);

            var inputList = ParseInput($"Day {Dayname}/input.txt", 1_000_000);
            //Solve(inputList, 100);
            Solve(inputList, 10_000_000);
        }

        public void Solve(List<long> input, int moves)
        {
            int listSize = input.Count;

            input.Reverse();

            LinkedListItem First = new(input[0]);
            var Last = First;

            //Picking a destination cup requires looking up a value to find a list member
            //a dictionary lookup time is much better than traversing a million entry linked list
            var LookupDict = new Dictionary<long, LinkedListItem>
            {
                { input[0], First }
            };

            //Build the linked list in reverse so we can assign next values without a second loop
            foreach (var number in input.Skip(1))
            {
                var cup = new LinkedListItem(number);
                LookupDict.Add(number, cup);
                cup.Next = Last;
                Last = cup;
            }

            //Close the linked list.
            First.Next = Last;

            //Starting cup is the first list int he input, or the last in our reversed input
            var currentCup = Last;

            for (int i = 1; i <= moves; i++)
            {
                ////Just debuggin display
                //Log.Debug("-- move {i} --", i);
                //string cupsString = $"({currentCup.Value})";
                //var displayNextCup = currentCup.Next;
                //for (var c = 1; c < 9; c++)
                //{
                //    cupsString += $",{displayNextCup.Value}";
                //    displayNextCup = displayNextCup.Next;
                //}
                //Log.Debug("cups: " + cupsString);

                //The crab picks up the three cups that are immediately clockwise of the current cup.
                //They are removed from the circle; cup spacing is adjusted as necessary
                //to maintain the circle.
                var pickupCups = new List<long>
                {
                    currentCup.Next.Value,
                    currentCup.NextN(2).Value,
                    currentCup.NextN(3).Value
                };

                //Keeping this around to make how to reassign the next values clearer
                var finalPickup = currentCup.NextN(3);

                //Log.Debug("pick up: {@pickupCups}", pickupCups);

                //The crab selects a destination cup: the cup with a label equal to the
                //current cup's label minus one. If this would select one of the cups that
                //was just picked up, the crab will keep subtracting one until it finds a cup
                //that wasn't just picked up. If at any point in this process the value goes below
                //the lowest value on any cup's label, it wraps around to the highest value on any
                //cup's label instead.
                long destinationValue = currentCup.Value == 1 ? listSize : currentCup.Value - 1;
                while (pickupCups.Contains(destinationValue))
                {
                    destinationValue--;
                    if (destinationValue < 1)
                    {
                        destinationValue = listSize;
                    }
                }

                var destinationCup = LookupDict[destinationValue];
                //Log.Debug("destination: {destinationCup}", destinationCup.Value);

                //Need to keep this one set aside so we can glue the list back together
                var setAside = currentCup.Next;

                currentCup.Next = finalPickup.Next;
                finalPickup.Next = destinationCup.Next;
                destinationCup.Next = setAside;

                //Move the current cup for the next round
                currentCup = currentCup.Next;
            }

            var cupOne = LookupDict[1];
            var firstNext = cupOne.Next;
            var secondNext = cupOne.NextN(2);

            Log.Information("First two cups after 1 are {firstNext} and {secondNext} Product: {product}",
                firstNext.Value, secondNext.Value, firstNext.Value * secondNext.Value);
        }

        private List<long> ParseInput(string filePath, int fill = 0)
        {
            var input = File.ReadAllText(filePath).Select(c => long.Parse(c.ToString())).ToList();
            var m = input.Max(i => i);

            if (fill != 0)
            {
                for (long i = m + 1; i <= 1000000; i++)
                {
                    input.Add(i);
                }
            }

            return input;
        }
    }
}

public class LinkedListItem
{
    public LinkedListItem Next;

    public long Value;

    public LinkedListItem(long Value)
    {
        this.Value = Value;
    }

    public LinkedListItem(long Value, LinkedListItem Previous)
    {
        this.Value = Value;
    }

    public LinkedListItem NextN(int n)
    {
        if (n < 1)
            return this;

        LinkedListItem current = this;

        while (n > 0)
        {
            current = current.Next;
            n--;
        }

        return current;
    }
}