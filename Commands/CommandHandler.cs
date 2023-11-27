namespace CheetahTerminal.Commands;

#region Using Statements
using System.Collections.Generic;
using Module = Modules.Module;
#endregion

public class CommandHandler(Terminal terminal, Module module)
{
    private readonly Terminal _terminal = terminal;
    public readonly Module Module = module;
    private readonly List<Command> _commands = [];

    public void Start()
    {
    }

    public void Stop()
    {
    }

    public CommandResult? HandleCommand(Screen screen, string command, string[] arguments)
    {
        if (string.IsNullOrEmpty(command))
        {
            return new CommandResult(false, "Command is null or empty");
        }

        foreach (var cmd in _commands)
        {
            if (cmd.Name == command)
            {
                return cmd.Execute(new CommandContext(Module, screen, command, arguments));
            }
        }

        return new CommandResult(false, $"Command not found: {command}");
    }

    public void AddCommand(Command command)
    {
        _commands.Add(command);
    }
}
