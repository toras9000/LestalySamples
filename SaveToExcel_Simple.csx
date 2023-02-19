#r "nuget: Lestaly, 0.31.0"
using Lestaly;

var outFile = ThisSource.RelativeFile($"SaveToExcel_Simple_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
var searchDir = new DirectoryInfo(ConsoleWig.ReadLine("Search Directory\n>"));
searchDir.SelectFiles(c => new
    {
        Path = c.File.RelativePathFrom(searchDir, ignoreCase: true),
        Size = c.File.Length.ToHumanize(),
        Time = c.File.LastWriteTime,
    })
    .SaveToExcel(outFile);
