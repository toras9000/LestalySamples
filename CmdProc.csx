#r "nuget: Lestaly, 0.31.0"
using System.Threading;
using Lestaly;

var result = CmdProc.CallAsync("dir");

(var code, var output) = await CmdProc.RunAsync("curl", new[] { "http://localhost", });

var canceller = new CancellationTokenSource();
canceller.CancelAfter(TimeSpan.FromSeconds(10));
await CmdProc.ExecAsync("ping", new[] { "localhost", }, stdOutWriter: Console.Out, cancelToken: canceller.Token);
