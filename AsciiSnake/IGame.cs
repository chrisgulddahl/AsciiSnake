namespace dk.ChrisGulddahl.AsciiSnake
{
	interface IGame
	{
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
