#r "nuget: Lestaly, 0.48.0"
using Lestaly;

// IEnumerable なデータをCSVファイルに保存する。
// 保存データの形は任意の型。以下では匿名型を利用。

var outFile = ThisSource.RelativeFile($"SaveToCsv_Simple_{DateTime.Now:yyyyMMdd_HHmmss}.csv");
var searchDir = new DirectoryInfo(ConsoleWig.Write("Search Directory\n>").ReadLine());
await searchDir
    .SelectFiles(c => new
    {
        Path = c.File.RelativePathFrom(searchDir, ignoreCase: true),
        Size = c.File.Length.ToHumanize(),
        Time = c.File.LastWriteTime,
    })
    .SaveToCsvAsync(outFile);
