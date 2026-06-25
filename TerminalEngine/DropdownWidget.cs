using finalProject.TerminalEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject.TerminalEngine
{
	internal class DropdownWidget : AUIWidget
	{
		private readonly string[] _options;
		private readonly int _longestOption;

		public int Selected { get; private set; }

		public TerminalColor SelectedOptionColor { get; private set; }
		public TerminalColor BorderColor { get; private set; }
		public TerminalColor HighlightBorderColor { get; private set; }

		public DropdownWidget(
			string[] options, TerminalColor selectedOptionColor = TerminalColor.BRIGHT_BLACK,
			TerminalColor defaultColor = TerminalColor.RED, TerminalColor selectedColor = TerminalColor.BRIGHT_RED
			)
		{
			_options = options;
			foreach (var option in this._options)
			{
				_longestOption = Math.Max(option.Length, this._longestOption);
			}

			SelectedOptionColor = selectedOptionColor;
			BorderColor = defaultColor;
			HighlightBorderColor = selectedColor;
		}

		public override void Draw(DrawFrame frame)
		{
			if (!CaptureInput)
				frame
					.DrawRect(Left, Top, _longestOption + 3, 3, IsSelected ? HighlightBorderColor : BorderColor)
					.DrawText(Left + 1, Top + 1, _options[Selected]);
			else
			{
				frame
					.DrawRect(Left, Top, _longestOption + 3, _options.Length + 2, HighlightBorderColor)
					.DrawRect(Left + 1, Top + 1 + Selected, _longestOption, 1, SelectedOptionColor);
				
				for (int idx = 0; idx < _options.Length; idx++)
				{
					frame.DrawText(Left + 1, Top + 1 + idx, _options[idx]);
				}
			}
		}

		public override void Update(ConsoleKeyInfo input)
		{
			if (input.Key == ConsoleKey.Enter)
			{
				CaptureInput = !CaptureInput;
				return;
			}
			if (!CaptureInput) return;

			if (input.Key == ConsoleKey.UpArrow) Selected = Math.Max(0, Selected - 1);
			if (input.Key == ConsoleKey.DownArrow) Selected = Math.Min(_options.Length - 1, Selected + 1);
		}
	}
}
