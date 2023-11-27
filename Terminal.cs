namespace CheetahTerminal;

#region Using Statements
using System;
using System.Threading;
using CheetahTerminal.Modules;
using CheetahUtils;
#endregion

public class Terminal
{
	public ScreenManager ScreenManager { get; private set; }
	public ModuleManager ModuleManager { get; private set; }

	private bool _isClosing;

	public Terminal(string[] args)
	{
		Log.Clear();
		Log.Write("CheetahTerminal v1.0");
		Log.Write($"AppData: {FolderPaths.LocalAppData}");
		Log.Write($"ProgramFiles: {FolderPaths.LocalAppData}");
		Log.Write($"Plugins: {FolderPaths.Plugins}");
		Console.CursorVisible = false;
		ScreenManager = new ScreenManager(this);
		ModuleManager = new ModuleManager(this);
	}

	public void Start()
	{
		Console.TreatControlCAsInput = true;
		Console.CancelKeyPress += (sender, e) =>
		{
			e.Cancel = true;
		};

		ScreenManager.Start();
		ModuleManager.Start();

		Console.Title = $"CTerm: {ScreenManager.CurrentID}";

#if DEBUG && WINDOWS
#pragma warning disable CA1416 // Validate platform compatibility
		Console.Title += " (D)";
#pragma warning restore CA1416 // Validate platform compatibility
#endif

		while (true)
		{
			if (_isClosing) break;
			Update();
			Thread.Sleep(1);
		}
	}

	private DateTime lastUpdate = DateTime.MinValue;
	public void Update()
	{
		if (Console.KeyAvailable)
		{
			ScreenManager.HandleKeyPress();
			ScreenManager.Draw();
			lastUpdate = DateTime.Now;
		}

		if (DateTime.Now - lastUpdate > TimeSpan.FromSeconds(1))
		{
			ScreenManager.Draw();
			lastUpdate = DateTime.Now;
		}
	}

	public void Close()
	{
		_isClosing = true;
		ScreenManager.Close();
		ModuleManager.Close();
	}
}