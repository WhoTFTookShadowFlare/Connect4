using finalProject.TerminalEngine;
using finalProject.TerminalEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
	internal class GameBoard : AGameScreen
	{
		private readonly APlayer _player1, _player2;
		private APlayer _currentPlayer;

		public APlayer Player1 { get { return _player1; } }
		public APlayer Player2 { get { return _player2; } }
		public APlayer CurrentPlayer { get  { return _currentPlayer; } }

		public GameBoard(APlayer player1, APlayer player2)
		{
			_player1 = player1;
			_currentPlayer = player1;
			_player2 = player2;
		}

		public override void Update(ConsoleKeyInfo input)
		{
			byte? column = CurrentPlayer.TakeTurn(input);
			if (column is null) return;
			// TODO: Place on board
		}

		public override void Draw(DrawFrame frame)
		{
			for (int x = 5; x < 19; x++)
			{
				frame.DrawRect(x, 10, 1, 10, (x % 2 == 0) ? TerminalColor.CYAN : TerminalColor.BRIGHT_CYAN);
			}
		}
	}
}
