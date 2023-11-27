namespace CheetahTerminal.Modules;

using System;
using CheetahTerminal.Commands;

/// <summary>
/// Base class for all modules.
/// </summary>
/// <param name="name"></param>
/// <param name="description"></param>
public class Module
{
    public Terminal? Terminal { get; private set; }
    public bool IsCore { get; private set; } = false;
    public string Name { get; private set; }
    public CommandHandler? CommandHandler { get; private set; }

    public Module()
    {
        Name = GetType().Name;
    }

    public void SetTerminal(Terminal terminal)
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

    public void AddCommand(Command command)
    {
        if (CommandHandler == null) throw new NullReferenceException(nameof(CommandHandler));
        CommandHandler.AddCommand(command);
    }
}