using System;
using System.Drawing;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public interface ICanvas
	{
		/// <summary>
		/// Height of canvas in char count.
		/// </summary>
		int Height { get; }

		/// <summary>
		/// Width of canvas in char count.
		/// </summary>
		int Width { get; }

		/// <summary>
		/// Draw a character to canvas using default console foreground color.
		/// </summary>
		/// <remarks>
		/// Default color is configured in the ConsoleForeground property of the injected <see cref="IConfig"/> instance.
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

		/// <summary>
		/// Draw a string to canvas starting from <paramref name="startPos"/> in the specified direction and color.
		/// </summary>
		/// <param name="str">String to be drawn.</param>
		/// <param name="startPos">Starting position of string.</param>
		/// <param name="direction"></param>
		/// <param name="color">Color of string.</param>
		void DrawString(string str, Point startPos, Direction direction, ConsoleColor color);
	}
}