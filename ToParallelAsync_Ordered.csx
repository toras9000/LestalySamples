#r "nuget: Lestaly, 0.48.0"
using System.Net.Http;
using Lestaly;

// IEnumerable の要素を並列に処理する IAsyncEnumerable に変換する ToParallelAsync() メソッド。

var randomer = new Random();
var numbers = Enumerable.Range(0, 10).Select(n => (idx: n, time: randomer.Next(500))).ToArray();
await foreach (var num in numbers.ToParallelAsync(parallels: 4, ordered: true, async n => { await Task.Delay(n.time); return n; }))
{
    Console.WriteLine($"[{num.idx}] Time {num.time} ms");
}
