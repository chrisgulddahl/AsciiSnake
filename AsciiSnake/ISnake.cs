using System.Drawing;
using System.Net;

namespace dk.ChrisGulddahl.AsciiSnake
{
	public interface ISnake : IDrawable
	{
		bool Crashed { get; set; }

		/**
		 * Property used for setting the direction of the snake to North, East, South, West or None.
		 */
		Direction Direction { get; set; }

		/**
		 * Length of the snake
		 */
		int Length { get;  }

		/**
		 * Coordinates of the head of the snake. Used for hit testing.
		 */
		Point Head { get; }

		/**
		 * Move the snake once according to its current direction. If current direction is "None" the snake shouldn't move.
		 */
		void Move();

		/**
		 * Grow the snake a bit longer
		 */
		void Grow();

		/**
		 * Returns whether the snake head has crashed with its body
		 */
		bool CrashedWithSelf();

		/**
		 * Returns whether the snake currently occupies the given coordinate
		 */
		bool ContainsPosition(Point position);
	}
}
