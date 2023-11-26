namespace CheetahTerminal;

using System.Text;
using CliWrap;

/// <summary>
/// Class for the native host shell.
/// </summary>
public static class NativeShell
{
    public static string Execute(string command, string[] args, bool runElevated = false)
    {
        StringBuilder sb = new();
        var c = Cli.Wrap(command).WithArguments(args)
            .WithStandardErrorPipe(PipeTarget.ToDelegate((s) => sb.AppendLine(s)))
            .WithStandardOutputPipe(PipeTarget.ToDelegate((s) => sb.AppendLine(s)));

        var result = c.ExecuteAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        return sb.ToString();
    }
}