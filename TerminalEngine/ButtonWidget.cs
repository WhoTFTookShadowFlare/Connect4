using finalProject.TerminalEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject.TerminalEngine
{
	internal class ButtonWidget : AUIWidget
	{
		public bool Pressed { get; private set; }

		public TerminalColor DefaultColor { get; set; }
		public TerminalColor SelectedColor { get; set; }

		public string Text { get; set; }

		public ButtonWidget(
			string text,
			TerminalColor defaultColor = TerminalColor.RED, TerminalColor selectedColor = TerminalColor.BRIGHT_RED
			)
		{
			Text = text;
			DefaultColor = defaultColor;
			SelectedColor = selectedColor;
		}

		public override void Update(ConsoleKeyInfo input)
		{
			Pressed = input.Key == ConsoleKey.Enter || input.Key == ConsoleKey.Spacebar;
		}

		public override void Draw(DrawFrame frame)
		{
			frame
				.DrawRect(Left, Top, Text.Length + 2, 3, IsSelected ? SelectedColor : DefaultColor)
				.DrawText(Left + 1, Top + 1, Text);
		}
	}
}
