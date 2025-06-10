#r "nuget: Lestaly, 0.84.0"
#nullable enable
using System.Threading;
using Lestaly;

using (var canceller = new CancellationTokenSource(15 * 1000))
{
    try
    {
        Write("行を入力するか15秒経過すると次へ進む。\ninput1: ");
        var input = await ConsoleWig.InReader.ReadLineAsync(canceller.Token);
        WriteLine($"Echo: {input}");
    }
    catch (OperationCanceledException)
    {
        WriteLine("cancelled");
    }
}

WriteLine();

using (var signal = new SignalCancellationPeriod())
{
    try
    {
        Write("行を入力するかCtrl+Cを押下すると次へ進む。\ninput2: ");
        var input = await ConsoleWig.InReader.ReadLineAsync(signal.Token);
        if (input == null)
        {
            input = "null";
            WriteLine();
        }
        WriteLine($"Echo: {input}");
    }
    catch (OperationCanceledException)
    {
        WriteLine("cancelled");
    }
}

if (!IsInputRedirected)
{
    WriteLine();
    WriteLine("(press any key to exit)");
    ReadKey(intercept: true);
}
