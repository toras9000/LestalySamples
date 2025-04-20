#r "nuget: Lestaly, 0.74.0"
#nullable enable
using System.Threading;
using Lestaly;

return await Paved.RunAsync(config: o => o.AnyPause(), action: async () =>
{
    // 複数の TextWriter に同じ内容を出力する TeeWriter
    using var tee = new TeeWriter();
    tee.Bind(Console.Out);
    tee.Bind(ThisSource.RelativeFile("CmdTeeWriter.txt").CreateTextWriter());

    // TextWriter を利用可能な場面で使う。
    using var signal = ConsoleWig.CreateCancelKeyHandlePeriod();
    using var canceller = CancellationTokenSource.CreateLinkedTokenSource(signal.Token);
    canceller.CancelAfter(TimeSpan.FromSeconds(10));
    await CmdProc.ExecAsync("ping", new[] { "localhost", }, stdOut: tee, cancelToken: canceller.Token);
});
