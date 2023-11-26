namespace CheetahTerminal.Commands;

using CheetahTerminal.Modules;

public class CommandContext(Module _module, string name, string[] args)
{
    public Module Module { get; private set; } = _module;
    public string Name { get; private set; } = name;
    public string[] Args { get; private set; } = args;
}