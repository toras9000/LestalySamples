#r "nuget: Lestaly, 0.53.0"
#nullable enable
using System.Threading;
using Lestaly;

// ShellExecute で実行
{
    await CmdShell.ExecAsync("https://github.com/toras9000/LestalySamples");
}
