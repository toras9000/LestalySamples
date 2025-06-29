#r "nuget: Lestaly, 0.100.0"
#nullable enable
using Lestaly;

// IEnumerable なデータをCSVファイルに保存する。
// 保存データの形は任意の型。以下では匿名型を利用。

var outFile = ThisSource.RelativeFile($"SaveToCsv_Simple_{DateTime.Now:yyyyMMdd_HHmmss}.csv");
var searchDir = new DirectoryInfo(ConsoleWig.Write("Search Directory\n>").ReadLine().Unquote());
await searchDir
    .SelectFiles(c => new
    {
        Path = c.File?.RelativePathFrom(searchDir),
        Size = c.File?.Length.ToHumanize(),
        Time = c.File?.LastWriteTime,
    })
    .SaveToCsvAsync(outFile);
