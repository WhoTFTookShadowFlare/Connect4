using finalProject.TerminalEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject.TerminalEngine
{
	internal abstract class AUIWidget : IDrawable
	{
		public AUIWidget? NeighborUp { get; set; } = null;
        public AUIWidget? NeighborDown { get; set; } = null;
        public AUIWidget? NeighborLeft { get; set; } = null;
        public AUIWidget? NeighborRight { get; set; } = null;

		public int Left { get; set; } = 0;
		public int Top { get; set; } = 0;

		public bool CaptureInput { get; protected set; } = false;
		public bool IsSelected { get; set; } = false;

		public abstract void Draw(DrawFrame frame);
		public abstract void Update(ConsoleKeyInfo input);
	}
}
