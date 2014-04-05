namespace dk.ChrisGulddahl.AsciiSnake
{
	interface IGame
	{
		/// <summary>
		/// Number of ticks since game started. A single tick occurs every time the snake moves.
		/// </summary>
		int CurrentTick { get; }

		/// <summary>
		/// Start and run the game by repeatedly calling <see cref="Tick"/>.
		/// </summary>
		void Start();
	}
}
