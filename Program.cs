namespace finalProject
{
	internal class Program
	{
		ConsoleKeyInfo key = new('\0', ConsoleKey.None, false, false, false);
		int winWidth = 0, winHeight = 0;

		AGameScreen screen;
		APlayer? currentPlayer;
		APlayer? player1, player2;

		public Program()
		{
			screen = new GameSetupScreen();
		}

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
			DrawFrame.DefaultPixel.BackgroundColor = TerminalColor.BRIGHT_BLUE;
			while (key.Key != ConsoleKey.Escape)
			{
				if (Console.KeyAvailable)
					do
					{
						key = Console.ReadKey(true);
					} while (Console.KeyAvailable);
				else key = new('\0', ConsoleKey.None, false, false, false);

				screen.Update(key);

				DrawFrame frame = SetupDraw();
				screen.Draw(frame);
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
			try
			{
				Program app = new();
				app.Run();
			}
			catch (Exception e)
			{
                Console.WriteLine("A fatal error has occored, please report:");
                Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}
		}
	}
}
