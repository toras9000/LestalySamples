#r "nuget: Lestaly, 0.84.0"
#r "nuget: Kokuban, 0.2.0"
#nullable enable
using Kokuban;
using Lestaly;

// 例外を補足する補助メソッド。スクリプトが終了してしまわないようにして表示するのが主な目的。
return await Paved.ProceedAsync(async () =>
{
    // 例外を送出するような任意の処理。
    await Task.CompletedTask;
    Console.Write("Command:");
    var input = Console.ReadLine().CancelIfWhite();
    switch (input.ToLowerInvariant())
    {
        case "error": throw new PavedMessageException("!!error message!!");
        case "cancel": throw new OperationCanceledException();
        default: WriteLine(Chalk.Yellow["no op."]); break;
    }
});
