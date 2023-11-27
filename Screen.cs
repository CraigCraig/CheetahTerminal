namespace CheetahTerminal;

using System;
using System.Text;

public partial class Screen(Terminal terminal, int id)
{
    public int Id = id;
    private readonly Terminal _terminal = terminal;
    private bool _isDirty = true;

    private readonly StringBuilder _input = new();
    private StringBuilder _last_input = new();

    private readonly string _currentDirectory = Environment.CurrentDirectory;

    public readonly LineArea Header = new(0);
    //public readonly DrawArea Output = new(new(), new());
    public readonly LineArea Prompt = new(1, true);

    public Vector2i CursorInputPosition = new(); // UNDONE: This is not used yet

    private int width = Console.BufferWidth;
    private int height = Console.BufferHeight;

    internal void HandleKeyPress()
    {
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
            _isDirty = true;
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
            _isDirty = true;
            if (_input.Length == 0) { return; }
            //Output.Add(_input.ToString());

            var cmd = _input.ToString().Split(' ')[0];
            var args = _input.ToString().Split(' ')[1..];
            var result = _terminal.ModuleManager.ExecuteCommand(cmd, args);
            Prompt.Clear();
            //Output.Add($"{result?.Message}");
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
        // TODO: Add different screen layouts

        if (width != Console.BufferWidth || height != Console.BufferHeight)
        {
            width = Console.BufferWidth;
            height = Console.BufferHeight;
            _isDirty = true;
        }

        // Draw Header
        Header.Text = $"{DateTime.Now}";
        Header.Draw();

        // Draw Output Area
        //OutputArea.Draw();

        // Draw Prompt
        Prompt.Text = $"test@nightly > {_input}";
        Prompt.Draw();
    }

    internal void Close()
    {
        throw new NotImplementedException();
    }
}