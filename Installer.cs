namespace CBash;

using System.Diagnostics;
using System.Threading.Tasks;

public static class Installer
{
	public static async Task StartAsync(string path)
	{
		Console.WriteLine("Installing CBash..");
		Stopwatch stopwatch = Stopwatch.StartNew();
		string binPath = Path.Combine(Environment.CurrentDirectory, "bin\\Release\\net8.0");

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
		Task.WaitAll([.. tasks]);

		Console.WriteLine($"Install Location: {path}");

		if (Directory.Exists(path))
		{
			var entries = Directory.GetFileSystemEntries(path, "*", new EnumerationOptions() { RecurseSubdirectories = true });

			foreach (var entry in entries)
			{
				if (File.Exists(entry))
				{
					File.Delete(entry);
					Console.WriteLine($"Deleted: {entry}");
				}
				else if (Directory.Exists(entry))
				{
					Directory.Delete(entry, true);
					Console.WriteLine($"Deleted: {entry}");
				}
			}
		}

		if (!Directory.Exists(path))
		{
			_ = Directory.CreateDirectory(path);
		}

		try
		{
			CopyFolder(binPath, path, true);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}

		Console.WriteLine(await Shell.ExecuteAsync("dotnet", ["CBash.exe", "install"]));
		stopwatch.Stop();
		Console.WriteLine("Done Installing CBash!");
		Console.WriteLine($"Time Elapsed: {stopwatch.ElapsedMilliseconds}ms");
		Console.WriteLine($"Program Path: {Path.Combine(path, "CBash.exe")}");
	}

	public static void CopyFolder(string source, string target, bool overwrite = false)
	{
		if (!Directory.Exists(target))
		{
			_ = Directory.CreateDirectory(target);
		}

		try
		{
			foreach (var file in Directory.GetFiles(source))
			{
				string fileName = Path.GetFileName(file);
				string destination = Path.Combine(target, fileName);
				File.Copy(file, destination, overwrite);
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}

		try
		{
			foreach (var directory in Directory.GetDirectories(source))
			{
				string dirName = Path.GetFileName(directory);
				string destination = Path.Combine(target, dirName);
				CopyFolder(directory, destination, overwrite);
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
	}
}