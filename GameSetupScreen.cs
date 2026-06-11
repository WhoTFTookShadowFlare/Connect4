using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
	internal class GameSetupScreen : AGameScreen
	{
		public const int PLAYER_NAME_MAX_LENGTH = 25;
		private TextWidget
			player1Name = new TextWidget("Player 1", PLAYER_NAME_MAX_LENGTH),
			player2Name = new TextWidget("Player 2", PLAYER_NAME_MAX_LENGTH);

		public GameSetupScreen()
		{
			Selected = player1Name;
			player1Name.Top = 0;
			player2Name.Top = 5;

			player1Name.NeighborDown = player2Name;
			player2Name.NeighborUp = player1Name;
		}

		public override void Update(ConsoleKeyInfo input)
		{
			base.Update(input);
		}

		public override void Draw(DrawFrame frame)
		{
			player1Name.Draw(frame);
			player2Name.Draw(frame);
		}
	}
}
