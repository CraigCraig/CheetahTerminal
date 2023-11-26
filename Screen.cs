namespace CheetahTerminal;

using System;
using System.Text;

public class Screen(Terminal terminal, int id)
{
    public int Id = id;
    private readonly Terminal _terminal = terminal;
    private bool _isDirty = true;

    private readonly StringBuilder _input = new();
    private StringBuilder _last_input = new();

    private readonly string _currentDirectory = Environment.CurrentDirectory;

    public readonly ConsoleBuffer Header = new(DrawDirection.Down);
    public readonly ConsoleBuffer Output = new(DrawDirection.Up);

    private int width = Console.BufferWidth;
    private int height = Console.BufferHeight;

    internal void HandleKeyPress()
    {
        var keyInfo = Console.ReadKey();
        var key = keyInfo.Key;
        var keyChar = keyInfo.KeyChar;

        if (key == ConsoleKey.UpArrow)
        {
            Output.Add($"History not functional yet");
            return;
        }

        if (keyInfo.Modifiers == ConsoleModifiers.Control)
        {
            switch (key)
            {
                case ConsoleKey.D1:
                    Output.Add("Screen switching not functional yet.");
                    return;
            }
        }

        if (key == ConsoleKey.Backspace)
        {
            _isDirty = true;
            if (_input.Length > 0)
            {
                _input.Remove(_input.Length - 1, 1);
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

        if (key == ConsoleKey.Enter)
        {
            _isDirty = true;
            if (_input.Length == 0) { return; }
            Output.Add(_input.ToString());

            var cmd = _input.ToString().Split(' ')[0];
            var args = _input.ToString().Split(' ')[1..];
            var result = _terminal.ModuleManager.ExecuteCommand(cmd, args);
            Output.Add($"{result?.Message}");
            _input.Clear();
            return;
        }

        _input.Append(keyChar);

        if (!_last_input.Equals(_input))
        {
            _last_input = _input;
            _isDirty = true;
        }
    }

    internal void Draw()
    {
        if (Output.IsDirty)
        {
            _isDirty = true;
            Output.IsDirty = false;
        }
        if (!_isDirty) { return; }
        _isDirty = false;

        Console.Clear();

        // Display Header - Pinned To Top
        Header.Clear();
        Header.Add($"CTerm v1.0.0-nightly (Dev) - Screen: {Id}");
        Header.Add("-- -- -- -- -- -- -- -- -- -- -- --");
        Header.Draw(0, 0);

        // Display Output
        Output.Draw(0, height - 2);

        // Update Cursor Position
        if (height <= 0) { height = 1; }
        if (height > 30) { height = 30; }
        Console.SetCursorPosition(0, height - 1);
        string prompt = $"test@nightly > {_input}";
        Console.Write(prompt);
        Console.SetCursorPosition(prompt.Length, height - 1);
        Console.Title = $"CTerm Screen: {Id}";
    }
}
