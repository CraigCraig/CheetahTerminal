namespace CheetahTerminal;

using System;
using System.Text;

public partial class Screen
{
    public int Id;
    private readonly Terminal terminal;

    private readonly StringBuilder input = new();

    public string CurrentDirectory = string.Empty;

    public readonly LineArea Header = new(0);
    //public readonly DrawArea Output = new(new(), new());
    public readonly LineArea Prompt = new(1, true);

    public Vector2i CursorInputPosition = new();

    private int width = Console.BufferWidth;
    private int height = Console.BufferHeight;

    public Screen(Terminal _terminal, int _id)
    {
        Id = _id;
        terminal = _terminal;
        if (string.IsNullOrEmpty(CurrentDirectory))
        {
            CurrentDirectory = Environment.CurrentDirectory;
        }
    }

    internal void HandleKeyPress()
    {
        Console.SetCursorPosition(CursorInputPosition.X, CursorInputPosition.Y);

        var keyInfo = Console.ReadKey();
        var key = keyInfo.Key;
        var keyChar = keyInfo.KeyChar;

        if (key == ConsoleKey.UpArrow)
        {
            return;
        }

        if (keyInfo.Modifiers == ConsoleModifiers.Control)
        {
            switch (key)
            {
                case ConsoleKey.D1:
                    return;
                    // TODO: Handle D2 - D9
            }
        }

        if (key == ConsoleKey.Backspace)
        {
            if (input.Length > 0)
            {
                input.Remove(input.Length - 1, 1);
                return;
            }
        }

        if (key == ConsoleKey.Escape)
        {
            return;
        }

        if (key == ConsoleKey.Tab)
        {
            return;
        }

#if WINDOWS
        if (key == ConsoleKey.F1)
        {
#pragma warning disable CA1416 // Validate platform compatibility
            Console.CursorVisible = !Console.CursorVisible;
#pragma warning restore CA1416 // Validate platform compatibility
            return;
        }
#endif

        if (key == ConsoleKey.Enter)
        {
            if (input.Length == 0) { return; }
            //Output.Add(_input.ToString());

            var module = input.ToString().Split(' ')[0];
            var lines = input.ToString().Split(' ')[1..];
            var result = terminal.ModuleManager.ExecuteCommand(this, module, lines);
            //Output.Add($"{result?.Message}");
            input.Clear();
            return;
        }

        input.Append(keyChar);
        Prompt.Redraw();
    }

    internal void Draw()
    {
        // TODO: Add different screen layouts

        if (width != Console.BufferWidth || height != Console.BufferHeight)
        {
            width = Console.BufferWidth;
            height = Console.BufferHeight;
        }

        // Draw Header
        Header.Text = $"CTerm: {Id} - {DateTime.Now}";
        Header.Draw();

        // Draw Output Area
        //OutputArea.Draw();

        // Draw Prompt
        Prompt.Text = $"{CurrentDirectory} > {input}";
        Prompt.Draw();

        CursorInputPosition.X = Prompt.area.Position.X + Prompt.Text.Length + 1;
        CursorInputPosition.Y = Prompt.area.Position.Y + 1;
    }

    internal void Close()
    {
        throw new NotImplementedException();
    }
}