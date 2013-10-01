using System.Collections.Generic;
using System.Linq;

namespace dk.ChrisGulddahl.AsciiSnake
{
	class CompositeDrawable : IDrawable
	{
		private List<IDrawable> _drawables;

		public CompositeDrawable(List<IDrawable> drawables)
		{
			_drawables = drawables;
			Console = (_drawables.First() != null ? _drawables.First().Console : null);
		}

		public IConsoleWrapper Console { get; private set; }

		public void Draw()
		{
			foreach(var drawable in _drawables)
				drawable.Draw();
		}

		public void Redraw()
		{
			foreach (var drawable in _drawables)
				drawable.Redraw();
		}

		public bool NeedsRedraw()
		{
			return _drawables.Aggregate<IDrawable, bool>(false, (needsRedraw, drawable) => needsRedraw || drawable.NeedsRedraw());
		}
	}
}
