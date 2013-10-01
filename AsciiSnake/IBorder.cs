using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	interface IBorder : IDrawable
	{
		/**
		 * Returns whether a console cursor position collides with the border.
		 * Used for testing collision with the snake head.
		 */
		bool ContainsPosition(Point position);
	}
}
