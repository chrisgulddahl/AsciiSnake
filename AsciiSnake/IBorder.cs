using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public interface IBorder : IDrawable
	{
		/**
		 * Returns whether a console cursor position collides with the border.
		 * Used for testing collision with the snake head.
		 */
		bool ContainsPosition(Point position);
	}
}
