using Advent;
using Serilog;

namespace Day_07
{
    //https://adventofcode.com/2022/day/7#part2
    public class Part2 : IAdventProblem
    {
        private string Dayname => Helpers.GetDayFromNamespace(this);
        public string ProblemName { get => $"Day {Dayname}: No Space Left On Device. Part Two."; }

        public void Run()
        {
            //var testinput = Part1.ParseInput($"Day {Dayname}/inputTest.txt");
            //Solve(testinput);

            var input = Part1.ParseInput($"Day {Dayname}/input.txt");
            Solve(input);
        }

        public void Solve(List<string> input)
        {
            var rootDir = new ElfDirectory("/");
            var currentDir = rootDir;

            var files = new List<ElfFile>();
            var directories = new List<ElfDirectory>();
            directories.Add(rootDir);

            //Parse input
            foreach (var line in input)
            {
                switch (line)
                {
                    case string l when l.StartsWith("$ cd /"):
                        currentDir = rootDir;
                        break;

                    case string l when l.StartsWith("$ cd .."):
                        if (!currentDir.root) //Can't go up from root
                            currentDir = currentDir.Parent;

                        break;

                    case string l when l.StartsWith("$ cd"):
                        var targetdirName = l.Split(' ')[2];

                        var targetdir = directories
                            .Where(d => d.Name == targetdirName && d.Parent == currentDir);

                        if (targetdir.Any())
                        {
                            currentDir = targetdir.FirstOrDefault();
                        }
                        else
                        {
                            Log.Verbose("Entering directory without first listing it.");
                            Log.Verbose(line);
                            var newTargetDir = new ElfDirectory(targetdirName, currentDir);
                            currentDir = newTargetDir;
                        }

                        break;

                    case string l when l.StartsWith("$ ls"):
                        //Do not technically need to do anything here atm
                        break;

                    case string l when l.StartsWith("dir"):
                        var dirName = line.Split(' ')[1];

                        var exists = directories
                            .Where(d => d.Name == dirName && d.Parent == currentDir)
                            .Any();

                        if (!exists)
                        {
                            directories.Add(
                                new ElfDirectory(dirName, currentDir));
                        }

                        break;

                    default:
                        var fileChunks = line.Split(' ');
                        var size = int.Parse(fileChunks[0]);

                        files.Add(
                            new ElfFile(fileChunks[1], size, currentDir));
                        break;
                }
            }

            //This problem can probably be expressed as a recurisive depth first search with memoization
            //But i don't feel like doing that

            //Find the depth of all directories, higher depth means higher priority in determining size
            var priorities = new List<(int priority, ElfDirectory dir)>();
            foreach (var dir in directories)
            {
                var priority = 0;
                var cdir = dir;

                while (!cdir.root)
                {
                    priority++;
                    cdir = cdir.Parent;
                }

                priorities.Add((priority, dir));
            }

            //Determine sizes
            var directorySizes = new List<(ElfDirectory dir, int size)>();
            var maxPriority = priorities.Select(p => p.priority).Max();

            for (int i = maxPriority; i >= 0; i--)
            {
                foreach (var tuple in priorities.Where(p => p.priority == i))
                {
                    var fileSizeSum = files.Where(f => f.Parent == tuple.dir).Select(f => f.Size).Sum();

                    var dirs = directories.Where(f => f.Parent == tuple.dir);
                    var dirSizeSum = 0;

                    foreach (var dir in dirs)
                    {
                        dirSizeSum += directorySizes.First(d => d.dir == dir).size;
                    }

                    directorySizes.Add((tuple.dir, fileSizeSum + dirSizeSum));
                }
            }

            var totalDiskSpace = 70000000;
            var requiredDiskSpace = 30000000;
            var rootSize = directorySizes.First(d => d.dir.root == true).size;

            var sizeToDelete = requiredDiskSpace - (totalDiskSpace - rootSize);

            var candidateDirs = directorySizes.Where(d => d.size >= sizeToDelete);
            var candidate = candidateDirs.OrderBy(d => d.size).First();

            Log.Information("Total size of directory to delete is {size}.", candidate.size);
        }
    }
}