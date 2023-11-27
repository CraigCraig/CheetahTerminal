namespace CheetahTerminal.Modules;

using System;
using CheetahTerminal.Commands;

/// <summary>
/// Base class for all modules.
/// </summary>
/// <param name="name"></param>
/// <param name="description"></param>
public class Module(ModuleInfo info)
{
	public Terminal? Terminal { get; private set; }
	public ModuleInfo Info { get; private set; } = info;
	public CommandHandler? CommandHandler { get; private set; }

	internal void SetTerminal(Terminal terminal)
	{
		Terminal = terminal;
		CommandHandler = new(terminal, this);
	}

	public virtual void Start()
	{
		if (CommandHandler == null) throw new NullReferenceException(nameof(CommandHandler));
		CommandHandler.Start();
	}

	public virtual void Stop()
	{
		if (CommandHandler == null) throw new NullReferenceException(nameof(CommandHandler));
		CommandHandler.Stop();
	}

	internal void AddCommand(Command command)
	{
		if (CommandHandler == null) throw new NullReferenceException(nameof(CommandHandler));
		CommandHandler.AddCommand(command);
	}
}