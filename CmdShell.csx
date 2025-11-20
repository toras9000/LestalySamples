#r "nuget: Lestaly.General, 0.112.0"
#nullable enable
using System.Threading;
using Lestaly;

// ShellExecute で実行
{
    await CmdShell.ExecAsync("https://github.com/toras9000/LestalySamples");
}
