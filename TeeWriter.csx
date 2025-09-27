#r "nuget: Lestaly.General, 0.104.0"
#nullable enable
using Lestaly;

return await Paved.ProceedAsync(async () =>
{
    // 複数の TextWriter に同じ内容を出力する TeeWriter
    using var tee = new TeeWriter();
    tee.Bind(Console.Out);
    tee.Bind(ThisSource.RelativeFile("TeeWriter.txt").CreateTextWriter());

    await tee.WriteLineAsync("test1");
    await tee.WriteLineAsync("test2");
});
