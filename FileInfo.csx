#r "nuget: Lestaly.General, 0.104.0"
#nullable enable
using Lestaly;

record TestData(string Text, double Value);

var testDir = CurrentDir.RelativeDirectory("test").WithCreate();

WriteLine("オブジェクトをJSONファイルへ(型指定)");
{
    var testJson = testDir.RelativeFile("a.json");
    await testJson.WriteJsonAsync(new TestData("AAA", 1.25));
    var load = await testJson.ReadJsonAsync<TestData>() ?? throw new Exception("cannot read");
    WriteLine($"load: Text={load.Text}, Value={load.Value}");
    WriteLine();
}

WriteLine("オブジェクトをJSONファイルへ(匿名型)");
{
    var testJson = testDir.RelativeFile("b.json");
    var data = new { id = 1, name = "abc", args = new { time = DateTime.Now, numbers = new int[] { 1, 2, 3, 5, 7, }, }, };
    await testJson.WriteJsonAsync(data);
    var tmpl = new { id = default(int), name = default(string), args = new { time = default(DateTime), numbers = default(int[]), }, };
    var load = await testJson.ReadJsonByTemplateAsync(tmpl) ?? throw new Exception("cannot read");
    WriteLine($"load: Text={load.id}, Value={load.name}, arg.time={load.args.time}");
    WriteLine();
}

WriteLine("改行コードを正規化して保存");
{
    var testText = testDir.RelativeFile("c.txt");
    testText.WriteMultilineText(['\r'], """
    生文字列リテラルで複数行テキストを作ると、
    改行コードがファイルの保存改行コードに依存してしまう。
    WriteMultilineText() は指定した改行コードに正規化する。
    """);
}
