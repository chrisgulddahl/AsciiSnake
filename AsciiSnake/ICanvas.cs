using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public interface ICanvas : IEnumerable<ICanvasChar>
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

		/// <summary>
		/// Returns the difference between the canvas on which Diff is called 
		/// (referred to as canvas1) and the argument canvas (referred to as canvas2.
		/// I.e. writing the diff canvas to a console already containing canvas1
		/// corresponds exactly to writing canvas2 to an empty console in terms 
		/// of what will be visible on the console.
		/// The diff canvas consists of the characters on canvas2 that are new
		/// compared to canvas1 and for each deleted character (the characters that 
		/// canvas1 has but not canvas2) a null-char.
		/// </summary>
		/// <param name="canvas2">The canvas to compare the called canvas with.</param>
		/// <returns>The diff canvas between the called canvas and the argument canvas.</returns>
		ICanvas Diff(ICanvas canvas2);

		/// <summary>
		/// Write all canvas characters to console.
		/// </summary>
		/// <remarks>
		/// If multiple characters have been written to the canvas on the same position the latest one will be written to console.
		/// </remarks>
		void WriteToConsole();
	}
}
