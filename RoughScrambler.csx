#r "nuget: Lestaly, 0.32.0"
using Lestaly;

record Token(string Text, DateTime Time);

var storeFile = ThisSource.RelativeFile("RoughScrambler.bin");

var scrambler = new RoughScrambler();
var restored = await scrambler.DescrambleObjectFromFileAsync<Token>(storeFile);
if (restored == null)
{
    var input = ConsoleWig.ReadLine("Input Text:");
    if (input.IsEmpty())
    {
        Console.WriteLine("No input");
    }
    else
    {
        var token = new Token(input, DateTime.Now);
        await scrambler.ScrambleObjectToFileAsync(storeFile, token);
        Console.WriteLine("Saved.");
    }
}
else
{
    Console.WriteLine($"Restored: '{restored.Text}' ({restored.Time})");
}

Console.WriteLine($"(Press any key to exit.)");
Console.ReadKey(intercept: true);
