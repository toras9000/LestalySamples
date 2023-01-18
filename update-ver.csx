#nullable enable
#r "nuget: Lestaly, 0.47.0"
using System.Text.RegularExpressions;
using Lestaly;

// ディレクトリ内スクリプトで参照しているパッケージのバージョンを指定されたバージョンに書き換える。
// vscode/C# エクステンションは、ワークスペース内のスクリプト間で参照バージョン不一致があるとインテリセンスが正しく動作しないため、バージョンを揃える必要がある。

var settings = new
{
    // 対象スクリプト検索ディレクトリ
    TargetDir = ThisSource.RelativeDirectory("./"),

    // 更新するパッケージ名とバージョン
    Packages = new PackageVersion[]
    {
        new("Lestaly",                "0.47.0"),
    },
};

// パッケージバージョン情報
record PackageVersion(string Name, string Version);

return await Paved.RunAsync(config: o => o.AnyPause(), action: async () =>
{
    // パッケージ参照ディレクティブの検出正規表現
    var detector = new Regex(@"^\s*#\s*r\s+""\s*nuget\s*:\s*([a-zA-Z0-9_\-\.]+)(?:,| )\s*(\d+)([0-9\-\.]+)?\s*""");

    // アップデートするパッケージの辞書
    var versions = settings.Packages.ToDictionary(p => p.Name);

    // 対象ディレクトリ以下のスクリプトを検索
    foreach (var file in settings.TargetDir.EnumerateFiles("*.csx", SearchOption.AllDirectories))
    {
        Console.WriteLine(file.RelativePathFrom(settings.TargetDir, ignoreCase: true));

        // ファイル内容を読み取り
        var lines = await file.ReadAllLinesAsync();

        // ファイル内容のパッケージ参照行を書き換える
        var updated = false;
        for (var i = 0; i < lines.Length; i++)
        {
            // パッケージ参照ディレクティブの検出。無関係ならスキップ
            var line = lines[i];
            var match = detector.Match(line);
            if (!match.Success) continue;

            // パッケージが更新対象かどうかを判断。対象でなければスキップ
            var pkgName = match.Groups[1].Value;
            if (!versions.TryGetValue(pkgName, out var package))
            {
                Console.WriteLine($"  Skip: {pkgName} - Not update target");
                continue;
            }

            // バージョン更新の必要があるかを判定。不要ならばスキップ
            var pkgVer = match.Groups[2].Value + match.Groups[3].Value;
            if (pkgVer == package.Version)
            {
                Console.WriteLine($"  Skip: {pkgName} - Already in version");
                continue;
            }

            // 参照ディレクティブの置き換え
            var additional = line[(match.Index + match.Length)..];
            var newLine = @$"#r ""nuget: {pkgName}, {package.Version}""{additional}";
            lines[i] = newLine;

            // 更新ありフラグ
            updated = true;
            Console.WriteLine($"  Update: {pkgName} {pkgVer} -> {package.Version}");
        }

        // 更新が必要な場合はファイルに書き戻す。
        if (updated)
        {
            await file.WriteAllLinesAsync(lines);
        }
    }

});
