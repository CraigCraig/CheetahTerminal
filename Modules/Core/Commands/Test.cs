namespace CheetahTerminal.Modules.Core.Commands;

using CheetahTerminal.Commands;

public class Test() : Command("test", "test")
{
	public override CommandResult Execute(CommandContext context) => new(true, "Commands Work");
}