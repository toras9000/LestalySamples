#r "nuget: Lestaly, 0.27.0"
using System.ComponentModel.DataAnnotations;
using Lestaly;

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
data.SaveToExcel(ThisSource.GetRelativeFile("./SaveToExcel_Options.xlsx"), options);
