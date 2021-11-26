using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace Templater
{
    internal static class Program
    {
        private static string Templatepath = @"Sharing is Caring\Day 00";

        private static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Needs two arguments, year and day.");
                return;
            }

            var year = args[0].Trim().ToLower();
            var day = args[1].Trim().ToLower();

            var rootDir = Directory.GetCurrentDirectory().Split("Templater")[0];
            var templateDir = rootDir + Templatepath;

            var sessionId = "";
            if (File.Exists("session.txt"))
            {
                sessionId = File.ReadAllText("session.txt");
            }
            else
            {
                Console.WriteLine("No Session File.");
                File.WriteAllText("session.txt", "");

                return;
            }

            if (string.IsNullOrWhiteSpace(sessionId))
            {
                Console.WriteLine("No Session Data.");
                return;
            }

            var directories = Directory.GetDirectories(rootDir);

            var projectDirs = directories.Where(d => d.Contains(year));
            if (projectDirs.Count() == 0)
            {
                Console.WriteLine("Could not find project for given year. Make one ?");
                return;
            }

            var problemName = "";
            try
            {
                problemName = GetProblemName(sessionId, day, year);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not open Advent of Code website.");
                Console.WriteLine(e.Message);
                return;
            }

            var projectDir = projectDirs.First();

            var targetDir = projectDir + $"\\Day {day}\\";

            if (File.Exists("session.txt"))
            {
                sessionId = File.ReadAllText("session.txt");
            }
            else
            {
                Console.WriteLine("No Session File.");
                File.WriteAllText("session.txt", "");

                return;
            }

            if (string.IsNullOrWhiteSpace(sessionId))
            {
                Console.WriteLine("No Session Data.");
                return;
            }

            if (!CopyFiles(templateDir, targetDir))
            {
                Console.WriteLine($"Failed to copy files from {templateDir} to {targetDir}");
                return;
            }

            var input = "";
            try
            {
                input = GetInput(sessionId, day, year);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not open Advent of Code website.");
                Console.WriteLine(e.Message);
                return;
            }

            File.WriteAllText($"{targetDir}\\input.txt", input);

            DeTemplate(projectDir, targetDir, day, year, problemName);
        }

        private static void DeTemplate(string ProjectDirectory, string Directory, string DayName, string YearName, string ProblemTitle)
        {
            var part1File = Directory + @"Part1.cs";
            EditNamespace(part1File, DayName);
            EditPart(part1File, DayName, YearName, false, ProblemTitle);

            var part2File = Directory + @"Part2.cs";
            EditNamespace(part2File, DayName);
            EditPart(part2File, DayName, YearName, true, ProblemTitle);

            AddCopyAlways(ProjectDirectory, $"Day {DayName}\\input.txt");
            AddCopyAlways(ProjectDirectory, $"Day {DayName}\\inputTest.txt");
        }

        private static void EditPart(string FileName, string DayName, string YearName, bool part2 = false, string ProblemTitle = "Something Something")
        {
            var code = File.ReadAllText(FileName);

            var newUrl = $"https://adventofcode.com/{YearName}/day/{DayName}";
            if (part2)
                newUrl += "#part2";

            code = code.Replace("Problem URL", newUrl);
            code = code.Replace("Something Something", ProblemTitle);

            File.WriteAllText(FileName, code);
        }

        private static bool AddCopyAlways(string ProjectDirectory, string FilePath)
        {
            var files = Directory.GetFiles(ProjectDirectory);
            var projFile = "";
            foreach (var file in files)
            {
                if (file.EndsWith(".csproj"))
                {
                    projFile = file;
                }
            }

            if (string.IsNullOrWhiteSpace(projFile))
            {
                Console.WriteLine("Could not find project file");
                return false;
            }

            var projectLines = File.ReadAllLines(projFile).ToList();

            var insertPoint = 0;
            for (int i = 0; i < projectLines.Count; i++)
            {
                if (projectLines[i].Contains("<ItemGroup>"))
                {
                    insertPoint = i;
                    break;
                }
            }

            if (insertPoint == 0)
            {
                Console.WriteLine("Could not find a Itemgroup to edit in project");
                return false;
            }

            var contentBlock = $"        <Content Include=\"{FilePath}\"><CopyToOutputDirectory>Always</CopyToOutputDirectory></Content>";
            projectLines.Insert(insertPoint + 1, contentBlock);

            File.WriteAllLines(projFile, projectLines);

            return true;
        }

        private static void EditNamespace(string FileName, string DayName)
        {
            var code = File.ReadAllText(FileName);

            code = code.Replace("namespace Day_00", $"namespace Day_{DayName}");

            File.WriteAllText(FileName, code);
        }

        private static bool CopyFiles(string Source, string Destination)
        {
            //Make sure Destination exists
            Directory.CreateDirectory(Destination);

            //Make sure the sure and destination actually exist
            if (!Directory.Exists(Source) || !Directory.Exists(Destination))
            {
                return false;
            }

            var files = Directory.GetFiles(Source);
            foreach (var file in files)
            {
                var sourceFile = Path.GetFileName(file);
                var destinationFile = Path.Combine(Destination, sourceFile);

                File.Copy(file, destinationFile, true);
            }

            return true;
        }

        private static string GetInput(string Session, string Day, string Year)
        {
            var url = $"https://adventofcode.com/{Year}/day/{Day}/input";
            var wc = new WebClient();

            wc.Headers.Add(HttpRequestHeader.Cookie, $"session={Session}");
            string content = wc.DownloadString(url);

            //Fix to enviroment .newline
            content = content.Replace("\n", Environment.NewLine);

            //Trim Your Inputs
            content = content.Trim();

            return content;
        }

        private static string GetProblemName(string Session, string Day, string Year)
        {
            var url = $"https://adventofcode.com/{Year}/day/{Day}";
            var wc = new WebClient();

            wc.Headers.Add(HttpRequestHeader.Cookie, $"session={Session}");
            string content = wc.DownloadString(url);

            var titleRegex = @"--- Day \d+: (.+) ---";
            var titleMatch = Regex.Match(content, titleRegex);

            if (titleMatch.Success)
            {
                return titleMatch.Groups[1].Value;
            }

            return "Couldn't Find Title";
        }
    }
}