using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public interface IDiffableCanvas : ICanvas, IEnumerable<ICanvasChar>
	{
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
		IDiffableCanvas Diff(IDiffableCanvas canvas2);

		/// <summary>
		/// Write all canvas characters to console.
		/// </summary>
		/// <remarks>
		/// If multiple characters have been written to the canvas on the same position the latest one will be written to console.
		/// </remarks>
		void WriteToConsole();
	}
}
