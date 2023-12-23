#r "nuget: System.Text.Encoding.CodePages, 8.0.0"
#r "nuget: Lestaly, 0.53.0"
#nullable enable
using System.Runtime.InteropServices;
using System.Threading;
using Lestaly;
using Lestaly.Cx;

// Lestaly.Cx 名前空間で定義している拡張メソッドのサンプル。
// これは ProcessX.Zx にインスパイアされているが動作に互換はない。

// シンプルに外部コマンドを実行して出力を得る文字列への拡張メソッド
Console.WriteLine(">>Sample normal");
{
    // コマンドライン文字列に対して await することで外部コマンド呼び出し。
    // スペースがあればそれ以降を引数とみなす。
    // 標準出力と標準エラーは現在のコンソールにも出力。
    var output = await "cmd /c echo abc";
    Console.WriteLine($"output text: {output}");
}
Console.WriteLine();

// 上記のシンプルな呼び出しは、後で紹介の以下と等価。
Console.WriteLine(">>Sample normal (2)");
{
    var output = await "cmd /c echo abc".result().success(0).output();
    Console.WriteLine($"output text: {output}");
}
Console.WriteLine();

// シンプル呼び出しは終了コードを検証する。
Console.WriteLine(">>Sample exit code");
{
    // 終了コードがゼロ以外だと例外を送出する。
    try
    {
        await "cmd /c exit 1";
        Console.WriteLine("exit code is zero");
    }
    catch
    {
        Console.WriteLine("exit code is not zero");
    }
}
Console.WriteLine();

// 引数を別に指定する
Console.WriteLine(">>Sample args");
{
    // args() で引数リストを指定。個々のパラメータが必要に応じてクォートされる。
    // 元の文字列はコマンド本体とみなす。(スペース区切りの引数などは指定不可)
    await "cmd".args("/C", "echo", "%HOMEPATH%");
}
Console.WriteLine();

// 単に実行して結果を得る。
Console.WriteLine(">>Sample exec result");
{
    // 終了コードに関わらず例外は送出しない。
    var result = await "cmd /c exit 1".result();
    Console.WriteLine($"result code: {result.ExitCode}");
    Console.WriteLine($"result output: {result.Output}");

    // 追加で結果のコード/出力の必要な方を取得するメソッド。
    var code = await "cmd /c exit 1".result().code();
    var output = await "cmd /c exit 1".result().output();
}
Console.WriteLine();

// 明示的に正常な終了コード指定。
Console.WriteLine(">>Sample exit code (specify)");
{
    // 正常とみなす終了コードを指定して、それ以外の場合は例外にする。
    var result = await "cmd /c exit 1".success(0, 1, 2);
    Console.WriteLine($"result code: {result.ExitCode}");
}
Console.WriteLine();

// コンソール出力無しとする指定。
Console.WriteLine(">>Sample silent");
{
    // 現在のコンソールをアタッチしない
    var result = await "cmd /c echo AAA".silent();
    Console.WriteLine($"(no console output)");
    Console.WriteLine($"(result output is '{result.Output}')");
}
Console.WriteLine();

// 出力リダイレクト先を指定。
Console.WriteLine(">>Sample output redirect");
{
    var file = ThisSource.RelativeFile("test/Cx-out-redirect.txt").WithDirectoryCreate();
    var output1 = "";
    var output2 = "";
    using (var writer = file.CreateTextWriter())
    {
        output1 = await "cmd /c echo AAA".redirect(writer).result().output();
        output2 = await "cmd /c echo BBB".redirect(writer).result().output();
    }
    Console.WriteLine("RedirectFile:");
    Console.WriteLine(await file.ReadAllTextAsync());
    Console.WriteLine("OutputText:");
    Console.Write(output1);
    Console.Write(output2);
}
Console.WriteLine();

// 入力リダイレクト元を指定。
Console.WriteLine(">>Sample input redirect");
{
    var reader = new StringReader("input-test");
    var output = await "cmd /V:ON /C set /P TESTIN= & echo !TESTIN!".input(reader).result().output();
    Console.WriteLine($"Input-echo:{output}");
}
Console.WriteLine();

// 入力リダイレクト元を指定(文字列で)。
Console.WriteLine(">>Sample input redirect");
{
    var input = """
    The rabbit-hole went straight on like a tunnel for some way, and then dipped suddenly down,
    so suddenly that Alice had not a moment to think about stopping herself
    before she found herself falling down a very deep well. 
    """;
    var cmd = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "findstr" : "grep";
    var output = await cmd.args("moment").input(input).silent().result().output();
    Console.WriteLine($"Output:{output}");
}
Console.WriteLine();

// 入出力エンコーディングを指定。
Console.WriteLine(">>Sample encoding");
{
    await "cmd /C echo 日本語";
    var jpenc = CodePagesEncodingProvider.Instance.GetEncoding("Shift_JIS")!;
    await "cmd /C echo 日本語".encoding(jpenc);
}
Console.WriteLine();

// 環境変数を指定
Console.WriteLine(">>Sample environment");
{
    await "cmd /C echo test-%ENV1%-%ENV2%".env("ENV1", "aaa").env("ENV2", "bbb");
}
Console.WriteLine();

// 作業ディレクトリを指定
Console.WriteLine(">>Sample workdir");
{
    var cd = await "cmd /C echo %CD%".workdir(ThisSource.RelativeDirectory("test"));
    Console.WriteLine($"Print:{cd.Output}");
}
Console.WriteLine();

// 中止トークンを指定
Console.WriteLine(">>Sample cancel");
try
{
    using var canceller = new CancellationTokenSource();
    canceller.CancelAfter(3000);

    var jpenc = CodePagesEncodingProvider.Instance.GetEncoding("Shift_JIS")!;
    await "ping -t localhost".encoding(jpenc).killby(canceller.Token);
}
catch (CmdProcCancelException)
{
    Console.WriteLine($"Cancelled");
}
Console.WriteLine();

// 組み合わせて指定
Console.WriteLine(">>Sample environment");
try
{
    var file = ThisSource.RelativeFile("test/Cx-combine.txt").WithDirectoryCreate();
    using var canceller = new CancellationTokenSource(3000);
    using var writer = file.CreateTextWriter();

    var jpenc = CodePagesEncodingProvider.Instance.GetEncoding("Shift_JIS")!;
    await "cmd /C ping %TARGETHOST%"
        .encoding(jpenc)
        .env("TARGETHOST", "localhost")
        .redirect(writer)
        .killby(canceller.Token);
}
catch (CmdProcCancelException)
{
    Console.WriteLine($"Cancelled");
}
Console.WriteLine();



