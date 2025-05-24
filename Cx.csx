#r "nuget: System.Text.Encoding.CodePages, 9.0.5"
#r "nuget: Lestaly, 0.82.0"
#nullable enable
using System.Runtime.InteropServices;
using System.Threading;
using Lestaly;
using Lestaly.Cx;

// Lestaly.Cx 名前空間で定義している拡張メソッドのサンプル。
// これは ProcessX.Zx にインスパイアされているが動作に互換はない。

// シンプルに外部コマンドを実行して出力を得る文字列への拡張メソッド
{
    // コマンドライン文字列に対して await することで外部コマンド呼び出し。
    // スペースがあればそれ以降を引数とみなす。
    // 標準出力と標準エラーは現在のコンソールにも出力。
    WriteLine(">>Sample normal");
    var output = await "cmd /c echo abc";
    WriteLine($"output text: {output}");
    WriteLine();
}

// 上記のシンプルな呼び出しは、後で紹介の以下と等価。
{
    WriteLine(">>Sample normal (2)");
    var output = await "cmd /c echo abc".result().success(0).output();
    WriteLine($"output text: {output}");
    WriteLine();
}

// シンプル呼び出しは終了コードを検証する。
{
    // 終了コードがゼロ以外だと例外を送出する。
    try
    {
        WriteLine(">>Sample exit code");
        await "cmd /c exit 1";
        WriteLine("exit code is zero");
    }
    catch
    {
        WriteLine("exit code is not zero");
    }
    WriteLine();
}

// 引数を別に指定する
{
    // args() で引数リストを指定。個々のパラメータが必要に応じてクォートされる。
    // 元の文字列はコマンド本体とみなす。(スペース区切りの引数などは指定不可)
    WriteLine(">>Sample args");
    await "cmd".args("/C", "echo", "%HOMEPATH%");
    WriteLine();
}

// FileInfo などを引数に指定する
{
    // args() に FileInfo/DirectoryInfo を指定した場合はそのフルパスと解釈する。
    // あと echo() で呼び出しコマンドラインをエコーする。
    WriteLine(">>Sample args file");
    var file = ThisSource.RelativeFile(".gitignore");
    await "cmd".args("/C", "type", file).echo();
    WriteLine();
}

// 単に実行して結果を得る。
{
    // 終了コードに関わらず例外は送出しない。
    WriteLine(">>Sample exec result");
    var result = await "cmd /c exit 1".result();
    WriteLine($"result code: {result.ExitCode}");
    WriteLine($"result output: {result.Output}");

    // 追加で結果のコード/出力の必要な方を取得するメソッド。
    var code = await "cmd /c exit 1".result().code();
    var output = await "cmd /c exit 1".result().output();
    WriteLine();
}

// 明示的に正常な終了コード指定。
{
    // 正常とみなす終了コードを指定して、それ以外の場合は例外にする。
    WriteLine(">>Sample exit code (specify)");
    var result = await "cmd /c exit 1".success(0, 1, 2);
    WriteLine($"result code: {result.ExitCode}");
    WriteLine();
}

// コンソール出力無しとする指定。
{
    // 現在のコンソールをアタッチしない
    WriteLine(">>Sample silent");
    var result = await "cmd /c echo AAA".silent();
    WriteLine($"(no console output)");
    WriteLine($"(result output is '{result.Output}')");
    WriteLine();
}

// 出力リダイレクト先を指定。
{
    WriteLine(">>Sample output redirect");
    var file = ThisSource.RelativeFile("test/Cx-out-redirect.txt").WithDirectoryCreate();
    var output1 = "";
    var output2 = "";
    using (var writer = file.CreateTextWriter())
    {
        output1 = await "cmd /c echo AAA".redirect(writer).result().output();
        output2 = await "cmd /c echo BBB".redirect(writer).result().output();
    }
    WriteLine("RedirectFile:");
    WriteLine(await file.ReadAllTextAsync());
    WriteLine("OutputText:");
    Write(output1);
    Write(output2);
    WriteLine();
}

// 入力リダイレクト元を指定。
{
    WriteLine(">>Sample input redirect");
    var reader = new StringReader("input-test");
    var output = await "cmd /V:ON /C set /P TESTIN= & echo !TESTIN!".input(reader).result().output();
    WriteLine($"Input-echo:{output}");
    WriteLine();
}

// 入力リダイレクト元を指定(文字列で)。
{
    WriteLine(">>Sample input redirect");
    var input = """
    The rabbit-hole went straight on like a tunnel for some way, and then dipped suddenly down,
    so suddenly that Alice had not a moment to think about stopping herself
    before she found herself falling down a very deep well. 
    """;
    var cmd = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "findstr" : "grep";
    var output = await cmd.args("moment").input(input).silent().result().output();
    WriteLine($"Output:{output}");
    WriteLine();
}

// 入出力エンコーディングを指定。
{
    WriteLine(">>Sample encoding");
    await "cmd /C echo 日本語";
    var jpenc = CodePagesEncodingProvider.Instance.GetEncoding("Shift_JIS")!;
    await "cmd /C echo 日本語".encoding(jpenc);
    WriteLine();
}

// 環境変数を指定
{
    WriteLine(">>Sample environment");
    await "cmd /C echo test-%ENV1%-%ENV2%".env("ENV1", "aaa").env("ENV2", "bbb");
    WriteLine();
}

// 作業ディレクトリを指定
{
    WriteLine(">>Sample workdir");
    var cd = await "cmd /C echo %CD%".workdir(ThisSource.RelativeDirectory("test"));
    WriteLine($"Print:{cd.Output}");
    WriteLine();
}

// 中止トークンを指定
{
    WriteLine(">>Sample cancel");
    try
    {
        using var canceller = new CancellationTokenSource();
        canceller.CancelAfter(3000);

        var jpenc = CodePagesEncodingProvider.Instance.GetEncoding("Shift_JIS")!;
        await "ping -t localhost".encoding(jpenc).killby(canceller.Token);
    }
    catch (CmdProcCancelException)
    {
        WriteLine($"Cancelled");
    }
    WriteLine();
}

// 組み合わせて指定
{
    WriteLine(">>Sample environment");
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
        WriteLine($"Cancelled");
    }
    WriteLine();
}


