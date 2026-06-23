
namespace finalProject
{
	internal class AIPlayer : APlayer
	{
		private readonly byte _difficulty;
		
		public byte Difficulty { get { return _difficulty; } }

		public AIPlayer(string name, char token, byte difficulty) : base(name, token)
		{
			_difficulty = Math.Clamp(difficulty, (byte) 1, (byte) 10);
		}

		public override byte? TakeTurn(ConsoleKeyInfo input, APlayer?[,] boardState)
		{
			AIMove move = new AIMove(boardState, Difficulty, this);
			return move.GetBest();
		}

		private class AIMove : IComparable<AIMove>
		{
			private static Random rng = new Random();

			class DummyPlayer : APlayer
			{
				public static readonly DummyPlayer Dummy = new DummyPlayer();

				private DummyPlayer() : base("", '\0') { }

				public override byte? TakeTurn(ConsoleKeyInfo input, APlayer?[,] boardState)
				{ return null; }
			}

			enum Turn : sbyte
			{
				NONE = 0,
				THIS_BOT,
				OTHER_PLAYER
			}

			private readonly Turn[,] _state;
			private readonly byte? _move;
			private readonly byte _depth;

			private readonly AIMove[] _possibleMoves;

			private AIMove(Turn[,] state, byte depth, APlayer self, Turn currentTurn, byte? move)
			{
				_state = state;
				_move = move;
				_depth = depth;

				if (depth == 0)
				{
					_possibleMoves = Array.Empty<AIMove>();
					return;
				}

				depth -= 1;

				List<AIMove> possibleMoves = new List<AIMove>();
				for (byte col = 0; col < 7; col++)
				{
					if (_state[0, col] == Turn.NONE)
					{
						Turn[,] moveState = (Turn[,]) state.Clone();
						for(byte row = 5; row >= 0; row--)
						{
							if (moveState[row, col] == Turn.NONE)
							{
								moveState[row, col] = currentTurn;
								var next = new AIMove(moveState, depth, self, currentTurn == Turn.THIS_BOT ? Turn.OTHER_PLAYER : Turn.THIS_BOT, col);
								possibleMoves.Add(next);
								break;
							}
						}
					}
				}

				_possibleMoves = possibleMoves.ToArray();
			}

			public AIMove(APlayer?[,] state, byte depth, APlayer self) : this(ProcessBoard(state, self), depth, self, Turn.THIS_BOT, null)
			{	}

			private static Turn[,] ProcessBoard(APlayer?[,] board, APlayer self)
			{
				Turn[,] processed = new Turn[6, 7];

				for (byte row = 0; row < 6; row++)
				{
					for (byte col = 0; col < 7; col++)
					{
						if (board[row, col] is null) processed[row, col] = Turn.NONE;
						else processed[row, col] = (board[row, col] == self) ? Turn.THIS_BOT : Turn.OTHER_PLAYER;
					}
				}

				return processed;
			}

			public byte GetBest()
			{
				Array.Sort(_possibleMoves);

				List<AIMove> best = _possibleMoves.Where(move => move.Score() >= _possibleMoves[0].Score()).ToList();

				return (byte) best[rng.Next(best.Count)]._move;
			}

			public int Score()
			{
				if(_depth != 0)
				{
					Array.Sort(_possibleMoves);
					return _possibleMoves[0].Score();
				}

				int score = 0;

				int[] dx = [1, 1, 1, 0];
				int[] dy = [1, 0, -1, -1];
				for (byte row = 0; row < 5; row++)
				{
					for (byte col = 0; col < 7; col++)
					{
						if (_state[row, col] == Turn.NONE) continue;

						Turn check = _state[row, col];
						for(byte dir = 0; dir < 4; dir++)
						{
							int targetRow = row;
							int targetCol = col;
							int distance = 1;
							while (
								((targetRow >= 0 && targetRow < 6) && (targetCol >= 0 && targetCol < 7))
								&& _state[targetRow, targetCol] == check)
							{
								targetRow += dx[dir];
								targetCol += dy[dir];

								if (distance < 4)
								{
									score += (check == Turn.THIS_BOT ? 10 : -10) * distance;
								}
								else
								{
									score += (check == Turn.THIS_BOT ? 10 : -10) * 500;
								}
								distance++;
							}
						}
						continue;
					}
				}

				return score;
			}

			public int CompareTo(AIMove? other)
			{
				if (other == null) return 1;
				return Score() - other.Score();
			}
		}
	}
}
