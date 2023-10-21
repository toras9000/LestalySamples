#r "nuget: Lestaly, 0.48.0"
using System.Threading;
using Lestaly;

using (var canceller = new CancellationTokenSource(15 * 1000))
{
    try
    {
        ConsoleWig.Write("行を入力するか15秒経過すると次へ進む。\ninput1: ");
        var input = await ConsoleWig.InReader.ReadLineAsync(canceller.Token);
        ConsoleWig.WriteLine($"Echo: {input}");
    }
    catch (OperationCanceledException)
    {
        ConsoleWig.WriteLine("cancelled");
    }
}

Console.WriteLine();

using (var signal = ConsoleWig.CreateCancelKeyHandlePeriod())
{
    try
    {
        ConsoleWig.Write("行を入力するかCtrl+Cを押下すると次へ進む。\ninput2: ");
        var input = await ConsoleWig.InReader.ReadLineAsync(signal.Token);
        if (input == null)
        {
            input = "null";
            ConsoleWig.WriteLine();
        }
        ConsoleWig.WriteLine($"Echo: {input}");
    }
    catch (OperationCanceledException)
    {
        ConsoleWig.WriteLine("cancelled");
    }
}

if (!Console.IsInputRedirected)
{
    Console.WriteLine();
    Console.WriteLine("(press any key to exit)");
    Console.ReadKey(intercept: true);
}
