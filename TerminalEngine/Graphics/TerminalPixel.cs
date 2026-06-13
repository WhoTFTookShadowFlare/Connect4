using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject.TerminalEngine.Graphics
{
	public sealed class TerminalPixel
	{
		public char Character { get; set; }
		public TerminalColor ForegroundColor { get; set; } = TerminalColor.WHITE;
		public TerminalColor BackgroundColor { get; set; } = TerminalColor.BLACK;

		public TerminalPixel(char character) {
			Character = character;
		}

		public TerminalPixel(char character, TerminalColor foregroundColor, TerminalColor backgroundColor) : this(character)
		{
			ForegroundColor = foregroundColor;
			BackgroundColor = backgroundColor;
		}

		public override string ToString()
		{
			return $"\x1b[{(int) ForegroundColor + 30}m\u001b[{(int)BackgroundColor + 40}m{Character}";
		}
	}
}
