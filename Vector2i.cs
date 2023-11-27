namespace CheetahTerminal;

public class Vector2i(int x = 0, int y = 0)
{
    public int X = x;
    public int Y = y;

    // Override Add Operator
    public static Vector2i operator +(Vector2i a, Vector2i b)
    {
        return new Vector2i(a.X + b.X, a.Y + b.Y);
    }

    // Override Subtract Operator
    public static Vector2i operator -(Vector2i a, Vector2i b)
    {
        return new Vector2i(a.X - b.X, a.Y - b.Y);
    }
}