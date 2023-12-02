namespace CheetahTerminal.Modules.Core.Commands;

using System.Text;
using CheetahTerminal.Commands;

public class Help() : Command("help", "this menu")
{
	public override CommandResult Execute(CommandContext context)
	{
		StringBuilder output = new();

		foreach (var module in ModuleManager.Modules)
		{
			output.Append($"Module: {module.Info.Name}{System.Environment.NewLine}");

			foreach (var command in module.Commands)
			{
				output.Append($"\tCommand: {command.Name}");
			}
		}

		return new CommandResult(true, output.ToString());
	}
}