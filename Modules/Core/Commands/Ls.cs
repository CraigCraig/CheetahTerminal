namespace CheetahTerminal.Modules.Core.Commands;

using System.IO;
using System.Text;
using CheetahTerminal.Commands;

public class Ls() : Command("ls", "")
{
	public override CommandResult Execute(CommandContext context)
	{
		StringBuilder output = new();
		foreach (var entry in Directory.GetFileSystemEntries(Terminal.Environment.CurrentDirectory))
		{
			_ = output.AppendLine(entry);
		}
		return new CommandResult(true, output.ToString());
	}
}