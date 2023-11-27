namespace CheetahTerminal;

using System;

/// <summary>
/// <br>A console pixel is a single character on the screen.</br>
/// </summary>
public class ConsolePixel(Vector2i position, char character, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
{
    public bool IsDirty = true;
    private readonly Vector2i _position = position;
    private readonly ConsoleColor _backgroundColor = backgroundColor;

    private char _character = character;
    public char Character
    {
        get
        {
            return _character;
        }
        set
        {
            if (_character != value)
            {
                IsDirty = true;
                _character = value;
            }
        }
    }

    private ConsoleColor _foregroundColor = foregroundColor;
    public ConsoleColor ForegroundColor
    {
        get
        {
            return _foregroundColor;
        }
        set
        {
            if (_foregroundColor != value)
            {
                IsDirty = true;
                _foregroundColor = value;
            }
        }
    }

    public void Draw()
    {
        if (!IsDirty) { return; }
        IsDirty = false;

        Console.ForegroundColor = _foregroundColor;
        Console.BackgroundColor = _backgroundColor;
        Console.SetCursorPosition(_position.X, _position.Y);
        Console.Write(_character);
    }
}