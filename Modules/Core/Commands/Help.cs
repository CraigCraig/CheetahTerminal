namespace CheetahTerminal.Modules.Core.Commands;

using System.Text;
using CheetahTerminal.Commands;

public class Help() : Command("help", "this menu")
{
    public override CommandResult Execute(CommandContext context)
    {
        var sb = new StringBuilder();

        sb.AppendLine("Commands:");

        return new CommandResult(true, sb.ToString());
    }
}