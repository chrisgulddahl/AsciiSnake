using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public interface IDiffFlushableCanvas
	{
		/**
		 * Write changes (since last call of FlushChangesToConsole) to console.
		 */
		void FlushChangesToConsole();

		/**
		 * Write entire canvas to console including any pending changes.
		 */
		void WriteCurrentToConsole();

		/**
		 * Clear changes to canvas. After calling ClearChanges a call to FlushChangesToConsole will do nothing.
		 */
		void ClearChangesToCanvas();

		/**
		 * Draw character on canvas using default color
		 */
		void DrawChar(Point pos, char c);

		void DrawChar(Point pos, char c, ConsoleColor color);
	}
}
