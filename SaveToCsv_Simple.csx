#r "nuget: Lestaly, 0.31.0"
using Lestaly;

var outFile = ThisSource.RelativeFile($"SaveToCsv_Simple_{DateTime.Now:yyyyMMdd_HHmmss}.csv");
var searchDir = new DirectoryInfo(ConsoleWig.ReadLine("Search Directory\n>"));
await searchDir.SelectFiles(c => new
    {
        Path = c.File.RelativePathFrom(searchDir, ignoreCase: true),
        Size = c.File.Length.ToHumanize(),
        Time = c.File.LastWriteTime,
    })
    .SaveToCsvAsync(outFile);
