#r "nuget: Lestaly, 0.31.0"
using Lestaly;

var thisFile = ThisSource.GetFile();
Console.WriteLine($"This File: {thisFile.FullName}");

var relativeFile = ThisSource.RelativeFile("./sub/sub2/file.txt");
Console.WriteLine($"Relative File: {relativeFile.FullName}");

var relativeDir = ThisSource.RelativeDirectory("../other/any");
Console.WriteLine($"Relative Dir: {relativeDir.FullName}");

var curRelDir = CurrentDir.RelativeDirectory("aaa/bbb");
Console.WriteLine($"Current Relative Dir: {curRelDir.FullName}");

var moreRelFile = curRelDir.RelativeFile("more/rel/file.txt");
Console.WriteLine($"More Relative Dir: {moreRelFile.FullName}");

var relPath = moreRelFile.RelativePathFrom(CurrentDir.RelativeDirectory("."), ignoreCase: true);
Console.WriteLine($"Relative Path: {relPath}");

var testDir = CurrentDir.RelativeDirectory("test").WithCreate();
testDir.RelativeFile("xxx/a.txt").WithDirectoryCreate().Touch();
await testDir.RelativeFile("xxx/b.txt").WithDirectoryCreate().WriteAllTextAsync("t-e-x-t");
