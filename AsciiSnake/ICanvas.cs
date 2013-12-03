using System;
using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public interface ICanvas
	{
		/// <summary>
		/// Draw a character to canvas using default color.
		/// </summary>
		/// <remarks>
		/// Default color is configured in the injected <see cref="IConfig"/> instance.
		/// IConfig instance is injected in <see cref="DefaultGameFactory"/>.
		/// </remarks>
		/// <param name="pos">Position of character.</param>
		/// <param name="character">Character to be drawn.</param>
		void DrawChar(Point pos, char character);

		/// <summary>
		/// Draw a character to canvas using specified color.
		/// </summary>
		/// <param name="pos">Position of character.</param>
		/// <param name="character">Character to be drawn.</param>
		/// <param name="color">Color of character.</param>
		void DrawChar(Point pos, char character, ConsoleColor color);
	}
}