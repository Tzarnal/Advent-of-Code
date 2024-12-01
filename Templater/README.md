Templater
===================

Templater automatically sets up files for a provided day an year of an advent of code problem 

```templater.exe 2023 1```

Automatically adds a 'Day 01' folder to the project with a name starting with 2023, it adds Part1.cs and Part2.cs populated with the correct problem name and downloads input.txt. 

The templater sources the files from Sharing is Caring Day 00. Edits to the Part1 and Part2 .cs files there will be copied over on the next use of Templater.

Templater requires a session id sourced from the AoC cookie to be places in session.txt in the same folder as the .exe file. Templater checks one upon running it does not help you get split second input.txt the moment the problem goes up on the site. 

