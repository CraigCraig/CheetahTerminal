namespace CheetahTerminal;
public static class Program
{
    public static Terminal? Terminal { get; private set; }

    private static void Main(string[] args)
    {
        new Terminal().Start();
    }
}