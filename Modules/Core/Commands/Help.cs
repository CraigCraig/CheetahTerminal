namespace CheetahTerminal.Modules.Core.Commands;

using System.Text;
using CheetahTerminal.Commands;

public class Help() : Command("help", "this menu")
{
	public override CommandResult Execute(CommandContext context)
	{
		return new CommandResult(true, new StringBuilder().AppendLine("Commands:").ToString());
	}
}