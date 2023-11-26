namespace CheetahTerminal;

using System;

public class ScreenManager(Terminal terminal)
{
    public Screen CurrentScreen { get; } = new(terminal, 0);
    private readonly Terminal _terminal = terminal;

    public int CurrentID
    {
        get
        {
            return CurrentScreen.Id;
        }
    }


    internal void HandleKeyPress()
    {
        CurrentScreen.HandleKeyPress();
    }

    internal void Draw()
    {
        CurrentScreen.Draw();
    }

    internal void Start()
    {
        CurrentScreen.Output.Add("Try typing: help");
        CurrentScreen.Draw();
    }

    internal void Close()
    {
        // TODO: Handle this
        // _currentScreen.Close();
    }
}
