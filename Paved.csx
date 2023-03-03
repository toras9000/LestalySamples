#r "nuget: Lestaly, 0.32.0"
using Lestaly;

return await Paved.RunAsync(configuration: o => o.AnyPause(), action: async () =>
{
    await Task.CompletedTask;
    var input = ConsoleWig.ReadLine("Command:");
    switch (input.ToLowerInvariant())
    {
        case "error": throw new PavedMessageException("!!error message!!");
        case "cancel": throw new OperationCanceledException();
        default: ConsoleWig.WriteLineColord(ConsoleColor.Yellow, "no op."); break;
    }
});
