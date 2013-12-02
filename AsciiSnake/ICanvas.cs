using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public interface ICanvas : IEnumerable<IEnumerable<ICanvasChar>>, ICloneable
	{
		/**
		 * Draw character on canvas using default color
		 */
		void DrawChar(Point pos, char c);

		void DrawChar(Point pos, char c, ConsoleColor color);

		/**
		 * Returns the most recently added ICanvasChar on given position
		 */
		ICanvasChar TopCharAtPos(Point pos);

		void Reset();

		/**
		 * Returns the CanvasChars to be added to the caller for it to be equal to
		 * the argument canvas in terms of how they are written to console.
		 * Missing/deleted chars is represented by a null-char.
		 * I.e. writing the calling canvas to console and afterwards writing the
		 * diff canvas to console corresponds exactly to writing the argument canvas to a 
		 * clear console in terms of what will be visible on the console.
		 */
		ICanvas Diff(ICanvas canvas);

		void WriteToConsole();
	}
}
