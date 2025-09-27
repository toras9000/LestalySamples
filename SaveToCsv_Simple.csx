#r "nuget: Lestaly.General, 0.104.0"
#nullable enable
using Lestaly;

// IEnumerable なデータをCSVファイルに保存する。
// 保存データの形は任意の型。以下では匿名型を利用。

var outFile = ThisSource.RelativeFile($"SaveToCsv_Simple_{DateTime.Now:yyyyMMdd_HHmmss}.csv");
WriteLine("Search Directory");
Write(">");
var searchDir = ReadLine().CancelIfWhite().Unquote().CancelIfWhite().AsDirectoryInfo();
await searchDir
    .SelectFiles(c => new
    {
        Path = c.File?.RelativePathFrom(searchDir),
        Size = c.File?.Length.ToHumanize(),
        Time = c.File?.LastWriteTime,
    })
    .SaveToCsvAsync(outFile);
