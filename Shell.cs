namespace CBash;

using System.Text;
using CliWrap;

internal static class Shell
{
	/// <summary>
	/// Execute a command to the native shell.
	/// </summary>
	/// <returns></returns>
	public static async Task<string> ExecuteAsync(string command, string[] args)
	{
		StringBuilder error = new();
		StringBuilder output = new();

		try
		{
			var result = await Cli.Wrap(command)
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