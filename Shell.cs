namespace CBash;

using System.Text;
using CliWrap;

internal static class Shell
{
	/// <summary>
	/// Execute a command to the native shell.
	/// </summary>
	/// <returns></returns>
	public static async Task<string> ExecuteAsync(string command, string[] args, ShellType type = ShellType.Default)
	{
		var shell = string.Empty;

		if (type == ShellType.PowerShell)
		{
			shell = "pwsh.exe ";
		}

		StringBuilder error = new();
		StringBuilder output = new();

		// print test
		Console.WriteLine($"{shell}{command} {string.Join(" ", args)}");

		try
		{
			var result = await Cli.Wrap($"{shell}{command}")
				.WithArguments(args)
				.WithWorkingDirectory(Environment.CurrentDirectory)
				.WithStandardErrorPipe(PipeTarget.ToStringBuilder(error))
				.WithStandardOutputPipe(PipeTarget.ToStringBuilder(output))
				.WithValidation(CommandResultValidation.None)
				.ExecuteAsync();
		}
		catch (Exception ex)
		{
			return ex.Message;
		}

		return output.ToString();
	}
}