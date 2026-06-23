using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
	internal class HumanPlayer : APlayer
	{
		public HumanPlayer(string name, char token) : base(name, token) { }

		public override byte? TakeTurn(ConsoleKeyInfo input, APlayer?[,] boardState)
		{
			if (input.Key == ConsoleKey.Spacebar)
			{
				return 1;
			}

			switch (input.Key)
			{
				case ConsoleKey.D1: return 0;
				case ConsoleKey.D2: return 1;
				case ConsoleKey.D3: return 2;
				case ConsoleKey.D4: return 3;
				case ConsoleKey.D5: return 4;
				case ConsoleKey.D6: return 5;
				case ConsoleKey.D7: return 6;
				default: return null;
			}
		}
	}
}
