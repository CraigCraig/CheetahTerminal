namespace CheetahTerminal.Modules.Core.Commands;

using CheetahTerminal.Commands;
using System;

public class Exit() : Command("exit", "quit the program")
{
	public override CommandResult Execute(CommandContext context)
	{
		Console.WriteLine("Exiting...");
		Environment.Exit(0);
		return new CommandResult(true);
	}
}