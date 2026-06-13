using finalProject.TerminalEngine;
using finalProject.TerminalEngine.Graphics;

namespace finalProject
{
	internal class Program
	{
		ConsoleKeyInfo key = new('\0', ConsoleKey.None, false, false, false);
		int winWidth = 0, winHeight = 0;

		public AGameScreen Screen { get; set; }
		
		private static Program? _instance = null;
		public static Program Instance {
			get
			{
				if (_instance == null) _instance = new Program();
				return _instance;
			}
		}

		private Program()
		{
			Screen = new GameSetupScreen();
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

				Screen.Update(key);

				DrawFrame frame = SetupDraw();
				Screen.Draw(frame);
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
				Program app = Program.Instance;
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
