#r "nuget: Lestaly, 0.47.0"
using Lestaly;
using Lestaly.Cx;

// Lestaly.Cx 名前空間で定義している拡張メソッドのサンプル。
// これは ProcessX.Zx にインスパイアされているが動作に互換はない。

// シンプルに外部コマンドを実行して出力を得る文字列への拡張メソッド
{
    // コマンドライン文字列に対して await することで外部コマンド呼び出し。
    // スペースがあればそれ以降を引数とみなす。
    // 標準出力と標準エラーは現在のコンソールにも出力。
    var output = await "cmd /c echo abc";
    Console.WriteLine($"output text: {output}");
}

// 上記のシンプルな呼び出しは、後で紹介の以下と等価。
{
    var output = await "cmd /c echo abc".result().success(0).output();
    Console.WriteLine($"output text: {output}");
}

// シンプル呼び出しは終了コードを検証する。
{
    // 終了コードがゼロ以外だと例外を送出する。
    try
    {
        await "cmd /c exit 1";
    }
    catch
    {
        Console.WriteLine("exit code is not zero");
    }
}

// 引数を別に指定する
{
    // args() で引数リストを指定。個々のパラメータが必要に応じてクォートされる。
    // 元の文字列はコマンド本体とみなす。(スペース区切りの引数などは指定不可)
    await "find".args("/N", "/I", "result1 = await", "Cx.csx");
}

// 単に実行して結果を得る。
{
    // 終了コードに関わらず例外は送出しない。
    var result = await "cmd /c exit 1".result();
    Console.WriteLine($"result code: {result.ExitCode}");
    Console.WriteLine($"result output: {result.Output}");

    // 追加で結果のコード/出力の必要な方を取得するメソッド。
    var code = await "cmd /c exit 1".result().code();
    var output = await "cmd /c exit 1".result().output();
}

// 明示的に正常な終了コード指定。
{
    // 正常とみなす終了コードを指定して、それ以外の場合は例外にする。
    var result = await "cmd /c exit 1".success(0, 1, 2);
    Console.WriteLine($"result code: {result.ExitCode}");
}
