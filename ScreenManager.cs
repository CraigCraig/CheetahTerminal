namespace CheetahTerminal;

public class ScreenManager(Terminal terminal)
{
    public Screen CurrentScreen { get; } = new(terminal, 0);

    public int CurrentID => CurrentScreen.Id;

    internal void HandleKeyPress() => CurrentScreen.HandleKeyPress();

    internal void Draw() => CurrentScreen.Draw();

    internal void Start() => CurrentScreen.Draw();

    internal void Close() => CurrentScreen.Close();
}
