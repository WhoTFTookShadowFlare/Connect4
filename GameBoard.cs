using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
	internal class GameBoard : AGameScreen
	{
		public GameBoard() { }

        public override void Update(ConsoleKeyInfo input)
        {
            
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
