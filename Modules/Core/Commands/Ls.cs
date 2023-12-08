﻿namespace CheetahTerminal.Modules.Core.Commands;

using System.IO;
using CheetahTerminal.Commands;

public class Ls() : Command("ls", "")
{
	public override CommandResult Execute(CommandContext context)
	{
		foreach (var entry in Directory.GetFileSystemEntries(Terminal.Environment.CurrentDirectory))
		{
			Terminal.Output.Add(entry);
		}
		return new CommandResult(true, "");
	}
}