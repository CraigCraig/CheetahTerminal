namespace CheetahTerminal.Modules;

#region Using Statements
using System;
using System.Collections.Generic;
using System.Reflection;
using CheetahTerminal.Commands;
#endregion

public class ModuleManager(Terminal terminal)
{
    public Terminal Terminal { get; } = terminal;
    public List<Module> Modules { get; private set; } = [];

    public void Start()
    {
        // TODO: Load modules from DLLs

        // Load modules from this assembly
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes();
        foreach (var type in types)
        {
            if (type.BaseType == null || type.BaseType.FullName == null) continue;
            if (type.BaseType.FullName.Equals(nameof(Module)))
            {
                if (type == null || string.IsNullOrEmpty(type.FullName)) continue;
                if (assembly.FullName == null) continue;
                if (assembly.CreateInstance(type.FullName) is Module module)
                {
                    module.SetTerminal(Terminal);
                    Modules.Add(module);
#if DEBUG
                    Terminal.ScreenManager.CurrentScreen.Output.Add($"Module Loaded: {module.Name}");
#endif

                    // Add Commands from Modules Namespace
                    var commands = type.GetNestedTypes();

                    foreach (var test in commands)
                    {
                        Terminal.ScreenManager.CurrentScreen.Output.Add($"{test}");
                    }
                }
            }
        }

        foreach (var module in Modules)
        {
            module.Start();
        }
    }

    public void Close()
    {
        foreach (var module in Modules)
        {
            module.Stop();
        }
    }

    public Module? GetModule(string command)
    {
        foreach (var module in Modules)
        {
            if (module.Name == command)
            {
                return module;
            }
        }
        return null;
    }

    internal CommandResult? ExecuteCommand(string moduleName, string[] cmdArgs)
    {
        var module = GetModule(moduleName);

        if (module == null)
        {
            return new CommandResult(false, "Module Not Found");
        }

        var cmd = cmdArgs[0];
        var result = module.CommandHandler.HandleCommand(cmd, cmdArgs);
        return result;
    }
}
