namespace CheetahTerminal;

using System;

internal static class InputHandler
{
	public static void Update()
	{
		if (!Console.KeyAvailable) return;
		ConsoleKeyInfo key = Console.ReadKey(true);
		if (key.Key == ConsoleKey.Escape)
		{
			Console.Clear();
			Console.WriteLine($"Do you really want to exit?{Environment.NewLine}Hit escape to confirm, any other key to abort.");
			key = Console.ReadKey(true);
			if (key.Key == ConsoleKey.Escape)
			{
			}
		}
	}
}