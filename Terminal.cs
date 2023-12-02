namespace CheetahTerminal;

#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CheetahUtils;
using Microsoft.Win32.SafeHandles;
#endregion

public static class Terminal
{
	public static string Version { get; } = typeof(Terminal).Assembly.GetName().Version?.ToString() ?? string.Empty;
	public static ShutdownRequest? ShutdownRequest { get; set; }
	internal static ConsoleUtils.CharInfo[] CharBuffer { get; set; } = new ConsoleUtils.CharInfo[80 * 25];
	internal static SafeFileHandle OutputHandle { get; set; } = ConsoleUtils.CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
	internal static ConsoleUtils.Rectangle Rect { get; set; } = new ConsoleUtils.Rectangle(0, 0, 80, 25);

	internal static void Initialize(string[] args)
	{
		Log.PrintToConsole = true;
		Log.Clear();
		Log.Write($"CheetahTerminal v{Version}");

		if (args.Length > 0)
		{
			return;
		}

		if (OutputHandle.IsInvalid) throw new Exception("outputHandle is invalid!");

		Start();
	}

	internal static void Start()
	{
		// Fill CharBuffer with empty spaces
		for (int i = 0; i < CharBuffer.Length; i++)
		{
			CharBuffer[i].Char = 'a';
			CharBuffer[i].Attributes = 1;
		}

		List<Task> tasks = [];

		tasks.Add(Task.Run(() =>
		{
			while (true)
			{
				if (ShutdownRequest != null) break;
				UpdateInput();
			}
		}));

		tasks.Add(Task.Run(() =>
		{
			while (true)
			{
				if (ShutdownRequest != null) break;
				UpdateBuffer();
				Thread.Sleep(1);
			}
		}));

		Task.WaitAll([.. tasks]);
	}

	internal static void UpdateInput()
	{
		ConsoleKeyInfo keyInfo = Console.ReadKey(true);
		ConsoleKey key = keyInfo.Key;

		if (key == ConsoleKey.Tab)
		{
			// Fill CharBuffer with random characters
			for (int i = 0; i < CharBuffer.Length; i++)
			{
				CharBuffer[i].Char = (char)Random.Shared.Next(32, 128);
				CharBuffer[i].Attributes = (short)Random.Shared.Next(0, 15);
			}
		}
	}

	internal static void UpdateBuffer()
	{
		ConsoleUtils.Rectangle rect = Rect;
		ConsoleUtils.WriteConsoleOutputW(OutputHandle, CharBuffer, new ConsoleUtils.Coord(80, 25), new ConsoleUtils.Coord(0, 0), ref rect);
	}

	internal static void Stop()
	{

	}

	//public void Start()
	//{
	//	Console.TreatControlCAsInput = true;
	//	Console.CancelKeyPress += (sender, e) =>
	//	{
	//		e.Cancel = true;
	//	};

	//	Rect = new ConsoleUtils.Rectangle(0, 0, 80, 25);

	//	Log.PrintToConsole = true;
	//	Log.Clear();
	//	Log.Write($"CheetahTerminal v{Version}");
	//	Log.Write($"AppData: {FolderPaths.LocalAppData}");
	//	Log.Write($"ProgramFiles: {FolderPaths.LocalAppData}");
	//	Log.Write($"Plugins: {FolderPaths.Plugins}");
	//	Console.CursorVisible = false;

	//	ModuleManager.Start();

	//	List<Task> tasks = [];

	//	tasks.Add(Task.Run(() =>
	//	{
	//		while (!_isClosing)
	//		{
	//			HandleInput();
	//			Thread.Sleep(1);
	//		}
	//	}));

	//	tasks.Add(Task.Run(() =>
	//	{
	//		while (!_isClosing)
	//		{
	//			HandleOutput();
	//			Thread.Sleep(1);
	//		}
	//	}));

	//	Task.WaitAll([.. tasks]);
	//}

	//public void HandleInput()
	//{
	//	InputHandler.Update();
	//	if (!Console.KeyAvailable) return;
	//	ConsoleKeyInfo key = Console.ReadKey(true);
	//	if (key.Key == ConsoleKey.Escape)
	//	{
	//		Console.Clear();
	//		Console.WriteLine($"Do you really want to exit?{Environment.NewLine}Hit escape to confirm, any other key to abort.");
	//		key = Console.ReadKey(true);
	//		if (key.Key == ConsoleKey.Escape)
	//		{
	//			Close();
	//		}
	//	}
	//}

	//public void HandleOutput()
	//{

	//}

	//public void Close()
	//{
	//	_isClosing = true;
	//	ModuleManager.Close();
	//}
}