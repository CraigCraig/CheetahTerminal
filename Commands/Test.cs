namespace CheetahTerminal.Commands;

using System;
using CheetahApp;
using CheetahApp.Commands;

public class Test() : Command("test", "test commands")
{
    public override void Execute(CommandContext context)
    {
        Console.WriteLine("Commands Working");
    }
}