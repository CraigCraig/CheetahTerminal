namespace CheetahTerminal;
public static class Program
{
    public static Terminal? Terminal { get; private set; }

    private static void Main()
    {
        // TODO: Handle Command Line Arguments
        new Terminal().Start();
    }
}