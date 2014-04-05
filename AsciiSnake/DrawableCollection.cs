using System.Collections.Generic;
using System.Linq;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class DrawableCollection : IDrawable
	{
		public DrawableCollection(IList<IDrawable> drawables)
		{
			Drawables = drawables;
		}

		public IList<IDrawable> Drawables { get; private set; }

		public void Draw(ICanvas canvas)
		{
			foreach (var drawable in Drawables)
				drawable.Draw(canvas);
		}
	}
}
