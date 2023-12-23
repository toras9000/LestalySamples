#r "nuget: Lestaly, 0.53.0"
#nullable enable
using Lestaly;

// IEnumerable なデータをExcelファイルに保存する。
// 保存用データ型のプロパティに使う、Excel用に特殊化した情報を持たせた型をいくつか用意。

var data = new[]
{
    new { Name = "Abc", Number = new ExcelStyle(100, ForeColor: "Blue"),                        Link = new ExcelHyperlink("http://localhost:1000", "Site1"), },
    new { Name = "Def", Number = new ExcelStyle(200, Extra: new(Comment: "default")),           Link = new ExcelHyperlink("http://localhost:2000", "Site2"), },
    new { Name = "Ghi", Number = new ExcelStyle(300, ForeColor: "Red", Extra: new(Bold: true)), Link = new ExcelHyperlink("http://localhost:3000", "Site3"), },
};

data.SaveToExcel(ThisSource.RelativeFile("./SaveToExcel_SpecialData.xlsx"));
