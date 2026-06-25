using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
	internal class RandomPlayer : APlayer
	{
		private Random rng = new Random();

		public RandomPlayer(string name, char token) : base(name, token)
		{   }

		public override byte? TakeTurn(ConsoleKeyInfo input, APlayer?[,] boardState)
		{
			return (byte) rng.Next(7);
		}
	}
}
