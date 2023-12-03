#if DEBUG
namespace CheetahTerminal.Modules.Core.Commands;

using System.Text;
using CheetahTerminal.Commands;

public class Debug() : Command("debug", "debug command")
{
	public override CommandResult Execute(CommandContext context)
	{
		StringBuilder output = new();
		output.AppendLine("Debug: ");
		output.AppendLine($"Modules Count: {ModuleManager.ModuleCount}");
		output.AppendLine($"Commands Count: {ModuleManager.CommandCount}");

		return new CommandResult(true, output.ToString());
	}
}
#endif