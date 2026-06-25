using finalProject.TerminalEngine;
using finalProject.TerminalEngine.Graphics;
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

		private DropdownWidget
			p1Type = new DropdownWidget([ "Human", "Random", "Smart (beta)" ]),
			p2Type = new DropdownWidget([ "Human", "Random", "Smart (beta)" ]);

		private ButtonWidget startGame = new ButtonWidget("Start Game");

		public GameSetupScreen()
		{
			Selected = player1Name;
			player1Name.Top = 0;
			player2Name.Top = 5;

			p1Type.Top = 0;
			p1Type.Left = 30;

			p2Type.Top = 5;
			p2Type.Left = 30;

			startGame.Top = 10;

			player1Name.NeighborDown = player2Name;
			player2Name.NeighborUp = player1Name;

			player2Name.NeighborRight = p2Type;
			p2Type.NeighborLeft = player2Name;
			p2Type.NeighborUp = p1Type;
			p1Type.NeighborDown = p2Type;
			p1Type.NeighborLeft = player1Name;
			player1Name.NeighborRight = p1Type;

			startGame.NeighborUp = player2Name;
			player2Name.NeighborDown = startGame;
		}

		private static void SetupPlayer(ref APlayer? player, ref DropdownWidget typeSelection, ref TextWidget name, char token)
		{
			switch (typeSelection.Selected)
			{
				case 0:
					player = new HumanPlayer(name.Text, token);
					break;
				case 1:
					player = new RandomPlayer(name.Text, token);
					break;
				case 2:
					player = new AIPlayer(name.Text, token, 5);
					break;
			}
		}

		public override void Update(ConsoleKeyInfo input)
		{
			base.Update(input);

			if(startGame.Pressed)
			{
				APlayer? player1 = null;
				APlayer? player2 = null;

				SetupPlayer(ref player1, ref p1Type, ref player1Name, 'X');
				SetupPlayer(ref player2, ref p2Type, ref player2Name, 'O');

				if (player1 == null || player2 == null) throw new Exception("Player 1 or 2 has an invalid type.");
				Program.Instance.Screen = new GameBoard(player1, player2);
			}
		}

		public override void Draw(DrawFrame frame)
		{
			player1Name.Draw(frame);
			player2Name.Draw(frame);
			p1Type.Draw(frame);
			p2Type.Draw(frame);
			startGame.Draw(frame);
		}
	}
}
