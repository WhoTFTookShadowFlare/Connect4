using finalProject.TerminalEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject.TerminalEngine
{
	internal sealed class TextWidget : AUIWidget
	{
		public string Text { get; private set; }
		public int MaxLength { get; private set; }
		public TerminalColor FieldColor { get; private set; }
		public TerminalColor TextColor { get; private set; }
		public TerminalColor BorderColor {  get; private set; }
		public TerminalColor CapturedFieldColor { get; private set; }
		public TerminalColor CapturedTextColor { get; private set; }
		public TerminalColor HighlightBorderColor { get; private set; }

		public TextWidget(
			string startingText, int maxLength,
			TerminalColor fieldColor = TerminalColor.BLACK, TerminalColor textColor = TerminalColor.WHITE, TerminalColor borderColor = TerminalColor.RED,
			TerminalColor captureFieldColor = TerminalColor.WHITE, TerminalColor captureTextColor = TerminalColor.BLACK, TerminalColor highlightBorderColor = TerminalColor.BRIGHT_RED
			)
		{
			Text = startingText;
			MaxLength = maxLength;
			FieldColor = fieldColor;
			TextColor = textColor;
			BorderColor = borderColor;
			CapturedFieldColor = captureFieldColor;
			CapturedTextColor = captureTextColor;
			HighlightBorderColor = highlightBorderColor;
		}

		public override void Draw(DrawFrame frame)
		{
			frame
				.DrawRect(Left, Top, MaxLength + 2, 3, IsSelected ? HighlightBorderColor : BorderColor)
				.DrawRect(Left + 1, Top + 1, MaxLength, 1, CaptureInput ? CapturedFieldColor : FieldColor)
				.DrawText(Left + 1, Top + 1, Text, CaptureInput ? CapturedTextColor : TextColor);
		}

		public override void Update(ConsoleKeyInfo input)
		{
			if (input.Key == ConsoleKey.Enter)
			{
				CaptureInput = !CaptureInput;
				return;
			}
			if (!CaptureInput) return;

			if (input.Key == ConsoleKey.None) return;
			if(input.Key == ConsoleKey.Delete || input.Key == ConsoleKey.Backspace)
			{
				if (Text.Length == 0) return;
				if (input.Modifiers == ConsoleModifiers.Control) Text = "";
				else Text = Text[..^1];
				return;
			}

			if (Text.Length == MaxLength) return;
			if (input.KeyChar == '\0' || input.KeyChar == '\t') return;

			Text += input.KeyChar;
		}
	}
}
