#r "nuget: Lestaly, 0.100.0"
#nullable enable
using Lestaly;

// IEnumerable なデータをExcelファイルに保存する。
// 保存データの形は任意の型。以下では匿名型を利用。

var outFile = ThisSource.RelativeFile($"SaveToExcel_Simple_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
var searchDir = new DirectoryInfo(ConsoleWig.Write("Search Directory\n>").ReadLine().Unquote());
searchDir
    .SelectFiles(c => new
    {
        Path = c.File?.RelativePathFrom(searchDir),
        Size = c.File?.Length.ToHumanize(),
        Time = c.File?.LastWriteTime,
    })
    .SaveToExcel(outFile);
