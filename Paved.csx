#r "nuget: Lestaly, 0.62.0"
#r "nuget: Kokuban, 0.2.0"
#nullable enable
using Kokuban;
using Lestaly;

// 例外を補足する補助メソッド。スクリプトが終了してしまわないようにして表示するのが主な目的。
return await Paved.RunAsync(config: o => o.AnyPause(), action: async () =>
{
    // 例外を送出するような任意の処理。
    await Task.CompletedTask;
    var input = ConsoleWig.Write("Command:").ReadLine();
    switch (input.ToLowerInvariant())
    {
        case "error": throw new PavedMessageException("!!error message!!");
        case "cancel": throw new OperationCanceledException();
        default: WriteLine(Chalk.Yellow["no op."]); break;
    }
});
