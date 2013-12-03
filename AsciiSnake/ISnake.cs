using System.Drawing;
using System.Net;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public interface ISnake : IDrawable
	{

		/// <summary>
		/// Direction of the snake.
		/// </summary>
		/// <seealso cref="Direction"/>
		Direction Direction { get; set; }

		/// <summary>
		/// Length of snake.
		/// </summary>
		int Length { get;  }

		/// <summary>
		/// Coordinates of the head of the snake. Used for hit testing.
		/// </summary>
		Point Head { get; }

		/// <summary>
		/// Move the snake once according to its current direction. If current direction is "None" the snake does not move.
		/// </summary>
		void Move();

		/// <summary>
		/// Grow the snake a bit longer.
		/// </summary>
		void Grow();

		/// <summary>
		/// Returns true if the snake head has crashed with its body. False otherwise.
		/// </summary>
		/// <returns></returns>
		bool CrashedWithSelf();

		/// <summary>
		/// Returns whether any part of the snake currently occupies the given position.
		/// </summary>
		/// <param name="position">Position to test for.</param>
		/// <returns>Returns true if any part of the snake currently occupies the given position. False otherwise.</returns>
		bool ContainsPosition(Point position);
	}
}
