using System.Collections.Generic;
using System.Linq;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public class CompositeDrawable : IDrawable
	{
		public CompositeDrawable(IList<IDrawable> drawables)
		{
			Drawables = drawables;
		}

		public IList<IDrawable> Drawables { get; private set; }

		public void Draw()
		{
			foreach (var drawable in Drawables)
				drawable.Draw();
		}
	}
}
