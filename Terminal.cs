namespace CheetahTerminal;

#region Using Statements
using System;
using System.Threading;
#if DEBUG
using CheetahTerminal.Debugging;
#endif
using CheetahTerminal.Modules;
#endregion

public class Terminal
{
    public ScreenManager ScreenManager { get; private set; }
    public ModuleManager ModuleManager { get; private set; }

    private bool _isClosing;

    public Terminal()
    {
        Console.CursorVisible = false;
        ScreenManager = new ScreenManager(this);
        ModuleManager = new ModuleManager(this);
    }

    public void Start()
    {
        Console.TreatControlCAsInput = true;
        Console.CancelKeyPress += (sender, e) =>
        {
            e.Cancel = true;
        };

        ScreenManager.Start();
        ModuleManager.Start();

        Console.Title = $"CTerm: {ScreenManager.CurrentID}";

#if DEBUG
        if (Debug.Enabled)
        {
            Debug.Initialize();
        }
#endif

        while (true)
        {
            if (_isClosing) break;
            Update();
            Thread.Sleep(1);
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