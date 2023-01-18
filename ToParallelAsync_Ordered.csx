#r "nuget: Lestaly, 0.27.0"
using System.Net.Http;
using Lestaly;

var randomer = new Random();
var numbers = Enumerable.Range(0, 10).Select(n => (idx: n, time: randomer.Next(500))).ToArray();
await foreach (var num in numbers.ToParallelAsync(parallels: 4, ordered: true, async n => { await Task.Delay(n.time); return n; }))
{
    Console.WriteLine($"[{num.idx}] Time {num.time} ms");
}
