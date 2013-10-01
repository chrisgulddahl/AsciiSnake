using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dk.ChrisGulddahl.AsciiSnake
{
	interface IGame
	{
		/**
		 * Wrapper for the console that among other things allow for better testing.
		 */
		IConsoleWrapper Console { get; }

		/**
		 * Number of ticks since game started.
		 * A tick occurs every time the snake moves.
		 */
		int Tick { get; }

		/**
		 * Game score (typically length of the snake)
		 */
		int Score { get;  }

		/**
		 * Starts the game.
		 */
		void Start();
	}
}
