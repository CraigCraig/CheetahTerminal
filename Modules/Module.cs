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
	public ModuleInfo Info { get; private set; } = info;
	public CommandHandler? CommandHandler { get; private set; }

	internal void Initialize()
	{
		CommandHandler = new(this);
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