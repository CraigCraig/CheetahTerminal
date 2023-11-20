namespace CBash;

using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

public static class Installer
{
	public static async Task StartAsync(string path)
	{
		Console.WriteLine("Installing CBash..");

		var others = Process.GetProcessesByName("CBash");
		List<Task> tasks = [];
		foreach (var o in others)
		{
			Task task = Task.Run(() =>
			{
				if (o.Id != Environment.ProcessId)
				{
					Console.WriteLine($"Killing Old CBash: pid {o.Id}");
					o.Kill();
				}
			});
			tasks.Add(task);
		}
		await Task.WhenAll(tasks);

		Console.WriteLine($"Install Location: {path}");
		if (Directory.Exists(path))
		{
			Console.WriteLine("Removing Old CBash");
			Directory.Delete(path, true);
		}

		if (!Directory.Exists(path))
		{
			_ = Directory.CreateDirectory(path);
		}

		Console.WriteLine($"Moved: {Environment.CurrentDirectory} -> {path}");

		// TODO: Install CBash
		Console.WriteLine(await Shell.ExecuteAsync("dotnet", ["CBash.exe", "install"]));

		Console.WriteLine("Done Installing CBash!");
	}
}