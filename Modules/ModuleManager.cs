namespace CheetahTerminal.Modules;

#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
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
		var pluginsPath = FolderPaths.Plugins;
		foreach (var entry in Directory.GetFiles(pluginsPath))
		{
			if (entry.EndsWith(".plugin.dll"))
			{
				var assembly = Assembly.LoadFile(entry);
			}
		}

		// Load modules from all assemblies
		var assemblies = AppDomain.CurrentDomain.GetAssemblies();
		foreach (var assembly in assemblies)
		{
			var types = assembly.GetTypes();
			foreach (var type in types)
			{
				if (type.BaseType == null || type.BaseType.FullName == null) continue;
				if (type.BaseType.FullName.Equals(typeof(Module).FullName))
				{
					if (type == null || string.IsNullOrEmpty(type.FullName)) continue;
					if (assembly.FullName == null) continue;
					if (assembly.CreateInstance(type.FullName) is Module module)
					{
						module.SetTerminal(Terminal);
						Modules.Add(module);

						// Add Commands from Modules Namespace
						foreach (var type2 in types)
						{
							if (type2.BaseType == null || type2.BaseType.FullName == null) continue;
							if (!type2.BaseType.FullName.Equals(typeof(Command).FullName)) continue;
							{
								if (type2 == null || string.IsNullOrEmpty(type2.FullName)) continue;
								if (assembly.CreateInstance(type2.FullName) is not Command command) continue;
								module.AddCommand(command);
							}
						}
					}
				}
			}

			foreach (var module in Modules)
			{
				module.Start();
			}
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
			if (module.Info.Name == command)
			{
				return module;
			}
		}
		return null;
	}

	internal CommandResult? ExecuteCommand(Screen screen, string moduleName, string[] cmdArgs)
	{
		var module = GetModule(moduleName);

		if (module == null)
		{
			return new CommandResult(false, "Module Not Found");
		}

		var cmd = cmdArgs[0];
		var result = module.CommandHandler?.HandleCommand(screen, cmd, cmdArgs);
		return result;
	}
}
