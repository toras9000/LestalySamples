#r "nuget: Lestaly.General, 0.104.0"
#nullable enable
using System.Threading;
using Lestaly;

return await Paved.ProceedAsync(async () =>
{
    // 複数の TextWriter に同じ内容を出力する TeeWriter
    using var tee = new TeeWriter();
    tee.Bind(Console.Out);
    tee.Bind(ThisSource.RelativeFile("CmdTeeWriter.txt").CreateTextWriter());

    // TextWriter を利用可能な場面で使う。
    using var signal = new SignalCancellationPeriod();
    using var canceller = CancellationTokenSource.CreateLinkedTokenSource(signal.Token);
    canceller.CancelAfter(TimeSpan.FromSeconds(10));
    await CmdProc.ExecAsync("ping", ["localhost"], stdOut: tee, cancelToken: canceller.Token);
});
