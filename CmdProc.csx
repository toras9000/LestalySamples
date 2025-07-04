#r "nuget: Lestaly, 0.100.0"
#nullable enable
using System.Threading;
using Lestaly;

// これと同じ事は Lestaly.Cx 名前空間でできるので直接使う必要はあまりないかもしれない。

// コマンド呼び出し
{
    (var code, var output) = await CmdProc.RunAsync("curl", new[] { "http://localhost", });
}

// コマンド呼び出し(キャンセルあり)
{
    var canceller = new CancellationTokenSource();
    canceller.CancelAfter(TimeSpan.FromSeconds(10));
    await CmdProc.ExecAsync("ping", new[] { "localhost", }, stdOut: Console.Out, cancelToken: canceller.Token);
}
