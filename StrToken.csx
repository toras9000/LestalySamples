#r "nuget: Lestaly, 0.100.0"
#nullable enable
using Lestaly;

return await Paved.ProceedAsync(async () =>
{
    await Task.CompletedTask;
    var text = "abc,def,,ghi,jkl";
    var scan = text.AsSpan();
    while (!scan.IsEmpty)
    {
        var token = scan.TakeSkipToken(out scan, ',');
        WriteLine($"Token '{token}'");
    }
});
