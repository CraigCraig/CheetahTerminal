namespace CheetahTerminal;

using System;
using System.Collections.Generic;
using System.Linq;

public class ConsoleBuffer(DrawDirection direction)
{
    private readonly List<string> lines = [];
    public bool IsDirty = false;
    public DrawDirection Direction { get; private set; } = direction;

    public void Add(string line)
    {
        lines.Add(line);
        IsDirty = true;
    }

    public void Clear()
    {
        lines.Clear();
        IsDirty = true;
    }

    internal void Draw(int x, int y)
    {
        if (Direction == DrawDirection.Up)
        {
            var tmp = lines.Take(10).ToArray().Reverse();
            for (int i = tmp.Count() - 1; i >= 0; i--)
            {
                Console.SetCursorPosition(x, y - i);
                Console.WriteLine(tmp.ElementAt(i));
            }

            if (lines.Count > 25)
            {
                lines.RemoveRange(0, lines.Count - 25);
            }
        }

        if (Direction == DrawDirection.Down)
        {
            var tmp = lines.Take(10).ToArray();
            for (int i = 0; i < tmp.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(tmp.ElementAt(i));
            }

            if (lines.Count > 25)
            {
                lines.RemoveRange(0, lines.Count - 25);
            }
        }
    }
}
