#r "nuget: Lestaly, 0.81.0"
#nullable enable
using System.Threading;
using Lestaly;

using (var canceller = new CancellationTokenSource(15 * 1000))
{
    try
    {
        Console.Write("行を入力するか15秒経過すると次へ進む。\ninput1: ");
        var input = await ConsoleWig.InReader.ReadLineAsync(canceller.Token);
        Console.WriteLine($"Echo: {input}");
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("cancelled");
    }
}

Console.WriteLine();

using (var signal = new SignalCancellationPeriod())
{
    try
    {
        Console.Write("行を入力するかCtrl+Cを押下すると次へ進む。\ninput2: ");
        var input = await ConsoleWig.InReader.ReadLineAsync(signal.Token);
        if (input == null)
        {
            input = "null";
            Console.WriteLine();
        }
        Console.WriteLine($"Echo: {input}");
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("cancelled");
    }
}

if (!Console.IsInputRedirected)
{
    Console.WriteLine();
    Console.WriteLine("(press any key to exit)");
    Console.ReadKey(intercept: true);
}
