#r "nuget: Lestaly.General, 0.104.0"
#nullable enable
using Lestaly;

{
    WriteLine("現在のソースファイルを基準とした情報取得");
    var thisFile = ThisSource.File();
    WriteLine($"This File: {thisFile.FullName}");
    var relativeFile = ThisSource.RelativeFile("./sub/sub2/file.txt");
    WriteLine($"Relative File: {relativeFile.FullName}");
    var relativeDir = ThisSource.RelativeDirectory("../other/any");
    WriteLine($"Relative Dir: {relativeDir.FullName}");
    WriteLine();
}

{
    WriteLine("カレントディレクトリ基準");
    var curRelDir = CurrentDir.RelativeDirectory("aaa/bbb");
    WriteLine($"Current Relative Dir: {curRelDir.FullName}");
    WriteLine();

    WriteLine("ディレクトリオブジェクト基準");
    var moreRelFile = curRelDir.RelativeFile("more/rel/file.txt");
    WriteLine($"More Relative Dir: {moreRelFile.FullName}");
    WriteLine();

    WriteLine("相対パス取得");
    var relPath = moreRelFile.RelativePathFrom(CurrentDir.RelativeDirectory("."), ignoreCase: true);
    WriteLine($"Relative Path: {relPath}");
    WriteLine();
}

{
    WriteLine("特殊ディレクトリ");
    var profileDir = SpecialFolder.UserProfile();
    WriteLine($"Profile Dir: {profileDir.FullName}");
    WriteLine();

    WriteLine("大文字小文字区別せず");
    var pkgCacheDir = profileDir.FindPathDirectory([".nuget", "packages", "Lestaly"], MatchCasing.CaseInsensitive);
    WriteLine($"Found Dir: {pkgCacheDir?.FullName}");
    WriteLine();
}

{
    WriteLine("ディレクトリ階層チェック");
    var thisDir = ThisSource.RelativeDirectory(".");
    WriteLine($"IsAncestorOf(aaa)     : {thisDir.IsAncestorOf(ThisSource.RelativeDirectory("aaa"))}");
    WriteLine($"IsAncestorOf(bbb/ccc) : {thisDir.IsAncestorOf(ThisSource.RelativeDirectory("bbb/ccc"))}");
    WriteLine($"IsAncestorOf(../aaa)  : {thisDir.IsAncestorOf(ThisSource.RelativeDirectory("../aaa"))}");
    WriteLine();
}

{
    WriteLine("メソッドチェーン内でついでにディレクトリ作る");
    var testDir = CurrentDir.RelativeDirectory("test").WithCreate();
    testDir.RelativeFile("xxx/a.txt").WithDirectoryCreate().Touch();
    await testDir.RelativeFile("xxx/b.txt").WithDirectoryCreate().WriteAllTextAsync("t-e-x-t");
    WriteLine($"...ディレクトリが無い場合は作成されたはず。");
    WriteLine();
}
