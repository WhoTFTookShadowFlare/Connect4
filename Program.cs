namespace finalProject
{
	internal class Program
	{
		string[] displays =
		{
			"[\\]",
			"[|]",
			"[/]",
			"[-]"
		};

		ConsoleKey key = ConsoleKey.LeftWindows;
		int winWidth = 0, winHeight = 0;

		public Program()
		{	}

		~Program()
		{	}

		private DrawFrame SetupDraw()
		{
			Console.CursorVisible = false;
			winWidth = Console.WindowWidth;
			winHeight = Console.WindowHeight;

			DrawFrame frame = new(winWidth, winHeight);
			return frame;
		}

		public void Run()
		{
			int current = 0;
			while (key != ConsoleKey.Escape)
			{
				while (Console.KeyAvailable)
				{
					key = Console.ReadKey().Key;
				}

				DrawFrame frame = SetupDraw();

				current = (current + 1) % displays.Length;
				frame
					.DrawRect(0, 0, 5, 5, TerminalColor.BRIGHT_RED)
					.DrawText(3, 0, displays[current], TerminalColor.WHITE, TerminalColor.DONT_CARE)
                    .DrawText(5, 5, displays[current]);

				frame.Write(Console.Out);
				Thread.Sleep((int)Math.Floor(1.0 / 60.0 * 1000));
			}

			CleanUp();
		}

		private static void CleanUp()
		{
			Console.CursorVisible = true;
			Console.WriteLine("\x1b[0m");
			Console.WriteLine("\x1b[H");
			Console.WriteLine("\x1b[2J");
		}

		static void Main(string[] args)
		{
			Program app = new();
			app.Run();
		}
	}
}
