#r "nuget: Lestaly, 0.48.0"
#nullable enable
using Lestaly;

// スクリプトに直接書きたくない情報をスクランブルして保存しておくようなもの。
// セキュリティ上の意味は薄いので注意。ただ平文を避けたいというだけのもの。

// 任意のデータ型
record Token(string Text, DateTime Time);

return await Paved.RunAsync(config: o => o.AnyPause(), action: async () =>
{
    // 保存先ファイル
    var storeFile = ThisSource.RelativeFile("RoughScrambler.bin");

    // 任意のデータに適当なスクランブルをかけて保存するクラス
    // デフォルトではこのファイルのパスをスクランブル時のキーに使う。
    var scrambler = new RoughScrambler();

    // 既存の保存データがあるか、デコードしてみる。
    var restored = await scrambler.DescrambleObjectFromFileAsync<Token>(storeFile);
    if (restored == null)
    {
        // 情報復元されなかった。
        // 保存するテキストを入力させて、データに詰めてスクランブル保存する。
        var input = ConsoleWig.Write("Input Text:").ReadLine().CancelIfEmpty();
        var token = new Token(input, DateTime.Now);
        await scrambler.ScrambleObjectToFileAsync(storeFile, token);
        Console.WriteLine("Saved.");
    }
    else
    {
        // 保存されていた情報を表示
        Console.WriteLine($"Restored: '{restored.Text}' ({restored.Time})");
    }
});
