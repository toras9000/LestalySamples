#r "nuget: Lestaly, 0.31.0"
using Lestaly;

return await Paved.RunAsync(configuration: o => o.AnyPause(), action: async () =>
{
    using var tee = new TeeWriter();
    tee.Bind(Console.Out);
    tee.Bind(ThisSource.RelativeFile("TeeWriter.txt").CreateTextWriter());

    await tee.WriteLineAsync("test1");
    await tee.WriteLineAsync("test2");
});
