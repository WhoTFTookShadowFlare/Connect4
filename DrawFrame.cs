using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
	public sealed class DrawFrame
	{
		private static readonly TerminalPixel defaultPixel = new TerminalPixel(' ');

		private readonly int _width;
		private readonly int _height;

		private readonly TerminalPixel[,] _pixels;

		public int Width { get { return _width; } }
		public int Height { get { return _height; } }

		public DrawFrame(int width, int height)
		{
			_width = width;
			_height = height;

			_pixels = new TerminalPixel[width, height];
			for(int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					_pixels[x, y] = defaultPixel;
				}
			}
		}

		private int CalcPixelIndex(int x, int y)
		{
			return x + y * Width;
		}

		public DrawFrame DrawText(
			int x, int y, string text,
			TerminalColor fgColor = TerminalColor.WHITE, TerminalColor bgColor = TerminalColor.DONT_CARE
			)
		{
			for (int idx = 0; idx < text.Length; idx++)
			{
				TerminalColor targetFGColor = fgColor;
				TerminalColor targetBGColor = bgColor;
				if (fgColor == TerminalColor.DONT_CARE) targetFGColor = _pixels[x + idx, y].ForegroundColor;
				if (bgColor == TerminalColor.DONT_CARE) targetBGColor = _pixels[x + idx, y].BackgroundColor;
                _pixels[x + idx, y] = new TerminalPixel(text[idx], targetFGColor, targetBGColor);
			}

			return this;
		}

		public DrawFrame DrawRect(int x, int y, int width, int height, TerminalColor color)
		{
			for(int rx = 0; rx < height; rx++)
			{
				for(int ry = 0; ry < width; ry++)
				{
					_pixels[x + rx, y + ry] = new TerminalPixel(' ', color, color);
				}
			}

			return this;
		}

		public void Write(TextWriter output)
		{
			StringBuilder frameOutput = new StringBuilder();
			frameOutput.Capacity = Width * Height + 4096;
			output.Write("\x1b[H");
			for (int x = 0; x < Height; x++)
			{
				for (int y = 0; y < Width; y++)
				{
					frameOutput.Append(_pixels[y, x].ToString());
				}
			}
			output.Write(frameOutput.ToString());
		}
	}
}
