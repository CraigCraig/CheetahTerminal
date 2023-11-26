namespace CheetahTerminal;

using System;
using CheetahTerminal.Modules;

public class Terminal
{
    public ScreenManager ScreenManager { get; private set; }
    public ModuleManager ModuleManager { get; private set; }

    private bool _isClosing = false;

    public Terminal()
    {
        Console.CursorVisible = true;
        ScreenManager = new ScreenManager(this);
        ModuleManager = new ModuleManager(this);
    }

    public void Start()
    {
        Console.TreatControlCAsInput = true;
        Console.Title = $"CTerm: {ScreenManager.CurrentID}";

        Console.CancelKeyPress += (sender, e) =>
        {
            e.Cancel = true;
        };

        ScreenManager.Start();
        ModuleManager.Start();

        while (true)
        {
            if (_isClosing) break;
            Update();
        }
    }

    public void Update()
    {
        if (Console.KeyAvailable)
        {
            ScreenManager.HandleKeyPress();
        }

        ScreenManager.Draw();
    }

    public void Close()
    {
        _isClosing = true;
        ScreenManager.Close();
        ModuleManager.Close();
    }
}