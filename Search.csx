#r "nuget: Lestaly.General, 0.108.0"
#nullable enable
using Lestaly;

return await Paved.ProceedAsync(async () =>
{
    await Task.CompletedTask;

    using var signal = new SignalCancellationPeriod();
    using var outenc = ConsoleWig.OutputEncodingPeriod(Encoding.UTF8);

    WriteLine("Search Directory");
    Write(">");
    var searchDir = ReadLine().CancelIfWhite().Unquote().CancelIfWhite().AsDirectoryInfo();

    var options = new VisitFilesOptions(
        Recurse: true,
        Handling: VisitFilesHandling.OnlyFile,
        SkipInaccessible: true,
        DirectoryFilter: dir => !dir.Name.RoughAny([".git", ".hg"])
    );
    foreach (var path in searchDir.SelectFiles(c => c.File?.RelativePathFrom(searchDir), options))
    {
        WriteLine(path);
    }
});
