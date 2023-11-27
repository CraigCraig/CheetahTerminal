namespace CheetahTerminal.Modules.Core.Commands;

using CheetahTerminal.Commands;

public class Info() : Command("info", "")
{
	public override CommandResult Execute(CommandContext context) => new(true, "Info");
}