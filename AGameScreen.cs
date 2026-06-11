using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
	internal abstract class AGameScreen : IDrawable
	{
        private AUIWidget? selected;
		public AUIWidget? Selected {
            get { return selected; }
            protected set
            {
                if(selected is not null) selected.IsSelected = false;
                selected = value;
                if(selected is not null) selected.IsSelected = true;
            }
        }

		public virtual void Update(ConsoleKeyInfo input)
		{
            if (selected is null) throw new Exception("A screen must have a UI Widget selected");
            if (selected.CaptureInput)
            {
                selected.Update(input);
                return;
            }

            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selected.NeighborUp is not null) Selected = selected.NeighborUp;
                    break;
                case ConsoleKey.DownArrow:
                    if (selected.NeighborDown is not null) Selected = selected.NeighborDown;
                    break;
                case ConsoleKey.LeftArrow:
                    if (selected.NeighborLeft is not null) Selected = selected.NeighborLeft;
                    break;
                case ConsoleKey.RightArrow:
                    if (selected.NeighborRight is not null) Selected = selected.NeighborRight;
                    break;
            }

            selected.Update(input);
        }

		public abstract void Draw(DrawFrame frame);
	}
}
