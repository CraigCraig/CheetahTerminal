namespace CheetahTerminal;

using CheetahApp;

public class Terminal : ConsoleApp
{
    /// <summary>
    /// The core terminal application
    /// </summary>
    public static ConsoleApp? Core { get; private set; }

    public Terminal() : base(new(), new())
    {
        Core = this;
    }

    public override void Start()
    {
        base.Start();
        Core?.Start();
    }

    public override void Update()
    {
        base.Update();
        Core?.Update();
    }

    public override void Command(CommandContext context)
    {
        base.Command(context);
    }

    public override void Close()
    {
        base.Close();
        Core?.Close();
    }

    public static void Main(string[] args)
    {
        Core = new ConsoleApp(new(), new());
        Core.Start();
    }
}