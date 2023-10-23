#r "nuget: Lestaly, 0.50.0"
#nullable enable
using System.ComponentModel.DataAnnotations;
using Lestaly;

// IEnumerable なデータをExcelファイルに保存する。
// 保存データの形は任意の型。保存オプションで動作を指定。

record Item([Display(Name = "Address")] Uri URL, ExcelExpand Values);

var data = new Item[]
{

    new(new("http://localhost:1000"), new(new object[]{ 1, 3, 5, })  ),
    new(new("http://localhost:2000"), new(Array.Empty<object>())     ),
    new(new("http://localhost:3000"), new(new object[]{ 10, })       ),
};

var options = new SaveToExcelOptions();
options.Sheet = "Options";
options.AutoLink = true;
options.UseCaptionAttribute = true;
options.ColumnSpanSelector = m => m.Name == nameof(Item.Values) ? 5 : 1;
data.SaveToExcel(ThisSource.RelativeFile("./SaveToExcel_Options.xlsx"), options);
