#r "nuget: Lestaly, 0.27.0"
using Lestaly;

var outFile = ThisSource.GetRelativeFile($"SaveToExcel_Simple_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
var searchDir = new DirectoryInfo(ConsoleWig.ReadLine("Search Directory\n>"));
searchDir.SelectFiles(c => new
    {
        Path = c.File.RelativePathFrom(searchDir, ignoreCase: true),
        Size = c.File.Length.ToHumanize(),
        Time = c.File.LastWriteTime,
    })
    .SaveToExcel(outFile);
