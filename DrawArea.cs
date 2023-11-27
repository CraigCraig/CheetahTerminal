namespace CheetahTerminal;

#region Using Statements
using System;
using System.Collections.Generic;
using CheetahUtils;
#endregion

/// <summary>
/// <br>A draw area is a section of the screen that can be drawn to.</br>
/// <br>It is used to prevent the entire screen from being redrawn every frame.</br>
/// </summary>
/// <param name="position"></param>
/// <param name="size"></param>
public class DrawArea(Vector2i position, Vector2i size, bool pinToBottom = false)
{
	public Vector2i Position = position;
	public Vector2i Size = size;
	public bool IsDirty = true;
	public bool PinToBottom { get; private set; } = pinToBottom;

	public Dictionary<Vector2i, ConsolePixel> PixelBuffer = [];

	/// <summary>
	/// Draw the draw area on to the screen
	/// </summary>
	public void Draw()
	{
		// UNDONE: DrawArea.Draw() is not finished.

		var temp_position = Position;

		// Update Draw Area Size
		Size = new Vector2i(Console.BufferWidth - 1, Console.BufferHeight - 1);

		// If the draw area is pinned to the bottom, move it to the bottom
		if (PinToBottom)
		{
			temp_position = new Vector2i(Position.X, Console.BufferHeight - Size.Y);
		}

		// Check if the draw area is valid
		if (temp_position.X < 0 || temp_position.Y < 0) { return; }
		if (Size.X < 0 || Size.Y < 0) { return; }

		// Check if the draw area is within the screen bounds
		if (temp_position.X + Size.X > Console.BufferWidth) { return; }
		if (temp_position.Y + Size.Y > Console.BufferHeight) { return; }

		// Draw Pixels
		foreach (var pixel in PixelBuffer)
		{
			pixel.Value.Draw();
		}
	}

	public void Move(Vector2i position)
	{
		Position = position;
		IsDirty = true;
	}

	public void Move(int x, int y)
	{
		Position = new(x, y);
		IsDirty = true;
	}

	internal void Write(string text)
	{
		WriteAt(Position, text);
	}

	internal void WriteAt(Vector2i position, string text)
	{
		position += Position;
		int tmpX = position.X;
		foreach (var c in text)
		{
			WriteAt(new Vector2i(tmpX, position.Y), c);
			tmpX++;
		}

		if (tmpX < Size.X)
		{
			for (int i = tmpX; i < Size.X; i++)
			{
				WriteAt(new Vector2i(i, position.Y), ' ');
			}
		}
	}

	internal void WriteAt(Vector2i position, char c, ConsoleColor? color = null)
	{
		ConsoleColor fColor = ConsoleColor.Gray;

		// Check if text overflows the draw area and console
		if (position.X > Size.X) { return; }
		if (position.Y > Size.Y) { return; }

		// Reposition to bottom if needed
		int tmpY = PinToBottom ? Size.Y - position.Y : position.Y;

		// Check if tmpY is over the buffer height
		if (tmpY >= Console.BufferHeight) { return; }

		// Check if Pixel is in PixelBuffer, if not create it
		Vector2i ppos = new(position.X, position.Y);

		if (color != null)
		{
			fColor = color.Value;
		}

		_ = PixelBuffer.TryGetValue(ppos, out ConsolePixel? value);

		if (value == null)
		{
			PixelBuffer.Add(ppos, new ConsolePixel(ppos, c, fColor, ConsoleColor.Black));
		}
		else
		{
			if (value.Character != c)
			{
				value.Character = c;
			}
		}
	}

	internal void WriteAt(int x, int y, string text)
	{
		WriteAt(new Vector2i(x, y), text);
	}

	internal void Clear()
	{
		foreach (var pixel in PixelBuffer)
		{
			pixel.Value.Character = ' ';
			pixel.Value.ForegroundColor = ConsoleColor.Gray;
		}
	}
}