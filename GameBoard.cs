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
		
        private readonly char[,] _board = new char[6, 7];

        private bool _gameOver = false;
        private APlayer? _winner = null;

		public APlayer Player1 { get { return _player1; } }
		public APlayer Player2 { get { return _player2; } }
		public APlayer CurrentPlayer { get  { return _currentPlayer; } }

		public GameBoard(APlayer player1, APlayer player2)
		{
			_player1 = player1;
			_currentPlayer = player1;
			_player2 = player2;
		}

		        private bool CheckWin(char token)
        {
            int[] dx = { 1, 0, 1, 1 };
            int[] dy = { 0, 1, 1, -1 };

            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (_board[row, col] != token) continue;

                    for (int i = 0; i < 4; i++)
                    {
                        if (IsLineMatch(row, col, dx[i], dy[i], token))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool IsLineMatch(int startRow, int startCol, int stepX, int stepY, char token)
        {
            for (int i = 1; i < 4; i++)
            {
                int nextRow = startRow + (stepY * i);
                int nextCol = startCol + (stepX * i);

                if (nextRow < 0 || nextRow >= 6 || nextCol < 0 || nextCol >= 7)
                {
                    return false;
                }

                if (_board[nextRow, nextCol] != token)
                {
                    return false;
                }
            }

            return true;
        }

		public override void Update(ConsoleKeyInfo input)
		{
			if (_gameOver)
            {	
                if (input.Key == ConsoleKey.D1)
                {
                	Program.Instance.Screen = new GameSetupScreen();
                }
                else if (input.Key == ConsoleKey.D0)
                {
                	Environment.Exit(0);
                }
			return;
            }
		
			byte? column = CurrentPlayer.TakeTurn(input);
			if (column is null) return;
			
			for (int row = 5; row >= 0; row--)
            {
                if (_board[row, column.Value] == ' ')
                {
                    _board[row, column.Value] = CurrentPlayer.Token;

                    if (CheckWin(CurrentPlayer.Token))
                    {
                        _winner = CurrentPlayer;
                        _gameOver = true;
                        return;
                    }

                    _currentPlayer =
                        (_currentPlayer == _player1)
                        ? _player2
                        : _player1;

                    break;
                }
            }
		}

		public override void Draw(DrawFrame frame)
		{
			for (int col = 0; col < 7; col++)
            {
                frame.DrawText(6 + col * 2, 9, (col + 1).ToString(), TerminalColor.YELLOW, TerminalColor.BLACK);
            }
		
			for (int x = 5; x < 19; x++)
			{
				frame.DrawRect(x, 10, 1, 10, (x % 2 == 0) ? TerminalColor.CYAN : TerminalColor.BRIGHT_CYAN);
			}

			for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    char piece = _board[row, col];

                    if (piece != ' ')
                    {
                        TerminalColor fgColor = (piece == 'X') ? TerminalColor.BLUE : TerminalColor.RED;
                        TerminalColor bgColor = TerminalColor.BLACK;

                        frame.DrawText(6 + col * 2, 11 + row, piece.ToString(), fgColor, bgColor);
                    }
                }
            }

            if (_gameOver)
            {
                string msg = _winner != null
                    ? $"{_winner.Name} Wins!"
                    : "Game Over";

                frame.DrawText(5, 3, msg, TerminalColor.GREEN, TerminalColor.BLACK);
                frame.DrawText(5, 4, "Press 1 to restart or 0 to exit", TerminalColor.WHITE, TerminalColor.BLACK);
            }
		}
	}
}
