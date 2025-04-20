#r "nuget: Lestaly, 0.74.0"
#nullable enable
using System.Net.Http;
using Lestaly;

// IEnumerable の要素を並列に処理する IAsyncEnumerable に変換する ToParallelAsync() メソッド。

var addrs = new[]
{
    "https://httpbin.org/status/200",
    "https://httpbin.org/status/300",
    "https://httpbin.org/status/400",
    "https://httpbin.org/status/401",
    "https://httpbin.org/status/402",
    "https://httpbin.org/status/403",
    "https://httpbin.org/status/404",
    "https://httpbin.org/status/500",
    "https://httpbin.org/status/501",
    "https://httpbin.org/status/502",
    "https://httpbin.org/status/503",
};

var client = new HttpClient();
var reqs = addrs.ToParallelAsync(parallels: 3, async a => await client.GetAsync(a));
await foreach (var req in reqs)
{
    Console.WriteLine($"{req.RequestMessage?.RequestUri} -> {req.StatusCode}");
}
