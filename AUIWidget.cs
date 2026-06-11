using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
	internal abstract class AUIWidget : IDrawable
	{
		public AUIWidget? NeighborUp { get; set; }
		public AUIWidget? NeighborDown { get; set; }
		public AUIWidget? NeighborLeft { get; set; }
		public AUIWidget? NeighborRight { get; set; }

		public int Left {  get; set; }
		public int Top { get; set; }

		public bool CaptureInput { get; protected set; }
		public bool IsSelected { get; set; }

		public abstract void Draw(DrawFrame frame);
		public abstract void Update(ConsoleKeyInfo input);
	}
}
