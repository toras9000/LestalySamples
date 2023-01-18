#r "nuget: Lestaly, 0.27.0"
using Lestaly;

var thisFile = ThisSource.GetFile();
Console.WriteLine($"This File: {thisFile.FullName}");

var relativeFile = ThisSource.GetRelativeFile("./sub/sub2/file.txt");
Console.WriteLine($"Relative File: {relativeFile.FullName}");

var relativeDir = ThisSource.GetRelativeDirectory("../other/any");
Console.WriteLine($"Relative Dir: {relativeDir.FullName}");

var curRelDir = CurrentDir.GetRelativeDirectory("aaa/bbb");
Console.WriteLine($"Current Relative Dir: {curRelDir.FullName}");

var moreRelFile = curRelDir.GetRelativeFile("more/rel/file.txt");
Console.WriteLine($"More Relative Dir: {moreRelFile.FullName}");

var relPath = moreRelFile.RelativePathFrom(CurrentDir.GetRelativeDirectory("."), ignoreCase: true);
Console.WriteLine($"Relative Path: {relPath}");

var testDir = CurrentDir.GetRelativeDirectory("test").WithCreate();
testDir.GetRelativeFile("xxx/a.txt").WithDirectoryCreate().Touch();
await testDir.GetRelativeFile("xxx/b.txt").WithDirectoryCreate().WriteAllTextAsync("t-e-x-t");
