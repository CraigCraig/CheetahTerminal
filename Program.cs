namespace CBash;

using System.Threading.Tasks;

public class CBash
{
	public CBash()
	{
	}
}

internal class Program
{
	static async Task Main(string[] rawArgs)
	{
		Console.Title = "CBash";
		Console.WriteLine("CBash");

		string command = string.Empty;
		string[] args = [];

		if (rawArgs.Length >= 2)
		{
			command = rawArgs[0];
			args = rawArgs.Skip(1).ToArray();
		}

		switch (command)
		{
			case "install":
				await Installer.StartAsync(args[0]);
				break;
		}

		while (true)
		{
			Console.Write("> ");
			string? input = Console.ReadLine();
			string[] cbRawArgs = input?.Split(' ') ?? [];

			if (input == null)
			{
				break;
			}

			string cbCommand = string.Empty;
			string[] cbArgs = [];

			if (cbRawArgs.Length >= 2)
			{
				cbCommand = cbRawArgs[0];
				cbArgs = cbRawArgs.Skip(1).ToArray();
			}

			switch (cbCommand)
			{
				case "exit":
					return;
				default:
					Console.WriteLine(await Shell.ExecuteAsync(cbCommand, cbArgs));
					break;
			}
		}
	}
}