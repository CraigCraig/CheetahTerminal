﻿namespace CheetahTerminal;

public class ScreenManager(Terminal terminal)
{
    public int CurrentScreen { get; } = 1;
    public Screen[] Screens { get; } = [ new(terminal, 1), new(terminal, 2), new(terminal, 3) ];

    public int CurrentID => Screens[CurrentScreen - 1].Id;

    internal void HandleKeyPress() => Screens[CurrentScreen - 1].HandleKeyPress();

    internal void Draw() => Screens[CurrentScreen - 1].Draw();

    internal void Start() => Screens[CurrentScreen - 1].Draw();

    internal void Close() => Screens[CurrentScreen - 1].Close();
}
