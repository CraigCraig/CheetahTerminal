using CheetahTerminal.Commands;

namespace CheetahTerminal.Modules.Core.Commands;
public class Info() : Command("info", "")
{
    public override CommandResult Execute(CommandContext context)
    {
        return new CommandResult(true, "Info");
    }
}