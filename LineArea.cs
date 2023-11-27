namespace CheetahTerminal;

using System;

public class LineArea(int y, bool pinToBottom = false)
{
    public DrawArea area = new(new(0, y), new Vector2i(0, 1), pinToBottom);

    private string _text = string.Empty;
    public string Text
    {
        get
        {
            return _text;
        }
        set
        {
            _text = value;
            area.Write(_text);
        }
    }

    public void Draw()
    {
        area.Draw();
    }

    public void Clear()
    {
        _text = string.Empty;
        area.Clear();
    }
}
