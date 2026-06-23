using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
	internal abstract class APlayer
	{
		public string Name { get; private set; }
		public char Token { get; private set; }

		protected APlayer(string name, char token)
		{
			Name = name;
			Token = token;
		}

		public abstract byte? TakeTurn(ConsoleKeyInfo input, APlayer?[,] boardState);
	}
}
