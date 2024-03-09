#r "nuget: Lestaly, 0.57.0"
#nullable enable
using Lestaly;

{
    Console.WriteLine("現在のソースファイルを基準とした情報取得");
    var thisFile = ThisSource.File();
    Console.WriteLine($"This File: {thisFile.FullName}");
    var relativeFile = ThisSource.RelativeFile("./sub/sub2/file.txt");
    Console.WriteLine($"Relative File: {relativeFile.FullName}");
    var relativeDir = ThisSource.RelativeDirectory("../other/any");
    Console.WriteLine($"Relative Dir: {relativeDir.FullName}");
    Console.WriteLine();
}

{
    Console.WriteLine("カレントディレクトリ基準");
    var curRelDir = CurrentDir.RelativeDirectory("aaa/bbb");
    Console.WriteLine($"Current Relative Dir: {curRelDir.FullName}");
    Console.WriteLine();

    Console.WriteLine("ディレクトリオブジェクト基準");
    var moreRelFile = curRelDir.RelativeFile("more/rel/file.txt");
    Console.WriteLine($"More Relative Dir: {moreRelFile.FullName}");
    Console.WriteLine();

    Console.WriteLine("相対パス取得");
    var relPath = moreRelFile.RelativePathFrom(CurrentDir.RelativeDirectory("."), ignoreCase: true);
    Console.WriteLine($"Relative Path: {relPath}");
    Console.WriteLine();
}

{
    Console.WriteLine("ディレクトリ階層チェック");
    var thisDir = ThisSource.RelativeDirectory(".");
    Console.WriteLine($"IsAncestorOf(aaa)     : {thisDir.IsAncestorOf(ThisSource.RelativeDirectory("aaa"))}");
    Console.WriteLine($"IsAncestorOf(bbb/ccc) : {thisDir.IsAncestorOf(ThisSource.RelativeDirectory("bbb/ccc"))}");
    Console.WriteLine($"IsAncestorOf(../aaa)  : {thisDir.IsAncestorOf(ThisSource.RelativeDirectory("../aaa"))}");
    Console.WriteLine();
}

{
    Console.WriteLine("メソッドチェーン内でついでにディレクトリ作る");
    var testDir = CurrentDir.RelativeDirectory("test").WithCreate();
    testDir.RelativeFile("xxx/a.txt").WithDirectoryCreate().Touch();
    await testDir.RelativeFile("xxx/b.txt").WithDirectoryCreate().WriteAllTextAsync("t-e-x-t");
    Console.WriteLine($"...ディレクトリが無い場合は作成されたはず。");
    Console.WriteLine();
}

record TestData(string Text, double Value);

{
    Console.WriteLine("オブジェクトをJSONファイルへ(型指定)");
    var testDir = CurrentDir.RelativeDirectory("test").WithCreate();
    var testJson = testDir.RelativeFile("a.json");
    await testJson.WriteJsonAsync(new TestData("AAA", 1.25));
    var load = await testJson.ReadJsonAsync<TestData>() ?? throw new Exception("cannot read");
    Console.WriteLine($"load: Text={load.Text}, Value={load.Value}");
    Console.WriteLine();
}

{
    Console.WriteLine("オブジェクトをJSONファイルへ(匿名型)");
    var testDir = CurrentDir.RelativeDirectory("test").WithCreate();
    var testJson = testDir.RelativeFile("b.json");
    var data = new { id = 1, name = "abc", args = new { time = DateTime.Now, numbers = new int[] { 1, 2, 3, 5, 7, }, }, };
    await testJson.WriteJsonAsync(data);
    var tmpl = new { id = default(int), name = default(string), args = new { time = default(DateTime), numbers = default(int[]), }, };
    var load = await testJson.ReadJsonByTemplateAsync(tmpl) ?? throw new Exception("cannot read");
    Console.WriteLine($"load: Text={load.id}, Value={load.name}, arg.time={load.args.time}");
    Console.WriteLine();
}
